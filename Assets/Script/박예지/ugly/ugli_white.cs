using UnityEngine;

public class SpriteAppear : MonoBehaviour
{
    public Vector2 startPosition;  // 시작 위치
    public Vector2 targetPosition; // 목표 위치
    public float speed = 5f;       // 이동 속도
    public float rotationSpeed = 100f; // 회전 속도
    public float growthSpeed = 1f;   // 크기 증가 속도
    public Vector3 targetScale = new Vector3(0.8f,0.8f,0.8f);  // 최종 목표 크기
    public Vector3 initialScale = new Vector3(0, 0, 0); // 초기 크기

    [Header("깜박임 설정")]
    public Color blinkColor = Color.red;    // 깜박일 색
    public float blinkInterval = 0.3f;      // 깜박이는 간격 (초)

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
            InvokeRepeating(nameof(Blink), 0, blinkInterval);  // 깜박이기 시작
        }
    }

    void Update()
    {
        // 회전
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // 이동
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 크기 증가
        if (isScaling && transform.localScale.x < targetScale.x)
        {
            transform.localScale += new Vector3(growthSpeed, growthSpeed, 0) * Time.deltaTime;

            if (transform.localScale.x >= targetScale.x)
            {
                transform.localScale = targetScale;
                isScaling = false;

                CancelInvoke(nameof(Blink)); // 깜박이기 멈추기
                gameObject.SetActive(false); // 사라지기
            }
        }
    }

    // 색상 깜박이기
    void Blink()
    {
        if (spriteRenderer == null) return;

        isBlinkOn = !isBlinkOn;
        spriteRenderer.color = isBlinkOn ? blinkColor : originalColor;
    }
}
