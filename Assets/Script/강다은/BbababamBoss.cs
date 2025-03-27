using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BbababamBoss : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;
	private SpriteRenderer[] childRenderers;
	private Transform[] childTransforms;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		childRenderers = GetComponentsInChildren<SpriteRenderer>();
		childRenderers = System.Array.FindAll(childRenderers, r => r != spriteRenderer);

		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform && t.gameObject.name != "Water_tail"); // 보스 본체, 꼬리 제외
	}

	private void OnEnable()
	{
		transform.localScale = Vector3.zero;
		SetColor(originalColor);
		StartCoroutine(AppearSlayer());
	}

	private void SetColor(Color color)
	{
		spriteRenderer.color = color;
		foreach (SpriteRenderer childRenderer in childRenderers)
		{
			if (childRenderer != null)
			{
				childRenderer.color = color;
			}
		}
	}

	private void SetScale(Vector3 scale)
	{
		transform.localScale = scale; // 보스 크기 설정
		foreach (Transform childTransform in childTransforms)
		{
			if (childTransform != null)
			{
				childTransform.localScale = scale * 1.2f;
			}
		}
	}

	private IEnumerator AppearSlayer()
	{

		// 처음 등장: 0 → 0.5
		float time = 0f;
		while (time < growTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / growTime);
			transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, t);
			yield return null;
		}
		SetScale(Vector3.one * 0.5f);

		// 반복: 0.5 → 1.2 → 0.5 
		for (int i = 0; i < repeatCount; i++)
		{
			time = 0f;
			SetColor(flashColor);
			while (time < growTime)
			{
				time += Time.deltaTime;
				float t = Mathf.Clamp01(time / growTime);
				transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one * maxScale, t);
				yield return null;
			}
			SetScale(Vector3.one * maxScale);
			SetColor(originalColor);

			time = 0f;
			while (time < shrinkTime)
			{
				time += Time.deltaTime;
				float t = Mathf.Clamp01(time / shrinkTime);
				float easeT = t * t; 
				transform.localScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * 0.5f, easeT);
				yield return null;
			}
			SetScale(Vector3.one * 0.5f);
		}

		time = 0f;
		while (time < 0.02f)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / growTime);
			transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.zero, t);
			yield return null;
		}
		SetScale(Vector3.zero);

		Destroy(gameObject);
		gameObject.SetActive(false);
	}

	[SerializeField] private float growTime = 0.03f;
	[SerializeField] private float shrinkTime = 0.5f;
	[SerializeField] private int repeatCount = 4; 
	[SerializeField] private float maxScale = 1.2f;
	[SerializeField] private Color flashColor;
	[SerializeField] private Color originalColor;
}