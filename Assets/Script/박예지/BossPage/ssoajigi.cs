
using UnityEngine;
using System.Collections;

public class ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {

        while (true) // 무한 루프
        {
            transform.Translate(Vector2.left * Time.deltaTime * 3);
            yield return null; // 다음 프레임까지 대기
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // 화면 밖으로 나가면 객체 삭제
    }
}