
using UnityEngine;
using System.Collections;
public class CCCCCC : MonoBehaviour
{
    public float speed = 8f;
    private bool canMove = false;
    private Vector2 originalPos;
    // y�� ������ ����
    private float moveY, moveX;

    void Start()
    {
        // ������ �� �� ���� ���� Y�� ����


        moveY = Random.Range(-5.0f, 5.0f);



        transform.position = new Vector2(transform.position.x, moveY);
        StartCoroutine(DelayWithVibration());

    }
    IEnumerator DelayWithVibration()
    {
        float timer = 0f;
        float duration = 0.25f;
        originalPos = transform.position;  // ���� ��ġ ����

        while (timer < duration)
        {
            // ���Ʒ��� ���� ����
            float offsetY = Mathf.Sin(Time.time * 70f) * 0.03f; // ���� ���� ���� 
            transform.position = new Vector2(originalPos.x, moveY + offsetY);  // ���� ����

            timer += Time.deltaTime;
            yield return null;
        }

        // ���� ���� �� ����ġ�� ����
        transform.position = new Vector2(originalPos.x, moveY);
        canMove = true;
    }

    void Update()
    {
        // �������� �̵�

        if (canMove)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);  // ��� ������Ʈ ����
    }

}


