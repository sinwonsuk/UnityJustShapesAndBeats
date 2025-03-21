using System.Collections;
using UnityEngine;

public class BulletLuncert : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet; // 총알 프리팹
    [SerializeField]
    private float fireRate = 1f;

    void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    IEnumerator SpawnBullets()
    {
        int count = 1; // 생성할 총알 개수 (1부터 시작)

        while (count <= 6) // 최대 6개까지 증가
        {
            for (int i = 0; i < count; i++) // count 개수만큼 총알 생성
            {
                float posX = Random.Range(9.0f, 13.0f);
                float posY = Random.Range(0.0f, 5.0f);

                Instantiate(bullet, new Vector2(posX, posY), Quaternion.identity);
            }

            count++; // 다음 스폰 시 더 많은 총알 생성
            yield return new WaitForSeconds(fireRate);
        }
    }
}