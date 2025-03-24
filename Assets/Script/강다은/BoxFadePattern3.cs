using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFadePattern3 : MonoBehaviour
{
	public enum Direction
	{
		LeftToRight,
		RightToLeft,
		TopToBottom,
		BottomToTop
	}

	private Direction currentDirection;
	private int patternIndex = 0;

	private void OnEnable()
	{
		Debug.Log("Box Pattern 3 시작");
		StartPattern();
	}

	private void Update()
	{
		Delete();
	}

	private void OnDisable()
	{
		CleanUpPatternObject();
		Debug.Log("Box Pattern 3 끝 및 오브젝트 클린 작업 완료");
	}

	public void StartPattern()
	{
		StartCoroutine(RunPatternSequence());
	}

	private void CleanUpPatternObject()
	{
		Debug.Log("CleanUpPatternObject 호출됨");
		foreach (GameObject box in activeBoxes)
		{
			if (box != null)
			{
				Destroy(box);
			}
		}
		activeBoxes.Clear();
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
		for (int i = 0; i < 4; i++)
		{
			patternIndex = i % 4;
			currentDirection = (Direction)patternIndex;
			bool isHorizontal = (patternIndex < 2);
			yield return StartCoroutine(SpawnBoxPair(isHorizontal));
			yield return new WaitForSeconds(0.2f); // 박스 간 간격 (0.2초로 설정)
		}
		CleanUpPatternObject();
	}

	private IEnumerator ExecuteFadeBox(bool isHorizontal, Vector3 spawnPosition)
	{
		GameObject fadeBox = Instantiate(fadeBoxPrefab, spawnPosition, Quaternion.identity);
		Vector3 fixedScale = isHorizontal ? new Vector3(38f, 6f, 0f) : new Vector3(6f, 30f, 0f);
		fadeBox.transform.localScale = fixedScale;

		SpriteRenderer fadeRenderer = fadeBox.GetComponent<SpriteRenderer>();
		Color fadeColor = fadeRenderer.color;
		fadeColor.a = 0.3f;
		fadeRenderer.color = fadeColor;

		float elapsed = 0f;
		while (elapsed < fadeDuration)
		{
			elapsed += Time.deltaTime;
			fadeColor.a = Mathf.Lerp(0.3f, 0.3f, elapsed / fadeDuration);
			fadeRenderer.color = fadeColor;
			yield return null;
		}

		yield return new WaitForSeconds(0.05f); 
		Destroy(fadeBox);
	}

	private IEnumerator SpawnBoxPair(bool isHorizontal)
	{
		usedYPositions.Clear();

		Vector3 spawnPosition;
		int attempts = 0;

		do
		{
			float position = isHorizontal ? Random.Range(-4f, 5f) : Random.Range(-9f, 9f);
			spawnPosition = isHorizontal ? new Vector3(0, position, 0) : new Vector3(position, 0, 0);
			attempts++;

			if (attempts > maxAttempts)
			{
				if (isHorizontal) usedYPositions.Clear();
				break;
			}
		} while (isHorizontal && IsOverlapping(spawnPosition, true));

		if (isHorizontal) usedYPositions.Add(spawnPosition.y);

		Debug.Log($"Spawn Position (FadeBox & MovingBox): {spawnPosition}");

		yield return StartCoroutine(ExecuteFadeBox(isHorizontal, spawnPosition));

		GameObject newBox = Instantiate(movingBoxPrefab, spawnPosition, Quaternion.identity);
		newBox.transform.localScale = isHorizontal ? new Vector3(0f, 0.54f, 1f) : new Vector3(0.54f, 0f, 1f);
		activeBoxes.Add(newBox);

		yield return StartCoroutine(GenerateBox(newBox, isHorizontal));
	}

	private bool IsOverlapping(Vector3 position, bool isHorizontal)
	{
		if (!isHorizontal) return false;

		List<float> usedPositions = usedYPositions;

		foreach (float usedPos in usedPositions)
		{
			if (Mathf.Abs(position.y - usedPos) < minDistance)
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator GenerateBox(GameObject box, bool isHorizontal)
	{
		float positionDuration = 0.5f; // 모든 박스의 이동 시간 0.5초
		float scaleDuration = 0.5f; // 모든 박스의 크기 변화 시간 0.5초
		float elapsed = 0f;
		SpriteRenderer boxRenderer = box.GetComponent<SpriteRenderer>();
		boxRenderer.color = targetColor;

		float startPos, endPos;
		Vector3 startScale, endScale, movingScale;

		if (isHorizontal)
		{
			if (currentDirection == Direction.LeftToRight)
			{
				startPos = -9f;
				endPos = 9f;
				startScale = new Vector3(0f, 0.54f, 1f);
				movingScale = new Vector3(1.8f, 0.54f, 1f); // endScale의 10% 크기
				endScale = new Vector3(18f, 0.54f, 1f);
			}
			else // RightToLeft
			{
				startPos = 9f;
				endPos = -9f;
				startScale = new Vector3(0f, 0.54f, 1f);
				movingScale = new Vector3(1.8f, 0.54f, 1f);
				endScale = new Vector3(18f, 0.54f, 1f);
			}
		}
		else
		{
			if (currentDirection == Direction.TopToBottom)
			{
				startPos = 5f;
				endPos = -4f;
				startScale = new Vector3(0.54f, 0f, 1f);
				movingScale = new Vector3(0.54f, 0.9f, 1f); // endScale의 10% 크기
				endScale = new Vector3(0.54f, 9f, 1f);
			}
			else // BottomToTop
			{
				startPos = -4f;
				endPos = 5f;
				startScale = new Vector3(0.54f, 0f, 1f);
				movingScale = new Vector3(0.54f, 0.9f, 1f);
				endScale = new Vector3(0.54f, 9f, 1f);
			}
		}

		if (isHorizontal)
			box.transform.position = new Vector3(startPos, box.transform.position.y, 0);
		else
			box.transform.position = new Vector3(box.transform.position.x, startPos, 0);

		if (patternIndex == 2 || patternIndex == 3) // 세로 박스 (TopToBottom, BottomToTop)
		{
			box.transform.localScale = movingScale;

			elapsed = 0f;
			while (elapsed < scaleDuration)
			{
				elapsed += Time.deltaTime;
				float t = elapsed / scaleDuration;

				if (box != null)
				{
					box.transform.localScale = Vector3.Lerp(movingScale, endScale, t);
				}

				yield return null;
			}

			elapsed = 0f;
			while (elapsed < positionDuration)
			{
				elapsed += Time.deltaTime;
				float t = elapsed / positionDuration;

				if (box != null)
				{
					float newY = Mathf.Lerp(startPos, endPos, t);
					box.transform.position = new Vector3(box.transform.position.x, newY, 0);
				}
				else
				{
					Debug.LogWarning("Box is null during position movement!");
					yield break;
				}
				yield return null;
			}
		}
		else // 가로 박스 (LeftToRight, RightToLeft)
		{
			elapsed = 0f;
			while (elapsed < positionDuration)
			{
				elapsed += Time.deltaTime;
				float t = elapsed / positionDuration;

				if (box != null)
				{
					box.transform.localScale = Vector3.Lerp(startScale, endScale, t);
					float newX = Mathf.Lerp(startPos, endPos, t);
					box.transform.position = new Vector3(newX, box.transform.position.y, 0);
				}
				else
				{
					Debug.LogWarning("Box is null during position and scale movement!");
					yield break;
				}
				yield return null;
			}
		}

		if (box != null)
		{
			box.transform.localScale = endScale;
			if (isHorizontal)
				box.transform.position = new Vector3(endPos, box.transform.position.y, 0);
			else
				box.transform.position = new Vector3(box.transform.position.x, endPos, 0);

			boxRenderer.color = Color.white;
			yield return new WaitForSeconds(0.05f);
			boxRenderer.color = targetColor;

			float fadeOutDuration = 0.1f;
			elapsed = 0f;
			while (elapsed < fadeOutDuration)
			{
				elapsed += Time.deltaTime;
				float t = elapsed / fadeOutDuration;
				if (box != null)
				{
					if (isHorizontal)
					{
						box.transform.localScale = new Vector3(box.transform.localScale.x, Mathf.Lerp(0.54f, 0f, t), 1f);
					}
					else
					{
						box.transform.localScale = new Vector3(Mathf.Lerp(0.54f, 0f, t), box.transform.localScale.y, 1f);
					}
					Color color = boxRenderer.color;
					color.a = Mathf.Lerp(1f, 0f, t);
					boxRenderer.color = color;
				}

				yield return null;
			}

			if (box != null)
			{
				Debug.Log("Destroying box: " + box.name);
				Destroy(box);
			}
		}

		activeBoxes.Remove(box);
	}

	[SerializeField] private GameObject fadeBoxPrefab;
	[SerializeField] private GameObject movingBoxPrefab;
	[SerializeField] private float fadeDuration = 0.5f;
	[SerializeField] private Color targetColor = new Color(255f / 255f, 32f / 255f, 112f / 255f);
	private float time = 0;
	private List<GameObject> activeBoxes = new List<GameObject>();
	private List<float> usedYPositions = new List<float>();
	private const float minDistance = 10f;
	private const int maxAttempts = 10;
}