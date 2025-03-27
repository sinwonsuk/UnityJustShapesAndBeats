using UnityEngine;
using System.Collections;

public class ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay()); // 2초 대기 후 이동 시작
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        // 2초 뒤에 바로 이동 시작
        while (true) // 무한 루프
        {
            transform.Translate(Vector2.left * Time.deltaTime * 5);
            yield return null; // 다음 프레임까지 대기
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // 화면 밖으로 나가면 객체 삭제
    }
}
