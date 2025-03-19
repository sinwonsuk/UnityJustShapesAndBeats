
using UnityEngine;
using System.Collections;
public class CCCCCC : MonoBehaviour
{
    public float speed = 8f;
    private bool canMove = false;
    private Vector2 originalPos;
    // y값 고정용 변수
    private float moveY, moveX;

    void Start()
    {
        // 생성될 때 한 번만 랜덤 Y값 지정


        moveY = Random.Range(-5.0f, 5.0f);



        transform.position = new Vector2(transform.position.x, moveY);
        StartCoroutine(DelayWithVibration());

    }
    IEnumerator DelayWithVibration()
    {
        float timer = 0f;
        float duration = 0.25f;
        originalPos = transform.position;  // 원래 위치 저장

        while (timer < duration)
        {
            // 위아래로 랜덤 진동
            float offsetY = Mathf.Sin(Time.time * 70f) * 0.03f; // 진동 세기 조절 
            transform.position = new Vector2(originalPos.x, moveY + offsetY);  // 진동 적용

            timer += Time.deltaTime;
            yield return null;
        }

        // 진동 끝난 후 원위치로 정리
        transform.position = new Vector2(originalPos.x, moveY);
        canMove = true;
    }

    void Update()
    {
        // 왼쪽으로 이동

        if (canMove)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);  // 사람 오브젝트 제거
    }

}


