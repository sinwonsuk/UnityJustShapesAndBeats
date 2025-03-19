
using System.Collections;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float fadeDuration = 2f;        // ���̵� ��/�ƿ� �ð�
    public float visibleDuration = 21f;    // ���� ���̴� �ð�
    public float reappearDelay = 1.5f;     // ���� ����� �� �ٽ� ��Ÿ���� �ð�
    public float bounceSpeed = 2f;         // �ٿ �ӵ�
    public float bounceHeight = 2f;        // �ٿ ����
    public float stopTime = 32f;           // �ٿ ���� �ð�

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float startY;
    private bool isBouncing = true;  // �ٿ Ȱ��ȭ ����

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
       // originalColor = spriteRenderer.color;
        startY = transform.position.y;

        StartCoroutine(MapCycle());
    }

    IEnumerator MapCycle()
    {
        while (true)
        {
            yield return FadeIn();  // ������ ��Ÿ��
            yield return new WaitForSeconds(visibleDuration); // 21�� ���� ����
            yield return FadeOut(); // ������ �����
            yield return new WaitForSeconds(reappearDelay); // 1.5�� �� �ٽ� ����
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, originalColor.a, elapsedTime / fadeDuration);
           // spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        StartCoroutine(StopBouncingAfterTime(stopTime)); // 32�� �� �ٿ ���߱� ����
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
        isBouncing = false; // 32�� �� �ٿ ����
    }

    void Update()
    {
        if (isBouncing)
        {
            // �Ʒ����� ���� �ٿ (PingPong ���)
            float newY = startY + Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
