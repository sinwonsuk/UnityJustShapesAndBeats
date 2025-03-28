using UnityEngine;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class NextSceneAnimation : MonoBehaviour
{
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
			Vector3 spawnPosition = transform.position;
			for (int i = 0; i < circleCount; i++)
			{
				GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
				StartCoroutine(AnimateCircle(circle, i * circleWaveDelay));
			}
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

			// Bezier 곡선 계산
			float curveT = Mathf.Sin(t * Mathf.PI * 0.5f); // Sin 함수를 사용해 더 자연스럽게 곡선 그리기
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
			note.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t * noteShrinkSpeed) + Vector3.one * scaleVariation;

			if (sr != null)
				sr.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));

			yield return null;
		}

		Destroy(note);
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
		StartCoroutine(AnimateExpandingCircle(expandingCircle));

		yield return new WaitForSeconds(expandDuration * 0.5f);

		for (int i = 0; i < circleCount; i++)
		{
			GameObject circle = Instantiate(circlePrefab, startPos, Quaternion.identity);
			StartCoroutine(AnimateCircle(circle, i * circleWaveDelay));
		}
		GameObject fadeout = Instantiate(fadeoutImage, transform.position, Quaternion.identity);
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
			circle.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

			if (sr != null)
				sr.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));

			yield return null;
		}

		Destroy(circle);
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
			circle.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

			if (sr != null)
				sr.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));

			yield return null;
		}

		Destroy(circle);
	}

	private IEnumerator FadeOut(GameObject fade)
	{
		float elapsedTime = 0f;
		float fadeDuration = 1f;

		SpriteRenderer fadeSpriteRender = fade.GetComponent<SpriteRenderer>();
		fadeSpriteRender.sortingOrder = 10;
		Color startColor = new Color(0f, 0f, 0f, 0f);
		Color endColor = new Color(0f, 0f, 0f, 1f);

		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / fadeDuration;
			fadeSpriteRender.color = Color.Lerp(startColor, endColor, t);
			yield return null;
		}
		stage++;
        //씬 전환 로직
        SceneManager.ChangeScene(SceneStage.Lobby);
		GameController.PluseStage();
		entity.OffActive();
		gameObject.SetActive(false);
	}
	private Vector2 BezierCurve(Vector2 startPos, Vector2 controlPoint, Vector2 targetPos, float t)
	{
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;

		Vector2 p = uu * startPos; // (1 - t)^2 * startPos
		p += 2 * u * t * controlPoint; // 2 * (1 - t) * t * controlPoint
		p += tt * targetPos; // t^2 * targetPos

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