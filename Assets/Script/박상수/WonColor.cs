using System.Collections;
using UnityEngine;

public class WonColor : MonoBehaviour
{
    // 목표 색상 (여기서는 핑크 계열)
    [SerializeField] private Color targetColor = new Color(255f / 255f, 32f / 255f, 112f / 255f);
    // 깜빡임 간격 (PingPong의 주기를 조절: 값이 작을수록 빠르게 깜빡임)
    [SerializeField] private float blinkInterval = 0.5f;

    // SpriteRenderer를 저장할 변수
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 현재 게임 오브젝트의 SpriteRenderer 컴포넌트를 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 커스텀 러프 함수를 호출하여 깜빡임 효과를 적용합니다.
        ColorLerpStarting(gameObject);
    }

    // 전달받은 게임 오브젝트의 색상을 흰색과 targetColor 사이에서 깜빡이도록 변경합니다.
    void ColorLerpStarting(GameObject box)
    {
        // box의 SpriteRenderer 컴포넌트를 가져옵니다.
        SpriteRenderer movingRenderer = box.GetComponent<SpriteRenderer>();
        // Mathf.PingPong를 사용하여 Time.time 값에 따라 0에서 blinkInterval 사이의 값을 생성
        // 이를 blinkInterval로 나누면 0~1 사이의 t 값을 얻습니다.
        float t = Mathf.PingPong(Time.time, blinkInterval) / blinkInterval;
        // Color.Lerp를 사용하여 흰색과 targetColor 사이의 색을 t 값에 따라 보간하여 설정합니다.
        movingRenderer.color = Color.Lerp(Color.white, targetColor, t);
    }
}