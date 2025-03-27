using UnityEngine;
using System.Collections;

public class boss_ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay()); // 2�� ��� �� �̵� ����
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.1f); // 2�� ���

        while (true) // 2�� �Ŀ� ���� ������ �̵� ����
        {
            transform.Translate(Vector2.left * Time.deltaTime * 5);
            yield return null; // ���� �����ӱ��� ���
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
