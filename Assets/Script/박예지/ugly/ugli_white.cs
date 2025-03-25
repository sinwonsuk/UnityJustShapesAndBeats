using UnityEngine;

public class SpriteAppear : MonoBehaviour
{
    public Vector2 startPosition;  // ���� ��ġ
    public Vector2 targetPosition; // ��ǥ ��ġ
    public float speed = 5f;       // �̵� �ӵ�
    public float rotationSpeed = 100f; // ȸ�� �ӵ�
    public float growthSpeed = 1f;   // ũ�� ���� �ӵ�
    public Vector3 targetScale = new Vector3(0.8f,0.8f,0.8f);  // ���� ��ǥ ũ��
    public Vector3 initialScale = new Vector3(0, 0, 0); // �ʱ� ũ��

    [Header("������ ����")]
    public Color blinkColor = Color.red;    // ������ ��
    public float blinkInterval = 0.3f;      // �����̴� ���� (��)

    private bool isScaling = true;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinkOn = false;

    void Start()
    {
        transform.position = startPosition;
        transform.localScale = initialScale;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            InvokeRepeating(nameof(Blink), 0, blinkInterval);  // �����̱� ����
        }
    }

    void Update()
    {
        // ȸ��
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // �̵�
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ũ�� ����
        if (isScaling && transform.localScale.x < targetScale.x)
        {
            transform.localScale += new Vector3(growthSpeed, growthSpeed, 0) * Time.deltaTime;

            if (transform.localScale.x >= targetScale.x)
            {
                transform.localScale = targetScale;
                isScaling = false;

                CancelInvoke(nameof(Blink)); // �����̱� ���߱�
                gameObject.SetActive(false); // �������
            }
        }
    }

    // ���� �����̱�
    void Blink()
    {
        if (spriteRenderer == null) return;

        isBlinkOn = !isBlinkOn;
        spriteRenderer.color = isBlinkOn ? blinkColor : originalColor;
    }
}
