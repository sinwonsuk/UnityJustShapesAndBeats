using System;
using System.Collections;
using System.Collections.Generic;
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

    enum PlayerRenderState
    {
        Hp4,
        Hp3,
        Hp2,
        Hp1,
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        collider2D = GetComponent<BoxCollider2D>();

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckLobbyCollision();
        limitPlayerMove();
        UpdatePlayerState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage1") || collision.CompareTag("Stage2") || collision.CompareTag("Stage3") || collision.CompareTag("Outline"))
        {
            return;
        }
      
        Vector2 Targetpos = transform.position - collision.transform.position;

        if (isInvincible ==false)
        {
            ChangePlayerRender();
            StartCoroutine(ChangeInvincibleCoroutine());
            blinkCoroutine = StartCoroutine(Blink());
            StartCoroutine(ApplyHitEffectCoroutine());
            StartCoroutine(ApplyForceCoroutine(Targetpos.normalized));
                   
        }    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage1") || collision.CompareTag("Stage2") || collision.CompareTag("Stage3"))
        {
            lobbyCollisionCheck = true;
        }
       
    }

    private void OnDisable()
    {
        playerRenderState = PlayerRenderState.Hp4;
        spriteRenderer.sprite = spriteRenderers[(int)playerRenderState];
        spriteRenderer.color = Color.white;
        transform.position = new Vector2(-6.0f, 0.0f);
        transform.localScale = Vector3.one;

    }
    private void OnEnable()
    {
        playerRenderState = PlayerRenderState.Hp4;
        //spriteRenderer.sprite = spriteRenderers[(int)playerRenderState];
        //spriteRenderer.color = Color.white;
        transform.position = new Vector2(-6.0f, 0.0f);
        transform.localScale = Vector3.one;
    }


    private IEnumerator ApplyForceCoroutine(Vector2 Targetpos)
    {
        rigidbody2D.AddForce(Targetpos * 20.0f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f); // 0.1초 동안 힘을 가합니다.
        rigidbody2D.linearVelocity = Vector2.zero; // 속도를 0으로 설정하여 힘을 멈춥니다.
    }
    private IEnumerator ApplyHitEffectCoroutine()
    {
        Instantiate(hitEffect, transform.position, quaternion.identity);
        yield return new WaitForSeconds(0.1f); // 0.1초 동안 힘을 가합니다.
    }
    private IEnumerator ChangeInvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1.5f);
        StopCoroutine(blinkCoroutine);

        if(spriteRenderer.color.a <= 1)
        {
            StartCoroutine(IncreaseAlpha());
        }
        isInvincible = false;

        yield break;
    }

    public void CheckLobbyCollision()
    {
        if(lobbyCollisionCheck ==true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                FadeManager.fadeManager.FadeOutAndChangeScene(SceneStage.Play);
            }
            lobbyCollisionCheck = false;
        }
    }


    private void ChangePlayerRender()
    {
        playerRenderState = (PlayerRenderState)((int)playerRenderState + 1);

        if (playerRenderState == (PlayerRenderState)4)
        {
            playerRenderState = PlayerRenderState.Hp1;
        }

        spriteRenderer.sprite = spriteRenderers[(int)playerRenderState];
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            yield return ReduceAlpha();
            yield return IncreaseAlpha();
        }    
    }

    IEnumerator ReduceAlpha()
    {
        Color color = spriteRenderer.color;

        while (true)
        {
            if(spriteRenderer.color.a <= 0 )
            {
                yield break;
            }

            color.a -= Time.deltaTime * blinkSpeed;
            spriteRenderer.color = color;

            yield return null;
        }
    }

    IEnumerator IncreaseAlpha()
    {
        Color color = spriteRenderer.color;

        while (true)
        {
            if (spriteRenderer.color.a >= 1)
            {
                yield break;
            }

            color.a += Time.deltaTime * blinkSpeed;
            spriteRenderer.color = color;

            yield return null;
        }
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
        StartCoroutine(DashCoroutine());
        if(dashInvincibleCoroutine == null)
        {
            dashInvincibleCoroutine = StartCoroutine(DashInInvincible());
        }
        speed *= 3.0f;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            speed = 8;
            yield break;
        }     
    }

    IEnumerator DashInInvincible()
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(0.8f);
        collider2D.enabled = true;
        dashInvincibleCoroutine = null;
        yield break;
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

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Sprite> spriteRenderers = new List<Sprite>();

    private BoxCollider2D collider2D;

    private Rigidbody2D rigidbody2D;

    float hp = 0;

    float alpha = 0;

    PlayerRenderState playerRenderState = PlayerRenderState.Hp4;

    private PlayerState playerState = PlayerState.Idle;

    bool isInvincible = false;

    private float maxAlpha = 1f;
    private float minAlpha = 0.2f;

    [SerializeField]
    private float blinkSpeed = 0.5f;

    private Coroutine blinkCoroutine;
    private Coroutine dashInvincibleCoroutine;

    bool lobbyCollisionCheck = false;

}
