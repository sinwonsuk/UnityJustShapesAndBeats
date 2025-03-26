using System.Collections;
using UnityEngine;

public class BoxFadePattern1 : MonoBehaviour
{
	private void OnEnable()
	{
		Debug.Log("Box Pattern ����");
		StartPattern();
	}

	private void Update()
	{
		Delete();
	}

	private void OnDisable()
	{
		CleanUpPatternObject();
		Debug.Log("Box Pattern �� �� ������Ʈ Ŭ�� �۾� �Ϸ�");
	}

	public void StartPattern()
	{
		StartCoroutine(RunPatternSequence());
	}

	private void CleanUpPatternObject()
	{
		Debug.Log("CleanUpPatternObject ȣ���");
		if (currentBox != null)
		{
			Destroy(currentBox);
			currentBox = null;
		}
		Destroy(gameObject);
	}

	private void Delete()
	{
		time += Time.deltaTime;
		if (time > 10.0f)
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator RunPatternSequence()
	{
		if (currentBox != null)
		{
			Destroy(currentBox);
			currentBox = null;
		}

		// Y�� ���� ��ġ ����
		float yPos = GetRandomNonOverlappingY();
		Vector3 spawnPosition = new Vector3(transform.position.x, yPos, 0);

		// FadeBox ���� ��ġ�� ���� Y ��ġ�� ����
		yield return StartCoroutine(ExecutePattern(spawnPosition));

		// MovingBox ���� ��ġ�� ���� Y ��ġ�� ����
		currentBox = Instantiate(movingBoxPrefab, new Vector3(screenWidth, yPos, 0), Quaternion.identity);
		currentBox.transform.localScale = new Vector3(0f, 0.09f, 1f);

		yield return StartCoroutine(GenerateBox(currentBox));
		CleanUpPatternObject();
	}

	private IEnumerator ExecutePattern(Vector3 spawnPosition)
	{
		GameObject fadeBox = Instantiate(fadeBoxPrefab, spawnPosition, Quaternion.identity);
		fadeBox.transform.localScale = new Vector3(100f, 0f, 0f);
		SpriteRenderer fadeRenderer = fadeBox.GetComponent<SpriteRenderer>();
		Color fadeColor = fadeRenderer.color;
		fadeColor.a = 0f;
		fadeRenderer.color = fadeColor;
		Vector3 startScale = new Vector3(100f, 0f, 0f);
		Vector3 endScale = new Vector3(100f, 1f, 0f);

		float elapsed = 0f;
		while (elapsed < fadeDuration)
		{
			elapsed += Time.deltaTime;
			fadeColor.a = Mathf.Lerp(0f, 0.5f, elapsed / fadeDuration);
			fadeRenderer.color = fadeColor;
			fadeBox.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / fadeDuration);
			yield return null;
		}

		yield return new WaitForSeconds(0.1f);
		Destroy(fadeBox);
	}

	private IEnumerator GenerateBox(GameObject box)
	{
		float startX = screenWidth;
		float endX = -screenWidth;
		Vector3 startScale = new Vector3(0f, 0.09f, 1f);
		Vector3 endScale = new Vector3(screenWidth * 2f, 0.09f, 1f);
		float duration = 1f;
		float elapsed = 0f;
		SpriteRenderer movingRenderer = box.GetComponent<SpriteRenderer>();
		Color startColor = Color.white; // ���� ������ ������� ����

		box.transform.position = new Vector3(startX, box.transform.position.y, 0);

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;

			if (box != null)
			{
				// �����ϰ� ��ġ ����
				box.transform.localScale = Vector3.Lerp(startScale, endScale, t);
				float newX = Mathf.Lerp(startX, endX, t);
				box.transform.position = new Vector3(newX, box.transform.position.y, 0);

				// ���� ����: 50%������ ���, 50%~100%���� ��ũ������
				if (t <= 0.5f)
				{
					movingRenderer.color = startColor; // 0~50% ���� ��� ����
				}
				else
				{
					// 50%~100% ������ 0~1�� ����: (t - 0.5) / (1.0 - 0.5)
					float colorT = (t - 0.5f) / 0.5f;
					movingRenderer.color = Color.Lerp(startColor, targetColor, colorT * 2);
				}
			}
			yield return null;
		}

		if (box != null)
		{
			// ���� ���� ����
			box.transform.localScale = endScale;
			box.transform.position = new Vector3(endX, box.transform.position.y, 0);
			movingRenderer.color = targetColor;
			yield return new WaitForSeconds(0.05f);

			// ���̵� �ƿ�
			float fadeOutDuration = 0.2f;
			elapsed = 0f;
			Vector3 fadeStartScale = box.transform.localScale;
			Vector3 fadeEndScale = new Vector3(0f, 0f, 1f);

			while (elapsed < fadeOutDuration)
			{
				elapsed += Time.deltaTime;
				float t = elapsed / fadeOutDuration;
				if (box != null)
				{
					box.transform.localScale = Vector3.Lerp(fadeStartScale, fadeEndScale, t);
					Color color = movingRenderer.color;
					color.a = Mathf.Lerp(1f, 0f, t);
					movingRenderer.color = color;
				}
				yield return null;
			}

			if (box != null)
			{
				Destroy(box);
				currentBox = null;
				Debug.Log("�ڽ� ���� �Ϸ�");
			}
		}
	}

	private float GetRandomNonOverlappingY()
	{
		const float minY = -4f; // Y�� ���� ���� (BoxFadePattern3�� ����)
		const float maxY = 4f;

		float yPos;
		int attempts = 0;
		const int maxAttempts = 10;

		do
		{
			yPos = Random.Range(minY, maxY);
			attempts++;
			if (attempts > maxAttempts)
			{
				usedYPositions.Clear();
				attempts = 0;
			}
		} while (IsOverlapping(yPos));

		usedYPositions.Add(yPos);
		if (usedYPositions.Count > 5)
		{
			usedYPositions.RemoveAt(0);
		}
		Debug.Log("Selected Y Position: " + yPos + ", Used Y Positions: " + string.Join(", ", usedYPositions));
		return yPos;
	}

	private bool IsOverlapping(float yPos)
	{
		const float minDistance = 2.5f;
		foreach (float usedY in usedYPositions)
		{
			if (Mathf.Abs(yPos - usedY) < minDistance)
			{
				return true;
			}
		}
		return false;
	}

	[SerializeField] private GameObject fadeBoxPrefab;
	[SerializeField] private GameObject movingBoxPrefab;
	[SerializeField] private float fadeDuration = 0.5f;
	[SerializeField] private Color targetColor = new Color(255f / 255f, 32f / 255f, 112f / 255f);
	private const float screenWidth = 100f;

	private GameObject currentBox;
	private float time = 0;
	private System.Collections.Generic.List<float> usedYPositions = new System.Collections.Generic.List<float>();
}