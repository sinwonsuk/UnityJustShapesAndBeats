using System.Collections;
using UnityEngine;

public class SpawnPinkBullet : MonoBehaviour
{
   
    void Start()
    {
       StartCoroutine(CircleFire());
    }




    void Update()
    {
        
    }
    IEnumerator CircleFire()
    {
        //�����ֱ�
        float attackRate = 0.2f;
        //�߻�ü ��������
        int count = 15;
        //�߻�ü ������ ����
        float intervalAngle = 360 / count;
        //���ߵǴ� ����(�׻� ���� ��ġ�� �߻����� �ʵ��� ����
        float weightAngle = 0f;

        //�� ���·� ����ϴ� �߻�ü ����(count ���� ��ŭ)
        while (true)
        {
            if(fireCircleCheck == 6)
            {
               fireCircleCheck = 0;
               yield break;
            }

            for (int i = 0; i < count; ++i)
            {
                //�߻�ü ����
                GameObject clone = Instantiate(pinkBullet,BulletTransform.position, Quaternion.identity);

                clone.transform.SetParent(BulletTransform);

                //�߻�ü �̵� ����(����)
                float angle = weightAngle + intervalAngle * i;
                //�߻�ü �̵� ����(����)
                //Cos(����)���� ������ ���� ǥ���� ���� pi/180�� ����
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(����)���� ������ ���� ǥ���� ���� pi/180�� ����
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //�߻�ü �̵� ���� ����
                clone.GetComponent<PinkBullet>().Move(new Vector2(x, y));
            }

            fireCircleCheck++;
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���Ѻ���
            weightAngle += 1;

            //3�ʸ��� �̻��� �߻�
            yield return new WaitForSeconds(attackRate);

        }
    }

    [SerializeField]
    private Transform BulletTransform;

    [SerializeField]
    private GameObject pinkBullet;

    int fireCircleCheck = 0;

}
