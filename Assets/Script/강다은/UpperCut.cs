using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCut : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("UpperCut 시작");
        patternTimer = 0f; // 타이머 초기화
        enemyInstances.Clear();
        boxesList.Clear();
    }

    private void Start() { }

    private void Update()
    {     
        Delete();
    }

    private void OnDisable()
    {
        CleanUpAllPatternObjects();
        Debug.Log("UpperCut 끝 및 오브젝트 클린 작업 완료");
    }

    public void StartPattern()
    {
    	StartCoroutine(RunPatternSequence());
    }

    private void CleanUpAllPatternObjects()
    {
        Debug.Log("CleanUpAllPatternObjects 호출됨");
        foreach (var enemy in enemyInstances)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        enemyInstances.Clear();

        foreach (var boxes in boxesList)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] != null)
                    Destroy(boxes[i]);
            }
        }
        boxesList.Clear();

        Destroy(gameObject);
    }

    private void CleanUpPatternObjects(GameObject enemy, GameObject[] boxes)
    {
        Debug.Log("CleanUpPatternObjects 호출됨");
        if (enemy != null)
        {
            enemyInstances.Remove(enemy);
            Destroy(enemy);
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i] != null)
                Destroy(boxes[i]);
        }
        boxesList.Remove(boxes);
    }
    private void Delete()
    {
        time += Time.deltaTime;

        if(time > 10.0f)
        {
            Destroy(gameObject);
        }
    }
    private float GetRandomX()
    {
        float randomX;
        int maxAttempts = 10; // 최대 시도 횟수
        float minDistance = 2f; // 최소 거리 (겹침 방지)

        for (int i = 0; i < maxAttempts; i++)
        {
            randomX = Random.Range(-6f, 6f);

            bool isTooClose = false;
            foreach (float prevX in previousRandomXValues)
            {
                if (Mathf.Abs(randomX - prevX) < minDistance)
                {
                    isTooClose = true;
                    break;
                }
            }

            if (!isTooClose)
            {
                // 이전 값 리스트에 추가 (최대 3개만 유지)
                previousRandomXValues.Add(randomX);
                if (previousRandomXValues.Count > 3)
                    previousRandomXValues.RemoveAt(0);
                return randomX;
            }
        }

        // 시도 횟수를 초과하면 기본값 반환
        randomX = Random.Range(-6f, 6f);
        previousRandomXValues.Add(randomX);
        if (previousRandomXValues.Count > 3)
            previousRandomXValues.RemoveAt(0);
        return randomX;
    }

    private IEnumerator RunPatternSequence()
    {
        float randomX = GetRandomX();

        GameObject enemyInstance = Instantiate(enemyPrefab, new Vector3(randomX, -8f, 0), Quaternion.identity);
        SpriteRenderer enemyRenderer = enemyInstance.GetComponentInChildren<SpriteRenderer>();
        Animator enemyAnimator = enemyInstance.GetComponentInChildren<Animator>();
        GameObject[] boxes = new GameObject[8]; // 이 패턴 전용 boxes 배열

        enemyInstances.Add(enemyInstance);
        boxesList.Add(boxes);

        if (enemyRenderer == null || enemyAnimator == null)
        {
            CleanUpPatternObjects(enemyInstance, boxes);
            yield break;
        }

        yield return StartCoroutine(PerformUppercut(enemyInstance, enemyRenderer, enemyAnimator, randomX, boxes));

        // 패턴이 끝난 후 정리
        CleanUpPatternObjects(enemyInstance, boxes);
    }

    private IEnumerator PerformUppercut(GameObject enemyInstance, SpriteRenderer enemyRenderer, Animator enemyAnimator, float randomX, GameObject[] boxes)
    {
        // 1. Ready: 올라가며 3번 블링크 (0.5초) → 약간 하강 (0.1초)
        enemyAnimator.Play("Ready", -1, 0f);
        Debug.Log("Ready 재생 중");
        yield return StartCoroutine(MoveUp(enemyInstance, randomX, 0.5f));
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(SlightDescend(enemyInstance, randomX, 0.1f));

        // 2. UpperCut: 점프 (0.1초) → 박스 생성 (1.3초, 첫 박스 사라짐 후 하강) → 하강 (0.3초)
        enemyAnimator.Play("UpperCut", -1, 0f);
        Debug.Log("UpperCut 재생 중");
        yield return StartCoroutine(JumpAndDescend(enemyInstance, randomX, 0.4f, boxes));
    }

    private IEnumerator MoveUp(GameObject enemyInstance, float randomX, float duration)
    {
        Vector3 startPos = new Vector3(randomX, -6f, 0);
        Vector3 endPos = new Vector3(randomX, -5f, 0); // Ready 위치
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            if (enemyInstance != null)
                enemyInstance.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (enemyInstance != null)
            enemyInstance.transform.position = endPos;
    }

    private IEnumerator SlightDescend(GameObject enemyInstance, float randomX, float duration)
    {
        Vector3 startPos = new Vector3(randomX, -5f, 0); // Ready 위치
        Vector3 endPos = new Vector3(randomX, -5.5f, 0);   // 약간 하강
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            if (enemyInstance != null)
                enemyInstance.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (enemyInstance != null)
            enemyInstance.transform.position = endPos;
    }

    private IEnumerator JumpAndDescend(GameObject enemyInstance, float randomX, float duration, GameObject[] boxes)
    {
        Vector3 startPos = new Vector3(randomX, -5.5f, 0);  // SlightDescend 이후 위치
        Vector3 peakPos = new Vector3(randomX, -3.0f, 0);   // 점프 최고점
        Vector3 endPos = new Vector3(randomX, -8f, 0);      // 하강 위치
        float jumpDuration = 0.1f; // 점프 시간
        float descendDuration = duration - jumpDuration; // 하강 시간 (0.3초)

        // 점프 (0.1초)
        float jumpElapsed = 0f;
        while (jumpElapsed < jumpDuration)
        {
            jumpElapsed += Time.deltaTime;
            float t = jumpElapsed / jumpDuration;
            if (enemyInstance != null)
                enemyInstance.transform.position = Vector3.Lerp(startPos, peakPos, t);
            yield return null;
        }
        if (enemyInstance != null)
            enemyInstance.transform.position = peakPos;

        // 박스 생성 시작 (첫 박스가 사라질 때까지 대기 후 하강)
        StartCoroutine(GenerateBoxes(randomX, boxes));
        yield return new WaitForSeconds(0.15f); // 첫 박스 사라짐 타이밍 (0.1초 성장 + 0.05초 대기)

        // 하강 (0.3초)
        float descendElapsed = 0f;
        while (descendElapsed < descendDuration)
        {
            descendElapsed += Time.deltaTime;
            float t = descendElapsed / descendDuration;
            if (enemyInstance != null)
                enemyInstance.transform.position = Vector3.Lerp(peakPos, endPos, t);
            yield return null;
        }

        if (enemyInstance != null)
            enemyInstance.transform.position = endPos;

        // 박스 생성이 끝날 때까지 대기 (나머지 1.15초)
        yield return new WaitForSeconds(1.3f - 0.15f);
    }

    private IEnumerator GenerateBoxes(float baseX, GameObject[] boxes)
    {
        boxes[3] = Instantiate(boxPrefab, new Vector3(baseX, 0, 0), Quaternion.identity);
        boxes[3].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        yield return StartCoroutine(GrowBox(boxes[3], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));

        yield return new WaitForSeconds(0.05f);

        yield return new WaitForSeconds(0.025f);
        boxes[2] = Instantiate(boxPrefab, new Vector3(baseX - 0.35f, 0, 0), Quaternion.identity);
        boxes[4] = Instantiate(boxPrefab, new Vector3(baseX + 0.35f, 0, 0), Quaternion.identity);
        boxes[2].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        boxes[4].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        StartCoroutine(GrowBox(boxes[2], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));
        yield return StartCoroutine(GrowBox(boxes[4], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));

        yield return new WaitForSeconds(0.025f);
        boxes[1] = Instantiate(boxPrefab, new Vector3(baseX - 0.7f, 0, 0), Quaternion.identity);
        boxes[5] = Instantiate(boxPrefab, new Vector3(baseX + 0.7f, 0, 0), Quaternion.identity);
        boxes[1].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        boxes[5].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        StartCoroutine(GrowBox(boxes[1], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));
        yield return StartCoroutine(GrowBox(boxes[5], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));

        yield return new WaitForSeconds(0.025f);
        if (boxes[3] != null)
        {
            yield return StartCoroutine(ShrinkBox(boxes[3]));
            Destroy(boxes[3]);
            boxes[3] = null;
        }
        boxes[0] = Instantiate(boxPrefab, new Vector3(baseX - 1.05f, 0, 0), Quaternion.identity);
        boxes[6] = Instantiate(boxPrefab, new Vector3(baseX + 1.05f, 0, 0), Quaternion.identity);
        boxes[0].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        boxes[6].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        StartCoroutine(GrowBox(boxes[0], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));
        yield return StartCoroutine(GrowBox(boxes[6], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));

        yield return new WaitForSeconds(0.025f);
        if (boxes[2] != null && boxes[4] != null)
        {
            StartCoroutine(ShrinkBox(boxes[2]));
            StartCoroutine(ShrinkBox(boxes[4]));
            yield return new WaitForSeconds(0.1f);
            Destroy(boxes[2]);
            Destroy(boxes[4]);
            boxes[2] = null;
            boxes[4] = null;
        }
        boxes[3] = Instantiate(boxPrefab, new Vector3(baseX - 1.4f, 0, 0), Quaternion.identity);
        boxes[7] = Instantiate(boxPrefab, new Vector3(baseX + 1.4f, 0, 0), Quaternion.identity);
        boxes[3].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        boxes[7].transform.localScale = new Vector3(0.05f, screenHeight, 1f);
        StartCoroutine(GrowBox(boxes[3], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));
        yield return StartCoroutine(GrowBox(boxes[7], new Vector3(0.05f, screenHeight, 1f), new Vector3(0.25f, screenHeight, 1f), 0.1f));

        yield return new WaitForSeconds(0.025f);
        if (boxes[1] != null && boxes[5] != null)
        {
            StartCoroutine(ShrinkBox(boxes[1]));
            StartCoroutine(ShrinkBox(boxes[5]));
            yield return new WaitForSeconds(0.1f);
            Destroy(boxes[1]);
            Destroy(boxes[5]);
            boxes[1] = null;
            boxes[5] = null;
        }

        yield return new WaitForSeconds(0.025f);
        if (boxes[0] != null && boxes[6] != null)
        {
            StartCoroutine(ShrinkBox(boxes[0]));
            StartCoroutine(ShrinkBox(boxes[6]));
            yield return new WaitForSeconds(0.1f);
            Destroy(boxes[0]);
            Destroy(boxes[6]);
            boxes[0] = null;
            boxes[6] = null;
        }

        yield return new WaitForSeconds(0.025f);
        if (boxes[3] != null && boxes[7] != null)
        {
            StartCoroutine(ShrinkBox(boxes[3]));
            StartCoroutine(ShrinkBox(boxes[7]));
            yield return new WaitForSeconds(0.1f);
            Destroy(boxes[3]);
            Destroy(boxes[7]);
            boxes[3] = null;
            boxes[7] = null;
        }
    }

    private IEnumerator GrowBox(GameObject box, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (box != null)
                box.transform.localScale = Vector3.Lerp(startScale, endScale * 2.2f, elapsed / duration);
            yield return null;
        }
    }

    private IEnumerator ShrinkBox(GameObject box)
    {
        Vector3 startScale = new Vector3(0.25f, screenHeight * 2f, 1f);
        Vector3 endScale = new Vector3(0.05f, screenHeight * 2f, 1f);
        float duration = 0.1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (box != null)
                box.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            yield return null;
        }
    }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject boxPrefab;
    private const float screenHeight = 10f;

    private float patternTimer = 0f; // 1초 타이머
    private const float patternInterval = 1f; // 패턴 실행 간격

    // 각 패턴의 오브젝트를 관리하기 위한 리스트
    private List<GameObject> enemyInstances = new List<GameObject>();
    private List<GameObject[]> boxesList = new List<GameObject[]>();
    private List<float> previousRandomXValues = new List<float>(); // 이전 randomX 값 저장
    private float time = 0;
}