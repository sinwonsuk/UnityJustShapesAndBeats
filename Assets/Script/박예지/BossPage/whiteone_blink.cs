using UnityEngine;
using System.Collections;

public class whiteone_blink : MonoBehaviour
{
    public float spawnDelay = 5f; //  스폰되기 전 대기 시간 (초 단위, 인스펙터에서 설정 가능)
    public int blinkCount = 3;  //  반짝이는 횟수
    public float blinkInterval = 0.5f; //  반짝이는 간격 (초)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // 처음엔 숨겨놓기
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(spawnDelay); //  인스펙터에서 설정한 시간만큼 대기
        spriteRenderer.enabled = true; //  스폰 후 보이게 설정
        StartCoroutine(BlinkAndDestroy()); // 반짝이기 시작
    }

    IEnumerator BlinkAndDestroy()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false; // 숨기기
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.enabled = true; // 보이기
            yield return new WaitForSeconds(blinkInterval);
        }

        gameObject.SetActive(false); // 반짝이기 끝나면 비활성화
    }
}
