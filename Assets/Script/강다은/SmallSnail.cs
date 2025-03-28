using System.Collections;
using UnityEngine;

public class SmallSnail : MonoBehaviour
{
	private void Awake()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform); // 본체만 제외

		foreach (Transform child in childTransforms)
		{
			if (child.gameObject.name == "Water_tail")
			{
				waterTail = child;
				break;
			}
		}

		spriteRenderer = GetComponent<SpriteRenderer>();
		childRenderers = GetComponentsInChildren<SpriteRenderer>();
		childRenderers = System.Array.FindAll(childRenderers, r => r != spriteRenderer && r.gameObject.name != "mouth_0" && r.gameObject.name != "eye_collabo_0");
	}

	public void Initialize(GameObject spawnedObject)
	{
		transform.position = spawnedObject.transform.position;
		transform.localScale = Vector3.zero;
		SetColor(originalColor);
		StartCoroutine(AppearSlayer());
	}

	private void SetScale(Vector3 scale)
	{
		transform.localScale = scale;
		foreach (Transform childTransform in childTransforms)
		{
			if (childTransform != null)
			{
				childTransform.localScale = scale * 1.4f;
			}
		}
	}

	private void SetColor(Color color)
	{
		if (spriteRenderer != null)
		{
			spriteRenderer.color = color;
		}
		foreach (SpriteRenderer childRenderer in childRenderers)
		{
			if (childRenderer != null)
			{
				childRenderer.color = color;
			}
		}
	}

	private void Update()
	{
		// 몸통 회전 (자체 축 기준)
		transform.Rotate(0f, 0f, -bodyRotationSpeed * Time.deltaTime);

		// 꼬리 회전 (부모를 중심으로 공전)
		if (waterTail != null)
		{
			waterTail.RotateAround(transform.position, Vector3.forward, -tailRotationSpeed * Time.deltaTime);
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
			Vector3 newScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, t);
			SetScale(newScale);
			yield return null;
		}
		SetScale(Vector3.one * 0.5f);

		// 반복: 0.5 → maxScale → 0.5 (블링크 효과 포함)
		for (int i = 0; i < repeatCount; i++)
		{
			time = 0f;
			SetColor(flashColor); // 깜빡일 때 흰색
			while (time < growTime)
			{
				time += Time.deltaTime;
				float t = Mathf.Clamp01(time / growTime);
				Vector3 newScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one * maxScale, t);
				SetScale(newScale);
				yield return null;
			}
			SetScale(Vector3.one * maxScale);
			SetColor(originalColor); // 원래 색상으로 복귀

			time = 0f;
			while (time < shrinkTime)
			{
				time += Time.deltaTime;
				float t = Mathf.Clamp01(time / shrinkTime);
				float easeT = t * t; // 부드러운 감속 효과
				Vector3 newScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * 0.5f, easeT);
				SetScale(newScale);
				yield return null;
			}
			SetScale(Vector3.one * 0.5f);
		}
	}

	[SerializeField] private float growTime = 0.03f;
	[SerializeField] private float shrinkTime = 0.3f;
	[SerializeField] private int repeatCount = 4; // 반복 횟수
	[SerializeField] private float maxScale = 0.7f; // 최대 크기
	[SerializeField] private float bodyRotationSpeed = 45f; // 몸통 회전 속도 (도/초)
	[SerializeField] private float tailRotationSpeed = 360f; // 꼬리 회전 속도 (도/초, 몸통보다 빠름)
	[SerializeField] private Color flashColor = Color.white; // 블링크 색상 (흰색)
	private readonly Color originalColor = new Color(255 / 255f, 32 / 255f, 112 / 255f, 1f); // 원래 색상 (진한 핑크)

	private Transform[] childTransforms;
	private Transform waterTail; // 꼬리 Transform
	private SpriteRenderer spriteRenderer;
	private SpriteRenderer[] childRenderers;
}