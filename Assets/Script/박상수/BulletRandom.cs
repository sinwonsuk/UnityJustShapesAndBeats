using UnityEngine;

public class BulletRandom : MonoBehaviour
{
    public GameObject BulletPrefab; // ������ ��ó ������
    public Vector2 randomPositionMin; // ���� ��ġ �ּҰ�
    public Vector2 randomPositionMax; // ���� ��ġ �ִ밪

    void Start()
    {
        SpawnLauncher(); // ó�� �� ���� ����
    }

    void SpawnLauncher()
    {
        float randomX = Random.Range(randomPositionMin.x, randomPositionMax.x);
        float randomY = Random.Range(randomPositionMin.y, randomPositionMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
    }
}
