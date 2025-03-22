using System.Collections;
using UnityEngine;

public class BulletRandom : MonoBehaviour
{
    [SerializeField]
    private GameObject circle;
    [SerializeField]
    private float fireRate = 1f;

    public Vector2 randomPositionMin; // 랜덤 위치 최소값
    public Vector2 randomPositionMax; // 랜덤 위치 최대값

    void Start()
    {

        StartCoroutine(SpawnCircle());
    }

    IEnumerator SpawnCircle()
    {
        int count = 1; // 생성할 총알 개수 (1부터 시작)

        while (count <= 6) // 최대 6개까지 증가
        {
         
                float posX = Random.Range(9.0f, 13.0f);
                float posY = Random.Range(0.0f, 5.0f);

                Instantiate(circle, new Vector2(posX, posY), Quaternion.identity);

 
            yield return new WaitForSeconds(fireRate);
        }
    }
}

