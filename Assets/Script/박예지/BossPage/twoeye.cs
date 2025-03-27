using UnityEngine;

public class TwoEye : MonoBehaviour
{
    public float speed = 2f;             // Ÿ�� ������ ���� �ӵ�
    public float horizontalRadius = 3f;  // ���� ������
    public float verticalRadius = 5f;    // ���� ������
    public float bobbingSpeed = 1f;      // ���Ʒ��� �����̴� �ӵ�
    public float bobbingAmount = 0.5f;   // ���Ʒ��� �����̴� ����
    public Vector2 center;               // Ÿ���� �߽� (Inspector���� ���� ����)

    private float angle = 0f;            // ����

    void Start()
    {
        if (center == Vector2.zero)
            center = transform.localPosition; // �⺻������ ���� ��ġ�� �߽����� ����
    }

    void Update()
    {
        //// Ÿ�� ��θ� ���� �����̱�
        angle += Time.deltaTime * speed;

        // Ÿ�� ��ο��� X, Y ���
        float x = center.x + Mathf.Cos(angle) * horizontalRadius;
        float y = center.y + Mathf.Sin(angle) * verticalRadius;

        // ���Ʒ��� �����̴� ȿ�� �߰�
        y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // ��ġ ������Ʈ
        transform.localPosition = new Vector2(x, y);
    }
}
