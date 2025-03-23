using System.Collections;
using UnityEngine;

public class SmallerAni : MonoBehaviour
{

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        time = 0;

        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        time += Time.deltaTime;

        float decreaseTime = time / duration;
    

        float alpha = 0f;

        if (time < whiteStartTime)     // 흰색 나타기전
        {
            alpha = 0f;
        }
        else if (time < whiteStartTime + whiteFadeDuration)    // 흰색 나타난후
        {
            float fadeInProgress = (time - whiteStartTime) / whiteFadeDuration;
            alpha = Mathf.Clamp01(fadeInProgress);
        }
        else        // 투명해지기, 크기줄어들기
        {

            float fadeOutStart = whiteStartTime + whiteFadeDuration;
            float fadeOutDuration = duration - fadeOutStart; 


            float fadeOutProgress = (time - fadeOutStart) / fadeOutDuration;
            fadeOutProgress = Mathf.Clamp01(fadeOutProgress);

            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, decreaseTime);

            alpha = 1f - fadeOutProgress;
        }

        spriteRenderer.color = new Color(1, 1, 1, alpha);

        if (time >= duration)
        {
            gameObject.SetActive(false); 
        }
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector3 originalScale;
    [SerializeField] private float time;

    [SerializeField] private float duration = 0.6f; // 크기 줄어드는 시간 
    [SerializeField] private float whiteStartTime = 0.2f; // 하얀색등장 시점
    [SerializeField] private float whiteFadeDuration = 0.1f; // 흰색 완전히 등장시간 

}
