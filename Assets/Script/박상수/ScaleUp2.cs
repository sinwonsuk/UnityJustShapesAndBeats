using System.Collections;
using UnityEngine;

public class ScaleUp2 : MonoBehaviour
{  
    public float targetScale = 2f;  
    public float duration = 2f;
    public float speedMultiplier = 1f;

    void Start()
    {
        StartCoroutine(ScaleOverTime(duration / speedMultiplier));
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 original = transform.localScale;
        Vector3 target = new Vector3(targetScale, targetScale, 1);
        float elapsed = 0f;
        while (elapsed < time)
        {
            transform.localScale = Vector3.Lerp(original, target, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = target;
        Destroy(gameObject);
    }
}