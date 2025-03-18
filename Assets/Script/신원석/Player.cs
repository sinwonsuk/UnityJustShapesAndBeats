using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Move,
    }

    PlayerState playerState = PlayerState.Idle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangePlayerState();
        MovePlayer();
    }

   void MovePlayer()
   {
        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");
        //transform.Translate(new Vector2(moveX, moveY) * Time.deltaTime * speed);


        float moveX = Input.GetAxis("Horizontal"); // A, D 또는 ←, →
        float moveY = Input.GetAxis("Vertical");   // W, S 또는 ↑, ↓

        Vector2 moveDir = new Vector2(moveX, moveY);

        if (moveDir.sqrMagnitude > 0.01f) // 이동 중일 때만 회전
        {
            ChangeUpScale();
            transform.right = moveDir.normalized * Time.deltaTime *speed; // 이동 방향을 바라보게 설정
        }
        else
        {
            ChangeDownScale();
        }


        //if(moveX == 0 && moveY == 0)
        //{
        //    transform.localScale = new Vector3(1, 1, 0);
        //}



        transform.position += (Vector3)moveDir.normalized * speed * Time.deltaTime;
    }

    void ChangePlayerState()
    {

    }

    void ChangeUpScale()
    {
        float scaleX = Mathf.MoveTowards(transform.localScale.x, 1.8f, scaleXSpeed * Time.deltaTime);
        float scaleY = Mathf.MoveTowards(transform.localScale.y, 0.6f, scaleYSpeed * Time.deltaTime);

        if (scaleX > 1.6f)
        {
            scaleX = 1.6f;
        }
        if(scaleY > 0.8f)
        {
            scaleY = 0.8f;
        }

        transform.localScale = new Vector3(scaleX, scaleY, 0.0f);

    }
    void ChangeDownScale()
    {
        float scaleX = Mathf.MoveTowards(transform.localScale.x, 1.0f, scaleXSpeed * Time.deltaTime);
        float scaleY = Mathf.MoveTowards(transform.localScale.y, 1.0f, scaleYSpeed * Time.deltaTime);

        if (scaleX > 1.0f)
        {
            scaleX = 1.0f;
        }
        if (scaleY > 1.0f)
        {
            scaleY = 1.0f;
        }
        transform.localScale = new Vector3(scaleX, scaleY, 0.0f);
    }
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float scaleXSpeed = 5.0f;

    [SerializeField]
    private float scaleYSpeed = 5.0f;

}
