using UnityEngine;
using System.Collections;

public class boss_ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay()); // 2초 대기 후 이동 시작
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.1f); // 2초 대기

        while (true) // 2초 후에 무한 루프로 이동 시작
        {
            transform.Translate(Vector2.left * Time.deltaTime * 5);
            yield return null; // 다음 프레임까지 대기
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
