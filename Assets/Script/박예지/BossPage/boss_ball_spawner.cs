using UnityEngine;
using System.Collections;

public class boss_ball_spawner : MonoBehaviour
{
    public GameObject Bbullet;

    void Start()
    {
        StartCoroutine(StartDelay()); // ������ ������ ���� ź �߻�
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ��� �� ����

        // 12���� ź�� �� ���� �߻�
        ShootBullets();

        // ���� ������Ʈ�� 18�� �ڿ� ����
        Destroy(gameObject, 18f);
    }

    // 12���� ź�� �� ���� �߻��ϴ� �Լ�
    void ShootBullets()
    {
        for (int i = 0; i < 12; i++)
        {
            // �������� ȸ���Ͽ� ź�� �߻�
            Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }
    }
}
