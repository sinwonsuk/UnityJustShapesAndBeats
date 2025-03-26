using System.Collections;
using UnityEngine;

public class WonColor : MonoBehaviour
{
    // ��ǥ ���� (���⼭�� ��ũ �迭)
    [SerializeField] private Color targetColor = new Color(255f / 255f, 32f / 255f, 112f / 255f);
    // ������ ���� (PingPong�� �ֱ⸦ ����: ���� �������� ������ ������)
    [SerializeField] private float blinkInterval = 0.5f;

    // SpriteRenderer�� ������ ����
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // ���� ���� ������Ʈ�� SpriteRenderer ������Ʈ�� �����ɴϴ�.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Ŀ���� ���� �Լ��� ȣ���Ͽ� ������ ȿ���� �����մϴ�.
        ColorLerpStarting(gameObject);
    }

    // ���޹��� ���� ������Ʈ�� ������ ����� targetColor ���̿��� �����̵��� �����մϴ�.
    void ColorLerpStarting(GameObject box)
    {
        // box�� SpriteRenderer ������Ʈ�� �����ɴϴ�.
        SpriteRenderer movingRenderer = box.GetComponent<SpriteRenderer>();
        // Mathf.PingPong�� ����Ͽ� Time.time ���� ���� 0���� blinkInterval ������ ���� ����
        // �̸� blinkInterval�� ������ 0~1 ������ t ���� ����ϴ�.
        float t = Mathf.PingPong(Time.time, blinkInterval) / blinkInterval;
        // Color.Lerp�� ����Ͽ� ����� targetColor ������ ���� t ���� ���� �����Ͽ� �����մϴ�.
        movingRenderer.color = Color.Lerp(Color.white, targetColor, t);
    }
}