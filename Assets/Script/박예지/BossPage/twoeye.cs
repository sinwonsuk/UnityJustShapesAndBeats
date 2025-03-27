using UnityEngine;

public class twoeye : MonoBehaviour
{
    public float speed = 2f;             // Ÿ�� ������ ���� �ӵ�
    public float horizontalRadius = 3f;  // ���� ������
    public float verticalRadius = 5f;    // ���� ������
    public float bobbingSpeed = 1f;      // ���Ʒ��� �����̴� �ӵ�
    public float bobbingAmount = 0.5f;   // ���Ʒ��� �����̴� ����

    private Vector2 center;              // Ÿ���� �߽� ��ġ
    private float angle = 0f;            // ����
    private float originalY;             // ���� Y �� (�������� ������ ����)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        center = transform.position;  // Ÿ���� �߽��� ���� ��ġ�� ����
        originalY = transform.position.y; // ���� Y �� ����
    }

    // Update is called once per frame
    void Update()
    {
        // Ÿ�� ��θ� ���� �����̱�
        angle += Time.deltaTime * speed;

        // Ÿ�� ��ο��� X, Y ���
        float x = center.x + Mathf.Cos(angle) * horizontalRadius;
        float y = center.y + Mathf.Sin(angle) * verticalRadius;

        // ���Ʒ��� �����̴� ȿ���� �߰� (Sine ��������)
        y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // ��ġ ������Ʈ
        transform.position = new Vector2(x, y);
    }
}
