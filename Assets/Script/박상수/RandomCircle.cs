using UnityEngine;

public class RandomCircle : MonoBehaviour
{
    public GameObject RCircle; // ������ ��ó ������
    public Vector2 randomPositionMin; // ���� ��ġ �ּҰ�
    public Vector2 randomPositionMax; // ���� ��ġ �ִ밪

    void Start()
    {
        SpawnLauncher(); // ó�� �� ���� ����
    }

    void SpawnLauncher()
    {
        int count = 1;
        while (count <= 6)
        {
            float randomX = Random.Range(randomPositionMin.x, randomPositionMax.x);
            float randomY = Random.Range(randomPositionMin.y, randomPositionMax.y);


            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);


            Instantiate(RCircle, spawnPosition, Quaternion.identity);
        }
    }
}
