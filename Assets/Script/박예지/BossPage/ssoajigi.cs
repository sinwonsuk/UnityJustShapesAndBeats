
using UnityEngine;
using System.Collections;

public class ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {

        yield return new WaitForSeconds(2.0f); 

        while (true) // ���� ����
        {
            transform.Translate(Vector2.left * Time.deltaTime * 1.8f);
            yield return null; // ���� �����ӱ��� ���
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� ������ ������ ��ü ����
    }
}