using System.Collections;
using UnityEngine;

public class RandomCircle : MonoBehaviour
{
    public GameObject parentPrefab;  // ������ ��ġ�� ������ �θ� ������Ʈ ������
    public GameObject childPrefab;   // �θ� ������Ʈ�� �ڽ����� ������ ������Ʈ ������
    public int spawnCount = 6;       // �� ���� Ƚ��
    public float spawnInterval = 1f; // ���� ���� (��)
    public Vector2 spawnAreaMin = new Vector2(-5, -5); // �ּ� ��ǥ
    public Vector2 spawnAreaMax = new Vector2(5, 5);   // �ִ� ��ǥ
    public float targetScale = 2f;   // �ڽ� ������Ʈ�� ���� ũ��
    public float scaleDuration = 2f; // ũ�� Ŀ���� �ð�

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // ���� ��ġ ���
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            // �θ� ������Ʈ ����
            GameObject parentObj = Instantiate(parentPrefab, randomPosition, Quaternion.identity);

            // �θ��� �ڽ� ������Ʈ ����
            GameObject childObj = Instantiate(childPrefab, parentObj.transform);
            childObj.transform.localPosition = Vector3.zero; // �θ��� �߾ӿ� ��ġ

            // �ڽ� ������Ʈ ũ�� ���� ��� �߰�
            ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
            scaleScript.targetScale = targetScale;
            scaleScript.duration = scaleDuration;

            yield return new WaitForSeconds(spawnInterval); // ���� �������� ����
        }
    }
}
