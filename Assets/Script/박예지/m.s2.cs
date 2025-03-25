using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject spritePrefab1;
    public GameObject spritePrefab2;
    public GameObject spritePrefab3;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public float spawnInterval = 0.2f;   // 공 생성 간격 (0.2초마다 생성)
    public float spawnDuration = 1f;     // 공 생성 지속 시간 (딱 1초 동안 생성)

    private void Start()
    {
        // 2초 후에 공 생성 시작
        Invoke(nameof(StartSpawning), 1f);
    }

    void StartSpawning()
    {
        // 첫 번째 공은 2초 후에 시작하고, 이후 0.2초마다 반복적으로 생성
        SpawnBalls();  // 첫 번째 공 바로 생성
        InvokeRepeating(nameof(SpawnBalls), spawnInterval, spawnInterval);  // 이후 공은 반복적으로 생성

        // spawnDuration(1초) 후에 스폰 멈추기
        Invoke(nameof(StopSpawning), spawnDuration);
    }

    void SpawnBalls()
    {
        Debug.Log("공 생성!");
        Instantiate(spritePrefab1, spawnPoint1.position, Quaternion.identity);
        Instantiate(spritePrefab2, spawnPoint2.position, Quaternion.identity);
        Instantiate(spritePrefab3, spawnPoint3.position, Quaternion.identity);
    }

    void StopSpawning()
    {
        Debug.Log("스폰 멈춤!");
        CancelInvoke(nameof(SpawnBalls));  // 반복 호출 중지
    }
}
