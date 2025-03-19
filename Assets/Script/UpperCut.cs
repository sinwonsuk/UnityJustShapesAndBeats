using System.Collections;
using UnityEngine;

public class UpperCut : State<OneStage>
{
	public override void Enter(OneStage entity)
	{
		Debug.Log("UpperCut 시작");
		StartPattern(entity);
	}

	public override void Execute(OneStage entity) { }

	public override void Exit(OneStage entity)
	{
		CleanUpPatternObjects();
		Debug.Log("UpperCut 끝 및 오브젝트 클린 작업 완료");
	}

	private void StartPattern(OneStage entity)
	{
		isPatternRunning = true;
		patternCount = 0;
		entity.StartCoroutine(RunPatternSequence(entity));
	}

	private void CleanUpPatternObjects()
	{
		Debug.Log("CleanUpPatternObjects 호출됨");
		isPatternRunning = false;
		if (enemyInstance != null)
		{
			Destroy(enemyInstance);
			enemyInstance = null;
		}
		for (int i = 0; i < boxes.Length; i++)
		{
			if (boxes[i] != null)
			{
				Destroy(boxes[i]);
				boxes[i] = null;
			}
		}
	}

	private IEnumerator RunPatternSequence(OneStage entity)
	{
		while (patternCount < maxPatterns)
		{
			if (enemyInstance != null)
			{
				CleanUpPatternObjects();
			}

			float randomX = Random.Range(-6f, 6f);

			enemyInstance = Instantiate(enemyPrefab, new Vector3(randomX, -8f, 0), Quaternion.identity);
			enemyRenderer = enemyInstance.GetComponentInChildren<SpriteRenderer>();
			enemyAnimator = enemyInstance.GetComponentInChildren<Animator>();

			if (enemyRenderer == null || enemyAnimator == null)
			{
				yield break;
			}

			yield return StartCoroutine(PerformUppercut(entity, randomX));

			CleanUpPatternObjects();
			patternCount++;
			yield return new WaitForSeconds(0.5f);
		}

		isPatternRunning = false;
	}

	private IEnumerator PerformUppercut(OneStage entity, float randomX)
	{
		// 1. Ready: 올라가며 3번 블링크 (1초) → 약간 하강 (0.1초)
		enemyAnimator.Play("Ready", -1, 0f);
		Debug.Log("Ready 재생 중");
		yield return StartCoroutine(MoveUp(randomX, 0.5f));
		yield return new WaitForSeconds(0.3f);
		yield return StartCoroutine(SlightDescend(randomX, 0.1f));

		// 2. UpperCut: 점프 (0.1초) → 박스 생성 (1.3초, 첫 박스 사라짐 후 하강) → 하강 (0.3초)
		enemyAnimator.Play("UpperCut", -1, 0f);
		Debug.Log("UpperCut 재생 중");
		yield return StartCoroutine(JumpAndDescend(entity, randomX, 0.4f));
	}

	private IEnumerator MoveUp(float randomX, float duration)
	{
		Vector3 startPos = new Vector3(randomX, -6f, 0);
		Vector3 endPos = new Vector3(randomX, -5f, 0); // Ready 위치
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			enemyInstance.transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}

		enemyInstance.transform.position = endPos;
	}

	private IEnumerator SlightDescend(float randomX, float duration)
	{
		Vector3 startPos = new Vector3(randomX, -5f, 0); // Ready 위치
		Vector3 endPos = new Vector3(randomX, -5.5f, 0);   // 약간 하강
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			enemyInstance.transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}

		enemyInstance.transform.position = endPos;
	}

	private IEnumerator JumpAndDescend(OneStage entity, float randomX, float duration)
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
			enemyInstance.transform.position = Vector3.Lerp(startPos, peakPos, t);
			yield return null;
		}
		enemyInstance.transform.position = peakPos;

		// 박스 생성 시작 (첫 박스가 사라질 때까지 대기 후 하강)
		StartCoroutine(GenerateBoxes(entity, randomX));
		yield return new WaitForSeconds(0.15f); // 첫 박스 사라짐 타이밍 (0.1초 성장 + 0.05초 대기)

		// 하강 (0.3초)
		float descendElapsed = 0f;
		while (descendElapsed < descendDuration)
		{
			descendElapsed += Time.deltaTime;
			float t = descendElapsed / descendDuration;
			enemyInstance.transform.position = Vector3.Lerp(peakPos, endPos, t);
			yield return null;
		}

		enemyInstance.transform.position = endPos;

		// 박스 생성이 끝날 때까지 대기 (나머지 1.15초)
		yield return new WaitForSeconds(1.3f - 0.15f);
	}

	private IEnumerator GenerateBoxes(OneStage entity, float baseX)
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
			box.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
			yield return null;
		}
	}

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject boxPrefab;

	private GameObject enemyInstance;
	private GameObject[] boxes = new GameObject[8];
	private SpriteRenderer enemyRenderer;
	private Animator enemyAnimator;
	private bool isPatternRunning = false;
	private int patternCount = 0;
	private const int maxPatterns = 14;
	private const float screenHeight = 10f;
}