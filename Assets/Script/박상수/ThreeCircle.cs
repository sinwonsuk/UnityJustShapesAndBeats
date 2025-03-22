using UnityEngine;

public class ThreeCircle : MonoBehaviour
{
    public GameObject parentPrefab;  // �θ� ������Ʈ ������
    public GameObject childPrefab;   // ũ�� ������ �ڽ� ������Ʈ ������
    public Vector2 spawnAreaMin = new Vector2(-5, -5); // �ּ� ��ǥ
    public Vector2 spawnAreaMax = new Vector2(5, 5);   // �ִ� ��ǥ
    public float targetScale = 2f;   // �ڽ� ������Ʈ�� ���� ũ��
    public float scaleDuration = 2f; // ũ�� Ŀ���� �ð�

    void Start()
    {
        SpawnThreeObjects(); // ���� ���� �� �� �� ����
    }

    void SpawnThreeObjects()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        for (int i = 0; i < 3; i++)
        {
            Vector2 offset = new Vector2(i * 2, 0); // 3���� ������Ʈ�� ������ ��ġ
            Vector2 spawnPosition = randomPosition + offset;

            // �θ� ������Ʈ ����
            GameObject parentObj = Instantiate(parentPrefab, spawnPosition, Quaternion.identity);

            // �θ��� �ڽ� ������Ʈ ����
            GameObject childObj = Instantiate(childPrefab, parentObj.transform);
            childObj.transform.localPosition = Vector3.zero; // �θ��� �߽ɿ� ��ġ

            // �ڽ� ������Ʈ ũ�� ���� ��� �߰�
            ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
            scaleScript.targetScale = targetScale;
            scaleScript.duration = scaleDuration;
        }
    }
}
