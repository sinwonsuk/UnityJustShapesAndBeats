using UnityEngine;
using System.Collections;

public class boss_ball_spawner : MonoBehaviour
{
    public GameObject Bbullet;

    void Start()
    {
        StartCoroutine(StartDelay()); // 보스가 등장한 직후 탄 발사
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 대기 후 실행

        // 12개의 탄을 한 번에 발사
        ShootBullets();

        // 보스 오브젝트는 18초 뒤에 삭제
        Destroy(gameObject, 18f);
    }

    // 12개의 탄을 한 번에 발사하는 함수
    void ShootBullets()
    {
        for (int i = 0; i < 12; i++)
        {
            // 각도마다 회전하여 탄을 발사
            Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }
    }
}
