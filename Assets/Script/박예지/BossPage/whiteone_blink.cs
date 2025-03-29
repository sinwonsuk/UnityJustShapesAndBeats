using UnityEngine;
using System.Collections;

public class whiteone_blink : MonoBehaviour
{
    public float spawnDelay = 5f; //  �����Ǳ� �� ��� �ð� (�� ����, �ν����Ϳ��� ���� ����)
    public int blinkCount = 3;  //  ��¦�̴� Ƚ��
    public float blinkInterval = 0.5f; //  ��¦�̴� ���� (��)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // ó���� ���ܳ���
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(spawnDelay); //  �ν����Ϳ��� ������ �ð���ŭ ���
        spriteRenderer.enabled = true; //  ���� �� ���̰� ����
        StartCoroutine(BlinkAndDestroy()); // ��¦�̱� ����
    }

    IEnumerator BlinkAndDestroy()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false; // �����
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.enabled = true; // ���̱�
            yield return new WaitForSeconds(blinkInterval);
        }

        gameObject.SetActive(false); // ��¦�̱� ������ ��Ȱ��ȭ
    }
}
