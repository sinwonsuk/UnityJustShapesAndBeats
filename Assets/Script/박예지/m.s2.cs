using UnityEngine;
using System.Collections;

public class Spawner2 : MonoBehaviour
{
    public GameObject spritePrefab1;   // ù ��° �� ������
    public GameObject spritePrefab2;   // �� ��° �� ������
    public GameObject spritePrefab3;   // �� ��° �� ������

    public Vector3 spawnPosition1;      // ù ��° ���� ���� ��ġ (���� ����)
    public Vector3 spawnPosition2;      // �� ��° ���� ���� ��ġ (���� ����)
    public Vector3 spawnPosition3;      // �� ��° ���� ���� ��ġ (���� ����)

    public float spawnInterval = 3f;   // ���� ����
    public float spawnDuration = 1f;   // ���� ���� �ð� (1��)
    public float delayBeforeStart = 4f; // ���� ���� �� ��� �ð� (4��)

    private void Start()
    {
        // 4�� �Ŀ� ���� ����
        Invoke(nameof(StartSpawning), delayBeforeStart);
    }

    void StartSpawning()
    {
        // 4�� �� ���� ����
        InvokeRepeating(nameof(SpawnSprites), 0f, spawnInterval);
        Invoke(nameof(StopSpawning), spawnDuration); // 1�� �� ���� ���߱�
    }

    void SpawnSprites()
    {
        // �� ���� ������ ��ġ���� ���� (spawnPosition���� ��ġ ����)
        Instantiate(spritePrefab1, spawnPosition1, Quaternion.identity);
        Instantiate(spritePrefab2, spawnPosition2, Quaternion.identity);
        Instantiate(spritePrefab3, spawnPosition3, Quaternion.identity);
    }

    void StopSpawning()
    {
        // ������ ���߱� ���� InvokeRepeating�� ���
        CancelInvoke(nameof(SpawnSprites));
    }
}
