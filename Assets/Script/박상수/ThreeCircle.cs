using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject parentPrefab;       // Inspector에 부모 프리팹 할당
    [SerializeField]
    private Vector2 spawnAreaMin = new Vector2(-10, -10);
    [SerializeField]
    private Vector2 spawnAreaMax = new Vector2(10, 10);
    [SerializeField]
    private int totalSets = 3;             // 동시에 생성할 부모–자식 세트 수 (3세트)

    void Start()
    {
        // 동시에 부모를 모두 생성 (for 루프 안에 딜레이 없이)
        for (int i = 0; i < totalSets; i++)
        {
            SpawnNewParent(i);
        }
        Debug.Log("[RandomSpawner] 모든 부모 오브젝트가 동시에 생성되었습니다.");
    }

    GameObject SpawnNewParent(int index)
    {
        if (parentPrefab == null)
        {
            Debug.LogError("ParentPrefab이 할당되지 않았습니다.");
            return null;
        }

        Vector2 pos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        GameObject parentObj = Instantiate(parentPrefab, pos, Quaternion.identity);
        parentObj.name = "Parent_" + index;
        Debug.Log($"[RandomSpawner] 생성됨: {parentObj.name}");
        return parentObj;
    }
}