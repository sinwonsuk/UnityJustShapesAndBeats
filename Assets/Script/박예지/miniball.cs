using UnityEngine;

public class SpriteExit : MonoBehaviour
{
    public float delayTime = 2f;   // ��ٸ� �ð�
    public float speed = 10f;      // ���������� �ӵ�
    private bool isExiting = false;
    private SpriteRenderer spriteRenderer;  // SpriteRenderer ������Ʈ

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // SpriteRenderer ������Ʈ ��������
        spriteRenderer.enabled = false;  // ��������Ʈ ��Ȱ��ȭ (������ �ʰ�)
        Invoke(nameof(StartExit), delayTime);  // 2�� �Ŀ� StartExit ȣ��
    }

    void Update()
    {
        if (isExiting)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // �ʹ� �־����� ����
            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
            }
        }
    }

    void StartExit()  // �̸��� StartExit�� ����
    {
        spriteRenderer.enabled = true;  // ��������Ʈ Ȱ��ȭ (���̰�)
        isExiting = true;  // ��������Ʈ �̵� ����
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
