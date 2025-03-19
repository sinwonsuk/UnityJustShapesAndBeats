using System.Collections;
using UnityEngine;

public class BulletLuncert : MonoBehaviour
{
    public float fadeDuration = 2f;        // 페이드 인/아웃 시간
    public float visibleDuration = 21f;    // 맵이 보이는 시간
    public float reappearDelay = 1.5f;     // 맵이 사라진 후 다시 나타나는 시간
    public float bounceSpeed = 2f;         // 바운스 속도
    public float bounceHeight = 0.5f;      // 바운스 높이 (짧게)
    public float stopTime = 32f;           // 바운스 정지 시간
    public float bounceDelay = 1f;         // 바운스 시작 전 대기 시간

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float startY;
    private bool isBouncing = false;  // 바운스 활성화 여부

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 없습니다! 'walll' 오브젝트에 SpriteRenderer를 추가하세요.");
            return;
        }

        originalColor = spriteRenderer.color;
        startY = transform.position.y;

        StartCoroutine(MapCycle());
    }

    IEnumerator MapCycle()
    {
        while (true)
        {
            yield return FadeIn();  // 서서히 나타남
            yield return new WaitForSeconds(bounceDelay); // 바운스 1초 딜레이 추가
            isBouncing = true; // 바운스 시작
            yield return new WaitForSeconds(visibleDuration); // 21초 동안 유지
            yield return FadeOut(); // 서서히 사라짐
            yield return new WaitForSeconds(reappearDelay); // 1.5초 후 다시 등장
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, originalColor.a, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        StartCoroutine(StopBouncingAfterTime(stopTime)); // 32초 후 바운스 멈추기 시작
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }

    IEnumerator StopBouncingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isBouncing = false; // 32초 후 바운스 정지
    }

    void Update()
    {
        if (isBouncing)
        {
            // 아래에서 위로 바운스 (짧게)
            float newY = startY + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}