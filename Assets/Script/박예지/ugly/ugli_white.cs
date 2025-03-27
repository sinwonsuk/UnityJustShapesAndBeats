using UnityEngine;



public class SpriteAppear : MonoBehaviour
{
    public GameObject Bbullet;
    [Header("스폰 범위 설정")]
    public Vector2 minSpawnRange;   // 스폰 최소 범위
    public Vector2 maxSpawnRange;   // 스폰 최대 범위

    [Header("목표 위치 범위 설정")]
    public Vector2 minTargetRange;  // 목표 위치 최소 범위
    public Vector2 maxTargetRange;  // 목표 위치 최대 범위

    public float speed = 5f;         // 이동 속도
    public float rotationSpeed = 100f; // 회전 속도
    public float growthSpeed = 1f;   // 크기 증가 속도
    public Vector3 targetScale = new Vector3(0.8f, 0.8f, 0.8f);  // 최종 목표 크기
    public Vector3 initialScale = new Vector3(0, 0, 0); // 초기 크기

    [Header("깜박임 설정")]
    public Color blinkColor = Color.red;    // 깜박일 색
    public float blinkInterval = 0.3f;      // 깜박이는 간격 (초)

    private Vector2 targetPosition;
    private bool isScaling;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isBlinkOn = false;

    void Start()
    {
        if (Bbullet == null)
        {
            Debug.LogError("Bbullet이 null입니다! Inspector에서 총알 프리팹을 할당해야 합니다.");
            return;
        }

        // 나머지 코드
    }
        void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void OnEnable()
    {
        // 매번 켜질 때마다 랜덤 위치 및 초기화
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
            spriteRenderer.color = originalColor;  // 초기화
            CancelInvoke(nameof(Blink));           // 혹시 몰라 취소
            InvokeRepeating(nameof(Blink), 0, blinkInterval);  // 깜박이기 시작
        }

        Debug.Log($"Spawned at: {randomSpawnPos}, Target: {targetPosition}");
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
                for (int i = 0; i < 8; i++)
                {
                    GameObject go = Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 45 * i));
                }
                
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
