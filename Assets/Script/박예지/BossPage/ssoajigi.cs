
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

        while (true) // ���� ����
        {
            transform.Translate(Vector2.left * Time.deltaTime * 3);
            yield return null; // ���� �����ӱ��� ���
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� ������ ������ ��ü ����
    }
}