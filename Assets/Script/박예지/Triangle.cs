using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour
{
    public float speed = 5f;             // 초기 속도
    public float deceleration = 1f;      // 감속량
    public float shakeAmount = 0.02f;    // 진동 높이
    public int shakeCount = 3;           // 진동 횟수
    public float shakeSpeed = 0.2f;      // 진동 속도
    public Color shakeColor = Color.white; // 진동 중 색상
    private Color originalColor;         // 원래 색상
    public Color startColor = new Color(1f, 0.4f, 0.7f); // 기본은 분홍색
    public float delayBeforeShake = 2f;  // 진동이 시작되기 전에 대기할 시간 (초)

    private bool isShaking = false;
    private Renderer rend; // Renderer 컴포넌트

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = startColor;  // 시작 색상 적용
        originalColor = startColor;        // 원래 색상 저장 (진동 끝나면 돌아갈 색)
    }

    void Update()
    {
        if (speed > 0)
        {
            // 위로 이동
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // 속도 감소
            speed -= deceleration * Time.deltaTime * 2;

            if (speed < 0)
            {
                speed = 0;
                StartCoroutine(WaitBeforeShake());
            }
        }
    }

    // 진동 시작 전 대기 시간 추가
    IEnumerator WaitBeforeShake()
    {
        yield return new WaitForSeconds(delayBeforeShake);  // delayBeforeShake 시간만큼 대기
        StartCoroutine(Shake());  // 대기 후 Shake 시작
    }

    IEnumerator Shake()
    {
        if (isShaking) yield break; // 중복 방지
        isShaking = true;

        Vector3 originalPos = transform.position;

        // 진동 시작 전에 색 변경
        StartCoroutine(ChangeColor(shakeColor));

        for (int i = 0; i < shakeCount; i++)
        {
            // 위로
            yield return MoveTo(originalPos + Vector3.left * shakeAmount);
            // 아래로
            yield return MoveTo(originalPos - Vector3.left * shakeAmount);
        }

        // 마지막으로 원래 위치로
        transform.position = originalPos;

        // 진동 끝난 후 색 원상 복귀
        StartCoroutine(ChangeColor(originalColor));

        // 진동 끝난 후 왼쪽으로 이동 시작
        speed = 15f; // 다시 이동 속도를 설정
    }

    IEnumerator MoveTo(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, shakeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ChangeColor(Color targetColor)
    {
        Color startColor = rend.material.color;
        float elapsedTime = 0f;
        float duration = 0.5f; // 색이 바뀌는 데 걸리는 시간

        while (elapsedTime < duration)
        {
            rend.material.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rend.material.color = targetColor; // 색이 완전히 변경되었을 때
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
