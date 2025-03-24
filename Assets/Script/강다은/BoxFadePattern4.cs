using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFadePattern4 : MonoBehaviour
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
		Debug.Log("Box Pattern 4 시작");
		StartPattern();
	}

	private void Update()
	{
		Delete();
	}

	private void OnDisable()
	{
		CleanUpPatternObject();
		Debug.Log("Box Pattern 4 끝 및 오브젝트 클린 작업 완료");
	}

	public void StartPattern()
	{
		StartCoroutine(RunPatternSequence());
	}

	public void SetDirection(Direction direction)
	{
		currentDirection = direction;
	}

	private void CleanUpPatternObject()
	{
		Debug.Log("CleanUpPatternObject 호출됨");
		foreach (GameObject box in activeBoxes)
		{
			Destroy(box);
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
		Direction[] sequence = { Direction.LeftToRight, Direction.RightToLeft, Direction.TopToBottom, Direction.BottomToTop };
		bool[] isHorizontalSequence = { true, true, false, false };

		for (int i = 0; i < sequence.Length; i++)
		{
			patternIndex = i;
			currentDirection = sequence[i];
			bool isHorizontal = isHorizontalSequence[i];
			yield return StartCoroutine(SpawnSingleBox(isHorizontal));
			yield return new WaitForSeconds(0.5f); 
		}
		CleanUpPatternObject();
	}

	private IEnumerator ExecuteFadeBox(bool isHorizontal, Vector3 spawnPosition)
	{
		GameObject fadeBox = Instantiate(fadeBoxPrefab, spawnPosition, Quaternion.identity);
		Vector3 startScale = isHorizontal ? new Vector3(38f, 0f, 0f) : new Vector3(0f, 20f, 0f);
		Vector3 endScale = isHorizontal ? new Vector3(38f, 1f, 0f) : new Vector3(1f, 20f, 0f);
		fadeBox.transform.localScale = startScale;

		SpriteRenderer fadeRenderer = fadeBox.GetComponent<SpriteRenderer>();
		Color fadeColor = fadeRenderer.color;
		fadeColor.a = 0f;
		fadeRenderer.color = fadeColor;

		float elapsed = 0f;
		while (elapsed < fadeDuration)
		{
			elapsed += Time.deltaTime;
			fadeColor.a = Mathf.Lerp(0f, 0.5f, elapsed / fadeDuration);
			fadeRenderer.color = fadeColor;
			fadeBox.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / fadeDuration);
			yield return null;
		}

		yield return new WaitForSeconds(0.05f);
		Destroy(fadeBox);
	}

	private IEnumerator SpawnSingleBox(bool isHorizontal)
	{
		usedYPositions.Clear();
		usedXPositions.Clear();

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
				else usedXPositions.Clear();
				break;
			}
		} while (IsOverlapping(spawnPosition, isHorizontal));

		if (isHorizontal) usedYPositions.Add(spawnPosition.y);
		else usedXPositions.Add(spawnPosition.x);

		yield return StartCoroutine(ExecuteFadeBox(isHorizontal, spawnPosition));

		GameObject newBox = Instantiate(movingBoxPrefab, spawnPosition, Quaternion.identity);
		newBox.transform.localScale = isHorizontal ? new Vector3(0f, 0.09f, 1f) : new Vector3(0.09f, 0f, 1f);
		activeBoxes.Add(newBox);

		yield return StartCoroutine(GenerateBox(newBox, isHorizontal));
	}

	private bool IsOverlapping(Vector3 position, bool isHorizontal)
	{
		List<float> usedPositions = isHorizontal ? usedYPositions : usedXPositions;

		foreach (float usedPos in usedPositions)
		{
			if (Mathf.Abs((isHorizontal ? position.y : position.x) - usedPos) < minDistance)
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator GenerateBox(GameObject box, bool isHorizontal)
	{
		float duration = 0.4f;
		float elapsed = 0f;
		SpriteRenderer boxRenderer = box.GetComponent<SpriteRenderer>();
		Color startColor = Color.white;

		float startPos, endPos;
		Vector3 startScale, endScale;

		if (isHorizontal)
		{
			if (currentDirection == Direction.LeftToRight)
			{
				startPos = -9f;
				endPos = 9f;
				startScale = new Vector3(0f, 0.09f, 1f);
				endScale = new Vector3(18f, 0.09f, 1f);
			}
			else // RightToLeft
			{
				startPos = 9f;
				endPos = -9f;
				startScale = new Vector3(0f, 0.09f, 1f);
				endScale = new Vector3(18f, 0.09f, 1f);
			}
		}
		else
		{
			if (currentDirection == Direction.TopToBottom)
			{
				startPos = 5f;
				endPos = -4f;
				startScale = new Vector3(0.09f, 0f, 1f);
				endScale = new Vector3(0.09f, 9f, 1f);
			}
			else // BottomToTop
			{
				startPos = -4f;
				endPos = 5f;
				startScale = new Vector3(0.09f, 0f, 1f);
				endScale = new Vector3(0.09f, 9f, 1f);
			}
		}

		if (isHorizontal)
			box.transform.position = new Vector3(startPos, box.transform.position.y, 0);
		else
			box.transform.position = new Vector3(box.transform.position.x, startPos, 0);

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;

			if (box != null)
			{
				box.transform.localScale = Vector3.Lerp(startScale, endScale, t);
				if (isHorizontal)
				{
					float newX = Mathf.Lerp(startPos, endPos, t);
					box.transform.position = new Vector3(newX, box.transform.position.y, 0);
				}
				else
				{
					float newY = Mathf.Lerp(startPos, endPos, t);
					box.transform.position = new Vector3(box.transform.position.x, newY, 0);
				}

				if (t <= 0.5f)
				{
					boxRenderer.color = startColor;
				}
				else
				{
					float colorT = (t - 0.5f) / 0.5f;
					boxRenderer.color = Color.Lerp(startColor, targetColor, colorT);
				}
			}
			yield return null;
		}

		if (box != null)
		{
			box.transform.localScale = endScale;
			if (isHorizontal)
				box.transform.position = new Vector3(endPos, box.transform.position.y, 0);
			else
				box.transform.position = new Vector3(box.transform.position.x, endPos, 0);
			boxRenderer.color = targetColor;
			yield return new WaitForSeconds(0.05f);

			float fadeOutDuration = 0.1f;
			elapsed = 0f;

			while (elapsed < fadeOutDuration)
			{
				elapsed += Time.deltaTime;
				float t = elapsed / fadeOutDuration;
				if (box != null)
				{
					if (isHorizontal)
						box.transform.localScale = new Vector3(box.transform.localScale.x, Mathf.Lerp(0.09f, 0f, t), 1f);
					else
						box.transform.localScale = new Vector3(Mathf.Lerp(0.09f, 0f, t), box.transform.localScale.y, 1f);
					Color color = boxRenderer.color;
					color.a = Mathf.Lerp(1f, 0f, t);
					boxRenderer.color = color;
				}
				yield return null;
			}

			if (box != null)
			{
				Destroy(box);
				activeBoxes.Remove(box);
			}
		}
	}

	[SerializeField] private GameObject fadeBoxPrefab;
	[SerializeField] private GameObject movingBoxPrefab;
	[SerializeField] private float fadeDuration = 0.5f;
	[SerializeField] private Color targetColor = new Color(255f / 255f, 32f / 255f, 112f / 255f);
	private float time = 0;
	private List<GameObject> activeBoxes = new List<GameObject>();
	private List<float> usedYPositions = new List<float>();
	private List<float> usedXPositions = new List<float>();
	private const float minDistance = 2f;
	private const int maxAttempts = 10;
}