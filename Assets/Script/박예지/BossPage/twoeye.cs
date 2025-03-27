using UnityEngine;

public class twoeye : MonoBehaviour
{
    public float speed = 2f;             // 타원 주위를 도는 속도
    public float horizontalRadius = 3f;  // 수평 반지름
    public float verticalRadius = 5f;    // 수직 반지름
    public float bobbingSpeed = 1f;      // 위아래로 끄덕이는 속도
    public float bobbingAmount = 0.5f;   // 위아래로 끄덕이는 범위

    private Vector2 center;              // 타원의 중심 위치
    private float angle = 0f;            // 각도
    private float originalY;             // 원래 Y 값 (끄덕임을 적용할 기준)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        center = transform.position;  // 타원의 중심을 현재 위치로 설정
        originalY = transform.position.y; // 원래 Y 값 저장
    }

    // Update is called once per frame
    void Update()
    {
        // 타원 경로를 따라 움직이기
        angle += Time.deltaTime * speed;

        // 타원 경로에서 X, Y 계산
        float x = center.x + Mathf.Cos(angle) * horizontalRadius;
        float y = center.y + Mathf.Sin(angle) * verticalRadius;

        // 위아래로 끄덕이는 효과를 추가 (Sine 파형으로)
        y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // 위치 업데이트
        transform.position = new Vector2(x, y);
    }
}
