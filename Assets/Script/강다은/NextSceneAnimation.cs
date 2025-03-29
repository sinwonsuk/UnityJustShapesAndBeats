using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextSceneAnimation : MonoBehaviour
{
	private List<GameObject> spawnedObjects = new List<GameObject>(); // 생성된 오브젝트 추적

	public void StartMoveAnimation(Vector2 startPos, Vector2 targetPos, float duration)
	{
		StartCoroutine(MoveFromOutside(startPos, targetPos, duration));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isTriggered && collision.CompareTag("Player"))
		{
			isTriggered = true;
			StopAllCoroutines();  // 모든 코루틴 중지
			isMoving = false;
			StartCoroutine(AnimateSequence());
		}
	}

	private void Update()
	{
		SoundManager.GetInstance().ReduceSoundBgm();
	}

	private IEnumerator MoveFromOutside(Vector2 startPos, Vector2 targetPos, float duration)
	{
		isMoving = true;
		float elapsedTime = 0f;
		float noteTimer = 0f;
		Vector2 controlPoint = new Vector2((startPos.x + targetPos.x) / 2, Mathf.Max(startPos.y, targetPos.y) + Random.Range(-2f, 2f));

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			noteTimer += Time.deltaTime;
			float t = elapsedTime / duration;

			float curveT = Mathf.Sin(t * Mathf.PI * 0.5f);
			Vector2 currentPos = BezierCurve(startPos, controlPoint, targetPos, curveT);

			transform.position = currentPos;
			transform.Rotate(0, 0, moveRotationSpeed * Time.deltaTime);

			if (useScaleAnimation)
			{
				float scale = Mathf.Lerp(scaleMin, scaleMax, (Mathf.Sin(elapsedTime * scaleFrequency * 2 * Mathf.PI) + 1) / 2);
				transform.localScale = Vector3.one * scale;
			}

			if (noteTimer >= noteSpawnInterval)
			{
				GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);
				spawnedObjects.Add(note);
				StartCoroutine(AnimateNote(note));
				noteTimer = 0f;
			}

			yield return null;
		}

		transform.position = targetPos;
		transform.localScale = Vector3.one;
		isMoving = false;
	}

	private IEnumerator AnimateNote(GameObject note)
	{
		SpriteRenderer sr = note.GetComponent<SpriteRenderer>();
		Vector3 initialScale = note.transform.localScale;
		float elapsedTime = 0f;

		while (elapsedTime < noteLifetime)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / noteLifetime;
			float scaleVariation = Mathf.Sin(elapsedTime * Mathf.PI * 2) * noteScaleVariation;

			if (note != null) // Null 체크
			{
				note.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t * noteShrinkSpeed) + Vector3.one * scaleVariation;
				if (sr != null)
					sr.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
			}

			yield return null;
		}

		if (note != null)
		{
			spawnedObjects.Remove(note);
			Destroy(note);
		}
	}

	private IEnumerator AnimateSequence()
	{
		Vector3 startPos = transform.position;
		Vector3 targetPos = startPos + Vector3.up * jumpHeight;
		float elapsedTime = 0f;

		while (elapsedTime < jumpDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Sin(Mathf.PI * (elapsedTime / jumpDuration));
			transform.position = Vector3.Lerp(startPos, targetPos, t);
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
			yield return null;
		}

		GameObject expandingCircle = Instantiate(expandingCirclePrefab, startPos, Quaternion.identity);
		spawnedObjects.Add(expandingCircle);
		StartCoroutine(AnimateExpandingCircle(expandingCircle));

		yield return new WaitForSeconds(expandDuration * 0.5f);

		for (int i = 0; i < circleCount; i++)
		{
			GameObject circle = Instantiate(circlePrefab, startPos, Quaternion.identity);
			spawnedObjects.Add(circle);
			StartCoroutine(AnimateCircle(circle, i * circleWaveDelay));
		}

		GameObject fadeout = Instantiate(fadeoutImage, transform.position, Quaternion.identity);
		spawnedObjects.Add(fadeout);
		yield return StartCoroutine(FadeOut(fadeout));
	}

	private IEnumerator AnimateCircle(GameObject circle, float delay)
	{
		yield return new WaitForSeconds(delay);

		SpriteRenderer sr = circle.GetComponent<SpriteRenderer>();
		Vector3 initialScale = Vector3.zero;
		Vector3 targetScale = Vector3.one * 2f;
		float elapsedTime = 0f;

		while (elapsedTime < circleFadeDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / circleFadeDuration;

			if (circle != null) // Null 체크
			{
				circle.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
				if (sr != null)
					sr.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
			}

			yield return null;
		}

		if (circle != null)
		{
			spawnedObjects.Remove(circle);
			Destroy(circle);
		}
	}

	private IEnumerator AnimateExpandingCircle(GameObject circle)
	{
		SpriteRenderer sr = circle.GetComponent<SpriteRenderer>();
		Vector3 initialScale = Vector3.zero;
		Vector3 targetScale = Vector3.one * expandMaxScale;
		float elapsedTime = 0f;

		while (elapsedTime < expandDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / expandDuration;

			if (circle != null) // Null 체크
			{
				circle.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
				if (sr != null)
					sr.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
			}

			yield return null;
		}

		if (circle != null)
		{
			spawnedObjects.Remove(circle);
			Destroy(circle);
		}
	}

	private IEnumerator FadeOut(GameObject fade)
	{
		float elapsedTime = 0f;
		float fadeDuration = 1f;

		SpriteRenderer fadeSpriteRender = fade.GetComponent<SpriteRenderer>();
		fadeSpriteRender.sortingOrder = 10;
		Color startColor = new Color(0f, 0f, 0f, 0f);
		Color endColor = new Color(0f, 0f, 0f, 1f);

		// FadeOut 애니메이션 진행 (화면이 검어질 때까지)
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / fadeDuration;

			if (fade != null && fadeSpriteRender != null) // Null 체크
			{
				fadeSpriteRender.color = Color.Lerp(startColor, endColor, t);
			}

			yield return null;
		}

		// 화면이 완전히 검어진 후 모든 오브젝트 제거
		foreach (GameObject obj in spawnedObjects.ToArray())
		{
			if (obj != null) // Null 체크
			{
				Destroy(obj);
			}
		}
		spawnedObjects.Clear(); // 리스트 초기화

		stage++;
		SceneManager.ChangeScene(SceneStage.Lobby); // 씬 전환
		GameController.PluseStage();
		entity.OffActive();
		gameObject.SetActive(false);
	}

	private Vector2 BezierCurve(Vector2 startPos, Vector2 controlPoint, Vector2 targetPos, float t)
	{
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;

		Vector2 p = uu * startPos;
		p += 2 * u * t * controlPoint;
		p += tt * targetPos;

		return p;
	}

	[SerializeField] private GameObject circlePrefab;
	[SerializeField] private GameObject expandingCirclePrefab;
	[SerializeField] private GameObject notePrefab;
	[SerializeField] private GameObject fadeoutImage;
	private float jumpHeight = 2f;
	private float jumpDuration = 0.5f;
	private float rotationSpeed = 360f;
	private int circleCount = 3;
	private float circleFadeDuration = 1f;
	private float expandDuration = 0.8f;
	private float expandMaxScale = 3f;
	private float noteSpawnInterval = 0.1f;
	private float noteLifetime = 2.5f;
	private float noteShrinkSpeed = 1f;
	private float moveRotationSpeed = 180f;
	private bool useScaleAnimation = true;
	private float scaleMin = 0.5f;
	private float scaleMax = 1.5f;
	private float scaleFrequency = 2f;
	private float circleWaveDelay = 0.2f;
	private float noteScaleVariation = 0.2f;

	private bool isTriggered = false;
	private bool isMoving = false;
	Stage stage = Stage.Stage1;
	public BaseGameEntity entity;
}