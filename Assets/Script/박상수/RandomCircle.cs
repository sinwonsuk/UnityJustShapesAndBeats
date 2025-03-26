using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject parentPrefab;        // 부모 프리팹 (Inspector에 할당)
    [SerializeField]
    private Vector2 spawnAreaMin = new Vector2(-10, -10);
    [SerializeField]
    private Vector2 spawnAreaMax = new Vector2(10, 10);
    [SerializeField]
    private int maxParents = 6;             // 총 생성할 부모 수

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

            // 부모 오브젝트가 삭제될 때까지 대기
            yield return new WaitUntil(() => parentObj == null);

            currentCycle++;
            // 다음 부모를 즉시 생성 (spawnDelay 제거)
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