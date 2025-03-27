using UnityEngine;

public class TwoEye : MonoBehaviour
{
    public float speed = 2f;             // 타원 주위를 도는 속도
    public float horizontalRadius = 3f;  // 수평 반지름
    public float verticalRadius = 5f;    // 수직 반지름
    public float bobbingSpeed = 1f;      // 위아래로 끄덕이는 속도
    public float bobbingAmount = 0.5f;   // 위아래로 끄덕이는 범위
    public Vector2 center;               // 타원의 중심 (Inspector에서 조정 가능)

    private float angle = 0f;            // 각도

    void Start()
    {
        if (center == Vector2.zero)
            center = transform.localPosition; // 기본적으로 현재 위치를 중심으로 설정
    }

    void Update()
    {
        //// 타원 경로를 따라 움직이기
        angle += Time.deltaTime * speed;

        // 타원 경로에서 X, Y 계산
        float x = center.x + Mathf.Cos(angle) * horizontalRadius;
        float y = center.y + Mathf.Sin(angle) * verticalRadius;

        // 위아래로 끄덕이는 효과 추가
        y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // 위치 업데이트
        transform.localPosition = new Vector2(x, y);
    }
}
