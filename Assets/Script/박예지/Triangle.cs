using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour
{
    public float speed = 5f;             // �ʱ� �ӵ�
    public float deceleration = 1f;      // ���ӷ�
    public float shakeAmount = 0.02f;    // ���� ����
    public int shakeCount = 3;           // ���� Ƚ��
    public float shakeSpeed = 0.2f;      // ���� �ӵ�
    public Color shakeColor = Color.white; // ���� �� ����
    private Color originalColor;         // ���� ����
    public Color startColor = new Color(1f, 0.4f, 0.7f); // �⺻�� ��ȫ��
    public float delayBeforeShake = 2f;  // ������ ���۵Ǳ� ���� ����� �ð� (��)

    private bool isShaking = false;
    private Renderer rend; // Renderer ������Ʈ

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = startColor;  // ���� ���� ����
        originalColor = startColor;        // ���� ���� ���� (���� ������ ���ư� ��)
    }

    void Update()
    {
        if (speed > 0)
        {
            // ���� �̵�
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // �ӵ� ����
            speed -= deceleration * Time.deltaTime * 2;

            if (speed < 0)
            {
                speed = 0;
                StartCoroutine(WaitBeforeShake());
            }
        }
    }

    // ���� ���� �� ��� �ð� �߰�
    IEnumerator WaitBeforeShake()
    {
        yield return new WaitForSeconds(delayBeforeShake);  // delayBeforeShake �ð���ŭ ���
        StartCoroutine(Shake());  // ��� �� Shake ����
    }

    IEnumerator Shake()
    {
        if (isShaking) yield break; // �ߺ� ����
        isShaking = true;

        Vector3 originalPos = transform.position;

        // ���� ���� ���� �� ����
        StartCoroutine(ChangeColor(shakeColor));

        for (int i = 0; i < shakeCount; i++)
        {
            // ����
            yield return MoveTo(originalPos + Vector3.left * shakeAmount);
            // �Ʒ���
            yield return MoveTo(originalPos - Vector3.left * shakeAmount);
        }

        // ���������� ���� ��ġ��
        transform.position = originalPos;

        // ���� ���� �� �� ���� ����
        StartCoroutine(ChangeColor(originalColor));

        // ���� ���� �� �������� �̵� ����
        speed = 15f; // �ٽ� �̵� �ӵ��� ����
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
        float duration = 0.5f; // ���� �ٲ�� �� �ɸ��� �ð�

        while (elapsedTime < duration)
        {
            rend.material.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rend.material.color = targetColor; // ���� ������ ����Ǿ��� ��
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
