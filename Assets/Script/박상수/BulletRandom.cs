using System.Collections;
using UnityEngine;

public class BulletRandom : MonoBehaviour
{
    [SerializeField]
    private GameObject circle;
    [SerializeField]
    private float fireRate = 1f;

    public Vector2 randomPositionMin; // ���� ��ġ �ּҰ�
    public Vector2 randomPositionMax; // ���� ��ġ �ִ밪

    void Start()
    {

        StartCoroutine(SpawnCircle());
    }

    IEnumerator SpawnCircle()
    {
        int count = 1; // ������ �Ѿ� ���� (1���� ����)

        while (count <= 6) // �ִ� 6������ ����
        {
         
                float posX = Random.Range(9.0f, 13.0f);
                float posY = Random.Range(0.0f, 5.0f);

                Instantiate(circle, new Vector2(posX, posY), Quaternion.identity);

 
            yield return new WaitForSeconds(fireRate);
        }
    }
}

