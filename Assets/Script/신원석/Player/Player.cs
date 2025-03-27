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
    }
  
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        limitPlayerMove();
        UpdatePlayerState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 Targetpos = transform.position - collision.transform.position;

        Instantiate(hitEffect, collision.transform.position, quaternion.Euler(Targetpos));

        StartCoroutine(ApplyForceCoroutine(Targetpos.normalized));
    }

    private IEnumerator ApplyForceCoroutine(Vector3 Targetpos)
    {
        rigidbody2D.AddForce(Targetpos * 20.0f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f); // 0.1초 동안 힘을 가합니다.
        rigidbody2D.linearVelocity = Vector3.zero; // 속도를 0으로 설정하여 힘을 멈춥니다.
    }

    void limitPlayerMove()
    {
        // 중심 좌표를 뷰포트 좌표로 변환
        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);

        // 월드 크기를 뷰포트 크기로 변환
        Vector3 colliderViewportSize = Camera.main.WorldToViewportPoint(transform.position + (Vector3)collider2D.size * 0.5f) - worldpos;

        worldpos.x = Mathf.Clamp(worldpos.x, colliderViewportSize.x, 1f - colliderViewportSize.x);
        worldpos.y = Mathf.Clamp(worldpos.y, colliderViewportSize.y, 1f - colliderViewportSize.y);
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(worldpos.x, worldpos.y, Camera.main.WorldToViewportPoint(transform.position).z));
    }
    
    void UpdatePlayerState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                {
                    UpdateIdle();
                }
                break;
            case PlayerState.Move:
                {
                    MovePlayer();
                }
                break;
            default:
                break;
        }
    }
   void UpdateIdle()
   {
        float moveX = Input.GetAxis("Horizontal"); // A, D 또는 ←, →
        float moveY = Input.GetAxis("Vertical");   // W, S 또는 ↑, ↓

        Vector2 moveDir = new Vector2(moveX, moveY);

        if (moveDir.sqrMagnitude > 0.01f) 
        {
            StopCoroutine(IdleAni());
            spawn.gameObject.SetActive(true);
            playerState = PlayerState.Move;
            return;
        }
   }

   void MovePlayer()
   {
        float moveX = Input.GetAxisRaw("Horizontal"); // A, D 또는 ←, →
        float moveY = Input.GetAxisRaw("Vertical");   // W, S 또는 ↑, ↓

        Vector2 moveDir = new Vector2(moveX, moveY);

        if (moveDir.sqrMagnitude > 0.01f) // 이동 중일 때만 회전
        {
            ChangeScale(1.2f,0.8f);
            transform.right = moveDir.normalized * Time.deltaTime * speed; // 이동 방향을 바라보게 설정
        }
        else
        {
            StartCoroutine(IdleAni());
            spawn.gameObject.SetActive(false);
            playerState = PlayerState.Idle;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(dash, dashTansform.position, quaternion.identity);
            StartCoroutine(Dash());
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
    IEnumerator Dash()
    {
        DashCoroutine();

        speed *= 3.0f;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            speed = 8;
            yield break;
        }     
    }

    IEnumerator MoveStopAni(float x,float y)
    {
               
       float startTime = Time.time; // 시작 시간 저장

       while (Time.time - startTime < 0.1f) // 0.1초 동안만 반복
       {
           float scaleX = Mathf.MoveTowards(transform.localScale.x, x, aniScaleXSpeed * Time.deltaTime);
           float scaleY = Mathf.MoveTowards(transform.localScale.y, y, aniScaleYSpeed * Time.deltaTime);

           transform.localScale = new Vector3(scaleX, scaleY, 0.0f);

           yield return null; // 한 프레임 대기 (0.02초 정도, FPS에 따라 다름)
       }

            // 0.1초 후 while문 탈출
            Debug.Log("탈출 완료!");     
    }

    IEnumerator IdleAni()
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

    IEnumerator DashCoroutine()
    {
        while (true)
        {
            yield return MoveStopAni(0.6f, transform.localScale.y);
            yield return MoveStopAni(1.2f, transform.localScale.y);
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

    [SerializeField]
    private GameObject dash;
    [SerializeField]
    private GameObject hitEffect;

    [SerializeField]
    private Transform dashTansform;

    private BoxCollider2D collider2D;

    private Rigidbody2D rigidbody2D;

    float hp = 0;

    private PlayerState playerState = PlayerState.Idle;

    int ad = 0;
}
