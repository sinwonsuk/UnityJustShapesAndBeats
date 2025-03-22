using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CircleEffect : MonoBehaviour
{
    public GameObject newObjectPrefab; // ������ ������Ʈ ������
    public float targetScale = 2f;     // ���� ũ��
    public float scaleDuration = 2f;   // ũ�� Ŀ���� �ð�
    public float replaceDelay = 4f;    // ���� �ð� (4�� ��)

    void Start()
    {
        Invoke(nameof(ReplaceObject), replaceDelay); // 4�� �� ������Ʈ ����
    }

    void ReplaceObject()
    {
        if (newObjectPrefab != null)
        {
            GameObject newObj = Instantiate(newObjectPrefab, transform.position, Quaternion.identity);

            // �� ������Ʈ�� ScaleUp ��ũ��Ʈ �߰� �� ũ�� ���� ����
            ScaleUp scaler = newObj.AddComponent<ScaleUp>();
            scaler.targetScale = targetScale;
            scaler.duration = scaleDuration;
        }

        Destroy(gameObject); // ���� ������Ʈ ����
    }
}

// ũ�⸦ �ε巴�� Ű��� ���� ��ũ��Ʈ
public class ScaleUp : MonoBehaviour
{
    public float targetScale = 2f;
    public float duration = 2f;

    void Start()
    {
        StartCoroutine(ScaleOverTime(duration));
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(targetScale, targetScale, 1);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale; // ��Ȯ�� ��ǥ ũ��� ����
        Destroy(gameObject);
    }
}
