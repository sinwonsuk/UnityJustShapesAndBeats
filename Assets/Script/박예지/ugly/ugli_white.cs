using UnityEngine;



public class SpriteAppear : MonoBehaviour
{
    public GameObject Bbullet;
    [Header("���� ���� ����")]
    public Vector2 minSpawnRange;   // ���� �ּ� ����
    public Vector2 maxSpawnRange;   // ���� �ִ� ����

    [Header("��ǥ ��ġ ���� ����")]
    public Vector2 minTargetRange;  // ��ǥ ��ġ �ּ� ����
    public Vector2 maxTargetRange;  // ��ǥ ��ġ �ִ� ����

    public float speed = 5f;         // �̵� �ӵ�
    public float rotationSpeed = 100f; // ȸ�� �ӵ�
    public float growthSpeed = 1f;   // ũ�� ���� �ӵ�
    public Vector3 targetScale = new Vector3(0.8f, 0.8f, 0.8f);  // ���� ��ǥ ũ��
    public Vector3 initialScale = new Vector3(0, 0, 0); // �ʱ� ũ��

    [Header("������ ����")]
    public Color blinkColor = Color.red;    // ������ ��
    public float blinkInterval = 0.3f;      // �����̴� ���� (��)

    private Vector2 targetPosition;
    private bool isScaling;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinkOn = false;

    void Start()
    {
        if (Bbullet == null)
        {
            Debug.LogError("Bbullet�� null�Դϴ�! Inspector���� �Ѿ� �������� �Ҵ��ؾ� �մϴ�.");
            return;
        }

        // ������ �ڵ�
    }
        void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void OnEnable()
    {
        // �Ź� ���� ������ ���� ��ġ �� �ʱ�ȭ
        Vector2 randomSpawnPos = new Vector2(
            Random.Range(minSpawnRange.x, maxSpawnRange.x),
            Random.Range(minSpawnRange.y, maxSpawnRange.y)
        );
        transform.position = randomSpawnPos;
        transform.localScale = initialScale;

        targetPosition = new Vector2(
            Random.Range(minTargetRange.x, maxTargetRange.x),
            Random.Range(minTargetRange.y, maxTargetRange.y)
        );

        isScaling = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;  // �ʱ�ȭ
            CancelInvoke(nameof(Blink));           // Ȥ�� ���� ���
            InvokeRepeating(nameof(Blink), 0, blinkInterval);  // �����̱� ����
        }

        Debug.Log($"Spawned at: {randomSpawnPos}, Target: {targetPosition}");
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
                for (int i = 0; i < 8; i++)
                {
                    GameObject go = Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 45 * i));
                }
                
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
