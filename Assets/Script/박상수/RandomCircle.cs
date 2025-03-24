using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircle : MonoBehaviour
{
    public GameObject parentPrefab;        // 부모 오브젝트 프리팹 (Inspector에 할당)
    public Vector2 spawnAreaMin = new Vector2(-10, -10);
    public Vector2 spawnAreaMax = new Vector2(10, 10);
    public int maxParents = 6;             // 총 실행할 부모 사이클 수
    public float spawnDelay = 0.5f;        // 사이클 간 대기 시간

    private int currentCycle = 0;

    void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    IEnumerator SpawnCycle()
    {
        while (currentCycle < maxParents)
        {
            // 새 부모 오브젝트 생성
            GameObject parentObj = SpawnNewParent();
            ParentController pc = parentObj.GetComponent<ParentController>();

            if (pc != null)
            {
                // 부모의 작업 완료(isFinished == true)까지 대기
                yield return new WaitUntil(() => pc.isFinished);

                Debug.Log($"[RandomSpawner] 삭제 시도: {parentObj.name}");
                Destroy(parentObj);

                // 부모 삭제 후 잠깐 대기하여 완전히 삭제되도록 함
                yield return new WaitForSeconds(0.1f);
            }

            currentCycle++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    GameObject SpawnNewParent()
    {
        if (parentPrefab == null)
        {
            Debug.LogError("ParentPrefab not assigned.");
            return null;
        }

        Vector2 pos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject parentObj = Instantiate(parentPrefab, pos, Quaternion.identity);
        parentObj.name = "Parent_" + currentCycle;
        Debug.Log($"[RandomSpawner] 생성됨: {parentObj.name}");
        return parentObj;
    }
}