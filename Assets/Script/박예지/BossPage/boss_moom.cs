using UnityEngine;
using System.Collections;

public class ShrinkAndDisappear : MonoBehaviour
{
    public Vector3 initialScale = new Vector3(1, 1, 1); // ó�� ũ��
    public float shrinkSpeed = 1f; // �پ��� �ӵ�
    public Color startColor = Color.white; // ó�� ����
    public Color endColor = Color.red; // �� ���� (�����̵�� �� ��)
    public float pauseTime = 1f; // ���� ��ȭ �� ���ߴ� �ð� (��)

    private Renderer objectRenderer;
    private float startTime;
    private float duration;
    private bool isPaused = false;

    private void Start()
    {
        // ó�� ũ�� ����
        transform.localScale = initialScale;

        // ������ ������Ʈ ��������
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = startColor;

        // ���� ��ȭ�� ���� �������� �ð� ���� (�پ��� �ð�)
        duration = initialScale.x / shrinkSpeed; // ũ�Ⱑ 0�� �� ������ �ɸ��� �ð�
        startTime = Time.time; // ���� �ð�

        // ���� ��ȭ �� ��� ���߱�
        StartCoroutine(PauseForColorChange());
    }

    private void Update()
    {
        // ũ�⸦ �ٿ����� ������� �ϱ�
        if (transform.localScale.x > 0)
        {
            if (!isPaused)
            {
                transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
                // �ð��� ���� ���� ���� (Lerp)
                float t = (Time.time - startTime) / duration;
                objectRenderer.material.color = Color.Lerp(startColor, endColor, t);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // ���� ��ȭ �� ��� ���ߴ� �ڷ�ƾ
    private IEnumerator PauseForColorChange()
    {
        yield return new WaitForSeconds(duration / 2); // ���� ��ȭ �߹ݿ� ��� ���߱�
        isPaused = true; // ���߱� ����
        yield return new WaitForSeconds(pauseTime); // ���ϴ� �ð���ŭ ����
        isPaused = false; // �ٽ� �����̱� ����
    }
}
