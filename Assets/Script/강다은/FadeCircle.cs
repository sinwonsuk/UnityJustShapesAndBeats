using UnityEngine;
using System.Collections;

public class FadeCircle : MonoBehaviour
{
    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
		Color color = spriteRenderer.color;
		color.a = 0f;
		spriteRenderer.color = color;

		StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn()
	{
		float blinkInterval = blinkDuration / (blinkCount * 2);
		float blinkStartRatio = 0.7f;
		float totalDuration = 0.3f;
		float nextBlinkTime = totalDuration * blinkStartRatio;
		bool isBlinking = false;

		float elapsedTime = 0.0f;
		Color startColor = spriteRenderer.color;
		Color targetColor = startColor;
		targetColor.a = 0.1f;

		while (elapsedTime < totalDuration)
		{
			elapsedTime += Time.deltaTime;
			float fadeT = Mathf.Clamp01(elapsedTime / totalDuration);

			Color currentColor = Color.Lerp(startColor, targetColor, fadeT);

			if (elapsedTime >= nextBlinkTime)
			{
				isBlinking = !isBlinking;
				nextBlinkTime += blinkInterval;
			}

			if (isBlinking && elapsedTime >= totalDuration * blinkStartRatio)
			{
				
				float blinkT = Mathf.PingPong(elapsedTime * 10f, 1f);
				currentColor = Color.Lerp(currentColor, Color.white, targetColor.a);
			}
			spriteRenderer.color = currentColor;
			yield return null;
		}

		spriteRenderer.color = targetColor; 
	}
	private SpriteRenderer spriteRenderer;
	private float blinkDuration = 1f;
	private int blinkCount = 10;
}
