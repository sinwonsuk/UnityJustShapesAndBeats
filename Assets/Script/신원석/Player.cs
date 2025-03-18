using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Move,
        MoveStop,
    }

    PlayerState playerState = PlayerState.Idle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                {
                    float moveX = Input.GetAxis("Horizontal"); // A, D �Ǵ� ��, ��
                    float moveY = Input.GetAxis("Vertical");   // W, S �Ǵ� ��, ��

                    Vector2 moveDir = new Vector2(moveX, moveY);

                    if (moveDir.sqrMagnitude > 0.01f) // �̵� ���� ���� ȸ��
                    {
                        StopCoroutine(test());
                        spawn.gameObject.SetActive(true);
                        playerState = PlayerState.Move;
                        return;                                   
                    }

                }
                break;
            case PlayerState.Move:
                {                   
                    MovePlayer();
                }
                break;
            case PlayerState.MoveStop:
                {
                   
                }
                break;
            default:
                break;
        }
    }

   void MovePlayer()
   {
        float moveX = Input.GetAxis("Horizontal"); // A, D �Ǵ� ��, ��
        float moveY = Input.GetAxis("Vertical");   // W, S �Ǵ� ��, ��

        Vector2 moveDir = new Vector2(moveX, moveY);

        if (moveDir.sqrMagnitude > 0.01f) // �̵� ���� ���� ȸ��
        {
            ChangeScale(1.2f,0.8f);
            transform.right = moveDir.normalized * Time.deltaTime * speed; // �̵� ������ �ٶ󺸰� ����
        }
        else
        {
            StartCoroutine(test());
            spawn.gameObject.SetActive(false);
            playerState = PlayerState.Idle;
            return;
        }

        transform.position += (Vector3)moveDir.normalized * speed * Time.deltaTime;
    }
    void ChangeScale(float targetX,float targetY)
    {
        float scaleX = Mathf.MoveTowards(transform.localScale.x, 1.8f, scaleXSpeed * Time.deltaTime);
        float scaleY = Mathf.MoveTowards(transform.localScale.y, 0.6f, scaleYSpeed * Time.deltaTime);

        if (scaleX > targetX)
        {
            scaleX = targetX;
        }
        if(scaleY > targetY)
        {
            scaleY = targetY;
        }

        transform.localScale = new Vector3(scaleX, scaleY, 0.0f);

    }
    IEnumerator MoveStopAni(float x,float y)
    {
               
       float startTime = Time.time; // ���� �ð� ����

       while (Time.time - startTime < 0.1f) // 0.1�� ���ȸ� �ݺ�
       {
           float scaleX = Mathf.MoveTowards(transform.localScale.x, x, aniScaleXSpeed * Time.deltaTime);
           float scaleY = Mathf.MoveTowards(transform.localScale.y, y, aniScaleYSpeed * Time.deltaTime);

           transform.localScale = new Vector3(scaleX, scaleY, 0.0f);

           yield return null; // �� ������ ��� (0.02�� ����, FPS�� ���� �ٸ�)
       }

            // 0.1�� �� while�� Ż��
            Debug.Log("Ż�� �Ϸ�!");     
    }

    IEnumerator test()
    {

        while (true)
        {
           
            yield return MoveStopAni(stopAniScaleY, stopAniScaleX);
            yield return MoveStopAni(stopAniScaleX, stopAniScaleY);           
            yield return MoveStopAni(stopAniScaleY, stopAniScaleX);
            yield return MoveStopAni(1.0f, 1.0f);
            yield break;
        }
    }


    [SerializeField]
    private PlayerParticleSpawn spawn;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float scaleXSpeed = 5.0f;

    [SerializeField]
    private float scaleYSpeed = 5.0f;


    [SerializeField]
    private float aniScaleXSpeed = 20.0f;

    [SerializeField]
    private float aniScaleYSpeed = 20.0f;

    [SerializeField]
    private float stopAniScaleX = 1.2f;

    [SerializeField]
    private float stopAniScaleY = 0.9f;


}
