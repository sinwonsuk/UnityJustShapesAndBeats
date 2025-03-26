using System.Collections;
using UnityEngine;

public class BossBulletSpawn : MonoBehaviour
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

        //�߻�ü ��������
        int count = 10;
        //�߻�ü ������ ����
        float intervalAngle = 360 / count;
        //���ߵǴ� ����(�׻� ���� ��ġ�� �߻����� �ʵ��� ����
       

        //�� ���·� ����ϴ� �߻�ü ����(count ���� ��ŭ)
        while (true)
        {            
            for (int i = 0; i < count; ++i)
            {
                //�߻�ü ����
                GameObject clone = Instantiate(pinkBullet, transform.position, Quaternion.identity);

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

            weightAngle = (weightAngle == 0f) ? 60f : 0f;

            // fireCircleCheck++;
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���Ѻ���

            yield return new WaitForSeconds(attackRate);

        }
    }

    //float fireCircleCheck = 0;


    //�����ֱ�
    [SerializeField]
    float attackRate = 0.2f;

    [SerializeField]
    float weightAngle = 0f;

    [SerializeField]
    private GameObject pinkBullet;

    bool test = false;

}
