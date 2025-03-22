using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

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



