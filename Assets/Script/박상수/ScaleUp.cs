using System.Collections;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float targetScale = 2f;
    public float duration = 2f;

    void Start()
    {
        StartCoroutine(ScaleOverTime(duration));
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(targetScale, targetScale, 1);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale; // 정확한 목표 크기로 설정
        Destroy(gameObject); // 최대 크기에 도달하면 삭제
    }
}
