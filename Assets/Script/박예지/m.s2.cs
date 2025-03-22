using UnityEngine;
using System.Collections;

public class Spawner2 : MonoBehaviour
{
    public GameObject spritePrefab1;   // 첫 번째 공 프리팹
    public GameObject spritePrefab2;   // 두 번째 공 프리팹
    public GameObject spritePrefab3;   // 세 번째 공 프리팹

    public Vector3 spawnPosition1;      // 첫 번째 공의 스폰 위치 (직접 조정)
    public Vector3 spawnPosition2;      // 두 번째 공의 스폰 위치 (직접 조정)
    public Vector3 spawnPosition3;      // 세 번째 공의 스폰 위치 (직접 조정)

    public float spawnInterval = 3f;   // 스폰 간격
    public float spawnDuration = 1f;   // 스폰 지속 시간 (1초)
    public float delayBeforeStart = 4f; // 스폰 시작 전 대기 시간 (4초)

    private void Start()
    {
        // 4초 후에 스폰 시작
        Invoke(nameof(StartSpawning), delayBeforeStart);
    }

    void StartSpawning()
    {
        // 4초 후 스폰 시작
        InvokeRepeating(nameof(SpawnSprites), 0f, spawnInterval);
        Invoke(nameof(StopSpawning), spawnDuration); // 1초 후 스폰 멈추기
    }

    void SpawnSprites()
    {
        // 각 공을 지정된 위치에서 스폰 (spawnPosition으로 위치 설정)
        Instantiate(spritePrefab1, spawnPosition1, Quaternion.identity);
        Instantiate(spritePrefab2, spawnPosition2, Quaternion.identity);
        Instantiate(spritePrefab3, spawnPosition3, Quaternion.identity);
    }

    void StopSpawning()
    {
        // 스폰을 멈추기 위해 InvokeRepeating을 취소
        CancelInvoke(nameof(SpawnSprites));
    }
}
