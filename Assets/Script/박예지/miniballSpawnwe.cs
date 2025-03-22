using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spritePrefab1;   // ù ��° �� ������
    public GameObject spritePrefab2;   // �� ��° �� ������
    public GameObject spritePrefab3;   // �� ��° �� ������
    public Transform spawnPoint1;      // ù ��° ���� ���� ��ġ
    public Transform spawnPoint2;      // �� ��° ���� ���� ��ġ
    public Transform spawnPoint3;      // �� ��° ���� ���� ��ġ
    public float spawnInterval = 3f;   // ���� ����
    public float spawnDuration = 1f;   // ���� ���� �ð� (1��)

    private void Start()
    {
        // 1�� ���� ������ �����ϰ�, �� �� ���ߵ��� ����
        InvokeRepeating(nameof(SpawnSprites), 0f, spawnInterval);
        Invoke(nameof(StopSpawning), spawnDuration);  // spawnDuration (1��) �Ŀ� StopSpawning ȣ��
    }

    void SpawnSprites()
    {
        // �� ���� ������ ���� ��ġ���� ����
        Instantiate(spritePrefab1, spawnPoint1.position, Quaternion.identity);
        Instantiate(spritePrefab2, spawnPoint2.position, Quaternion.identity);
        Instantiate(spritePrefab3, spawnPoint3.position, Quaternion.identity);
    }

    void StopSpawning()
    {
        // ������ ���߱� ���� InvokeRepeating�� ���
        CancelInvoke(nameof(SpawnSprites));
    }
}
