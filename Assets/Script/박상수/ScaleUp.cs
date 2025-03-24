using System.Collections;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{   
    public float targetScale = 2f;         // 최종 크기 
    public float duration = 2f;            // 크기 증가에 걸리는 시간
    public float speedMultiplier = 1f;     // 속도 조절 (1이면 기본, 높을수록 더 빠름)

    void Start()
    {
        StartCoroutine(ScaleOverTime(duration / speedMultiplier));
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 startScale = transform.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, 1);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(startScale, finalScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = finalScale;
        Debug.Log($"🧩 자식 작업 완료됨: {gameObject.name}");
        Destroy(gameObject);
    }
}