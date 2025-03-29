using UnityEngine;
using System.Collections;

public class ball_spawner2 : MonoBehaviour

{
    public float speed = 2f;             // 타원 주위를 도는 속도
    public float horizontalRadius = 3f;  // 수평 반지름
    public float verticalRadius = 5f;    // 수직 반지름
    public float bobbingSpeed = 1f;      // 위아래로 끄덕이는 속도
    public float bobbingAmount = 0.5f;   // 위아래로 끄덕이는 범위
    public Vector2 center;               // 타원의 중심 (Inspector에서 조정 가능)

    private float angle = 0f;            // 각도
    public GameObject Bbullet;

    void Start()
    {
        StartCoroutine(StartDelay());
        if (center == Vector2.zero)
            center = transform.localPosition;

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
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f); // 2초 대기
        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }

       
    }



}