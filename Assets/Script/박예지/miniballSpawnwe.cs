using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spritePrefab1;   // 첫 번째 공 프리팹
    public GameObject spritePrefab2;   // 두 번째 공 프리팹
    public GameObject spritePrefab3;   // 세 번째 공 프리팹
    public Transform spawnPoint1;      // 첫 번째 공의 스폰 위치
    public Transform spawnPoint2;      // 두 번째 공의 스폰 위치
    public Transform spawnPoint3;      // 세 번째 공의 스폰 위치
    public float spawnInterval = 3f;   // 스폰 간격
    public float spawnDuration = 1f;   // 스폰 지속 시간 (1초)

    private void Start()
    {
        // 1초 동안 스폰을 시작하고, 그 후 멈추도록 설정
        InvokeRepeating(nameof(SpawnSprites), 0f, spawnInterval);
        Invoke(nameof(StopSpawning), spawnDuration);  // spawnDuration (1초) 후에 StopSpawning 호출
    }

    void SpawnSprites()
    {
        // 각 공을 지정된 스폰 위치에서 스폰
        Instantiate(spritePrefab1, spawnPoint1.position, Quaternion.identity);
        Instantiate(spritePrefab2, spawnPoint2.position, Quaternion.identity);
        Instantiate(spritePrefab3, spawnPoint3.position, Quaternion.identity);
    }

    void StopSpawning()
    {
        // 스폰을 멈추기 위해 InvokeRepeating을 취소
        CancelInvoke(nameof(SpawnSprites));
    }
}
