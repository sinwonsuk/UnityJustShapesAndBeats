using UnityEngine;
using System.Collections;

public class ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay()); // 2�� ��� �� �̵� ����
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ���

        // 2�� �ڿ� �ٷ� �̵� ����
        while (true) // ���� ����
        {
            transform.Translate(Vector2.left * Time.deltaTime * 5);
            yield return null; // ���� �����ӱ��� ���
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� ������ ������ ��ü ����
    }
}
