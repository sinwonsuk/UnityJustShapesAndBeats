using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject spritePrefab1;
    public GameObject spritePrefab2;
    public GameObject spritePrefab3;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public float spawnInterval = 0.2f;   // �� ���� ���� (0.2�ʸ��� ����)
    public float spawnDuration = 1f;     // �� ���� ���� �ð� (�� 1�� ���� ����)

    private void Start()
    {
        // 2�� �Ŀ� �� ���� ����
        Invoke(nameof(StartSpawning), 1f);
    }

    void StartSpawning()
    {
        // ù ��° ���� 2�� �Ŀ� �����ϰ�, ���� 0.2�ʸ��� �ݺ������� ����
        SpawnBalls();  // ù ��° �� �ٷ� ����
        InvokeRepeating(nameof(SpawnBalls), spawnInterval, spawnInterval);  // ���� ���� �ݺ������� ����

        // spawnDuration(1��) �Ŀ� ���� ���߱�
        Invoke(nameof(StopSpawning), spawnDuration);
    }

    void SpawnBalls()
    {
        Debug.Log("�� ����!");
        Instantiate(spritePrefab1, spawnPoint1.position, Quaternion.identity);
        Instantiate(spritePrefab2, spawnPoint2.position, Quaternion.identity);
        Instantiate(spritePrefab3, spawnPoint3.position, Quaternion.identity);
    }

    void StopSpawning()
    {
        Debug.Log("���� ����!");
        CancelInvoke(nameof(SpawnBalls));  // �ݺ� ȣ�� ����
    }
}
