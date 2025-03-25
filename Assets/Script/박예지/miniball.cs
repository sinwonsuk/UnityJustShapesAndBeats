using UnityEngine;

public class SpriteExit : MonoBehaviour
{
    public float delayTime = 2f;   // 기다릴 시간
    public float speed = 10f;      // 빠져나가는 속도
    private bool isExiting = false;
    private SpriteRenderer spriteRenderer;  // SpriteRenderer 컴포넌트

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer.enabled = false;  // 스프라이트 비활성화 (보이지 않게)
        Invoke(nameof(StartExit), delayTime);  // 2초 후에 StartExit 호출
    }

    void Update()
    {
        if (isExiting)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // 너무 멀어지면 제거
            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
            }
        }
    }

    void StartExit()  // 이름을 StartExit로 변경
    {
        spriteRenderer.enabled = true;  // 스프라이트 활성화 (보이게)
        isExiting = true;  // 스프라이트 이동 시작
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
