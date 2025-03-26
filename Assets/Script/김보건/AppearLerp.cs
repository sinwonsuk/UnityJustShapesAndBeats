using UnityEngine;

public class AppearLerp : MonoBehaviour
{
    [SerializeField] private GameObject appearCircle;
    [SerializeField] private Color targetColor = Color.white; // ���� �� (����� �Դٰ���)
    [SerializeField] private float speed = 1f; // �� ��ȯ �ӵ�

    private Color originalColor;
    private SpriteRenderer movingRenderer;


    private void Start()
    {
        movingRenderer = appearCircle.GetComponent<SpriteRenderer>();
        originalColor = movingRenderer.color;
    }

    private void Update()
    {
        if (movingRenderer != null)
        {
            AppearCircleLerp();
        }
    }

    void AppearCircleLerp()
    {
        // PingPong���� 0~1 �ݺ�
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // ���� ����
        movingRenderer.color = Color.Lerp(originalColor, targetColor, t);
    }
}
