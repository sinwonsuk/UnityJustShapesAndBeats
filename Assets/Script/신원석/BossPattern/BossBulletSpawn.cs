using System.Collections;
using UnityEngine;

public class BossBulletSpawn : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(CircleFire());
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        StopCoroutine(CircleFire());
    }

    IEnumerator CircleFire()
    {

        //발사체 생성갯수
        int count = 10;
        //발사체 사이의 각도
        float intervalAngle = 360 / count;
        //가중되는 각도(항상 같은 위치로 발사하지 않도록 설정
       

        //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
        while (true)
        {            
            for (int i = 0; i < count; ++i)
            {
                //발사체 생성
                GameObject clone = Instantiate(pinkBullet, transform.position, Quaternion.identity);

                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향(벡터)
                //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //발사체 이동 방향 설정
                clone.GetComponent<PinkBullet>().Move(new Vector2(x, y));
            }

            weightAngle = (weightAngle == 0f) ? 60f : 0f;

            // fireCircleCheck++;
            //발사체가 생성되는 시작 각도 설정을 위한변수

            yield return new WaitForSeconds(attackRate);

        }
    }

    //float fireCircleCheck = 0;


    //공격주기
    [SerializeField]
    float attackRate = 0.2f;

    [SerializeField]
    float weightAngle = 0f;

    [SerializeField]
    private GameObject pinkBullet;

    bool test = false;

}
