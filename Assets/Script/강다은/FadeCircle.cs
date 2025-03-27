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

		// Fade In Ω√¿€
		StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn()
	{
		float fadeTime = 0.2f;
		float time = 0f;
		Color startColor = spriteRenderer.color;
		Color targetColor = startColor;
		targetColor.a = 0.2f;

		while (time < fadeTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / fadeTime);
			spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
			yield return null;
		}

		spriteRenderer.color = targetColor;
	}
	private SpriteRenderer spriteRenderer;
}
