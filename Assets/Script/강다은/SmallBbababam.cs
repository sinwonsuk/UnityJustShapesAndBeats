using System.Collections;
using UnityEngine;

public class SmallBbababam : MonoBehaviour
{


	private void Awake()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform && t.gameObject.name != "Water_tail"); // ��ü, ���� ����

		spriteRenderer = GetComponent<SpriteRenderer>();
		childRenderers = GetComponentsInChildren<SpriteRenderer>();
		childRenderers = System.Array.FindAll(childRenderers, r => r != spriteRenderer);
	}

	public void Initialize(GameObject spawnedObject)
	{
		transform.position = spawnedObject.transform.position;
		transform.localScale = Vector3.zero; // �ʱ� ũ�� 0
		SetColor(originalColor);
		StartCoroutine(AppearSlayerWithFadeCircle());
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

	private IEnumerator AppearSlayerWithFadeCircle()
	{
		// ó�� ����: 0 �� 0.5
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

		// �ݺ�: 0.5 �� maxScale �� 0.5 (��ũ ȿ�� ����)
		for (int i = 0; i < repeatCount; i++)
		{
			time = 0f;
			SetColor(flashColor); // ������ �� ���
			while (time < growTime)
			{
				time += Time.deltaTime;
				float t = Mathf.Clamp01(time / growTime);
				Vector3 newScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one * maxScale, t);
				SetScale(newScale);
				yield return null;
			}
			SetScale(Vector3.one * maxScale);
			SetColor(originalColor); // ���� �������� ����

			time = 0f;
			while (time < shrinkTime)
			{
				time += Time.deltaTime;
				float t = Mathf.Clamp01(time / shrinkTime);
				float easeT = t * t; // �ε巯�� ���� ȿ��
				Vector3 newScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * 0.5f, easeT);
				SetScale(newScale);
				yield return null;
			}
			SetScale(Vector3.one * 0.5f);
		}
	}

	[SerializeField] private float growTime = 0.03f;
	[SerializeField] private float shrinkTime = 0.3f;
	[SerializeField] private int repeatCount = 4; // �ݺ� Ƚ��
	[SerializeField] private float maxScale = 0.7f;
	[SerializeField] private int blinkCount = 3; // ��ũ Ƚ��
	[SerializeField] private float blinkStrength = 0.5f; // ��ũ ���� (0~1)
	[SerializeField] private GameObject fadeCirclePrefab; // FadeCircle ������ (��� �� ��)
	[SerializeField] private Color flashColor = Color.white; // ��ũ ���� (���)
	private readonly Color originalColor = new Color(255 / 255f, 32 / 255f, 112 / 255f, 1f); // ���� ���� (���� ��ũ)

	private Transform[] childTransforms;
	private SpriteRenderer spriteRenderer;
	private SpriteRenderer[] childRenderers;
}