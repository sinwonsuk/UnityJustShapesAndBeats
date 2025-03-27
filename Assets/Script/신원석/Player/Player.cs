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
        yield return new WaitForSeconds(0.1f); // 0.1�� ���� ���� ���մϴ�.
        rigidbody2D.linearVelocity = Vector3.zero; // �ӵ��� 0���� �����Ͽ� ���� ����ϴ�.
    }

    void limitPlayerMove()
    {
        // �߽� ��ǥ�� ����Ʈ ��ǥ�� ��ȯ
        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);

        // ���� ũ�⸦ ����Ʈ ũ��� ��ȯ
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
        float moveX = Input.GetAxis("Horizontal"); // A, D �Ǵ� ��, ��
        float moveY = Input.GetAxis("Vertical");   // W, S �Ǵ� ��, ��

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
        float moveX = Input.GetAxisRaw("Horizontal"); // A, D �Ǵ� ��, ��
        float moveY = Input.GetAxisRaw("Vertical");   // W, S �Ǵ� ��, ��

        Vector2 moveDir = new Vector2(moveX, moveY);

        if (moveDir.sqrMagnitude > 0.01f) // �̵� ���� ���� ȸ��
        {
            ChangeScale(1.2f,0.8f);
            transform.right = moveDir.normalized * Time.deltaTime * speed; // �̵� ������ �ٶ󺸰� ����
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
