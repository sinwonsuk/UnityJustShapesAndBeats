using UnityEngine;
using System.Collections;

public class ball_spawner2 : MonoBehaviour
{
    public GameObject Bbullet;

    void Start()
    {
        StartCoroutine(StartDelay()); // 2�� ��� �� ����
        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }

        Destroy(gameObject, 18f); // 18�� �ڿ� ����
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ���
    }
}
