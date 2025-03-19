using UnityEngine;

public class BulletRandom : MonoBehaviour
{
    public GameObject BulletPrefab; // 생성할 런처 프리팹
    public Vector2 randomPositionMin; // 랜덤 위치 최소값
    public Vector2 randomPositionMax; // 랜덤 위치 최대값

    void Start()
    {
        SpawnLauncher(); // 처음 한 번만 실행
    }

    void SpawnLauncher()
    {
        float randomX = Random.Range(randomPositionMin.x, randomPositionMax.x);
        float randomY = Random.Range(randomPositionMin.y, randomPositionMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
    }
}
