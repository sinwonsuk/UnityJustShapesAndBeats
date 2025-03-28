using System.Collections;
using UnityEngine;

public class DotShootSpawner : MonoBehaviour
{
	void OnEnable()
	{
		StartCoroutine(StartShootRoutine());
	}

	private IEnumerator StartShootRoutine()
	{
		yield return new WaitForSeconds(1.3f);
		Coroutine outer1 = StartCoroutine(SpawnOuterCircles(pos1));
		Coroutine outer2 = StartCoroutine(SpawnOuterCircles(pos2));

		yield return outer1;
		yield return outer2;

		yield return StartCoroutine(InnerSequence(pos1, true));
		yield return new WaitForSeconds(0.2f);
		yield return StartCoroutine(InnerSequence(pos2, false));
	}

	IEnumerator SpawnOuterCircles(Vector3 pos)
	{
		GameObject[] outerCircles = new GameObject[3];
		float[] scales = { 0.8f, 1.1f, 1.4f };

		for (int i = 0; i < 3; i++)
		{
			outerCircles[i] = Instantiate(dottedCirclePrefabs, pos, Quaternion.identity);
			outerCircles[i].transform.localScale = Vector3.one * scales[i];
			yield return new WaitForSeconds(0.2f);
		}

		OuterCircleStorage[pos] = outerCircles;
	}

	IEnumerator InnerSequence(Vector3 pos, bool isFirst)
	{
		GameObject[] outerCircles = OuterCircleStorage[pos];
		float[] outerScales = { 2f, 2.5f, 3f }; 
		Color startColor = new Color(255 / 255f, 80 / 255f, 159 / 255f, 1f); 
		Color targetColor = new Color(1f, 1f, 1f, 1f);

		GameObject inner = Instantiate(bigCirclePrefabs, pos, Quaternion.identity);
		SpriteRenderer innerRenderer = inner.GetComponent<SpriteRenderer>();
		float time = 0f;
		float duration = 0.2f;

		if (outerCircles[0] != null)
		{
			Destroy(outerCircles[0]);
		}

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;
			float smoothT = Mathf.SmoothStep(0f, 1f, t);
			inner.transform.localScale = Vector3.one * Mathf.Lerp(0.5f, outerScales[0], smoothT);
			innerRenderer.color = Color.Lerp(startColor, targetColor, smoothT); 
			yield return null;
		}

		time = 0f;
		duration = 0.2f;

		while (time < duration) // �۾���
		{
			time += Time.deltaTime;
			float t = time / duration;
			float smoothT = Mathf.SmoothStep(0f, 1f, t);
			inner.transform.localScale = Vector3.one * Mathf.Lerp(outerScales[0], 0.5f, smoothT);
			innerRenderer.color = Color.Lerp(targetColor, startColor, smoothT); 
			yield return null;
		}

		if (outerCircles[1] != null)
		{
			Destroy(outerCircles[1]);
		}

		time = 0f;
		duration = 0.2f;

		while (time < duration) 
		{
			time += Time.deltaTime;
			float t = time / duration;
			float smoothT = Mathf.SmoothStep(0f, 1f, t);
			inner.transform.localScale = Vector3.one * Mathf.Lerp(0.5f, outerScales[1], smoothT);
			innerRenderer.color = Color.Lerp(startColor, targetColor, smoothT);
			yield return null;
		}

		if (isFirst)
		{
			GameObject newInner = Instantiate(bigCirclePrefabs, pos, Quaternion.identity);
			SpriteRenderer newInnerRenderer = newInner.GetComponent<SpriteRenderer>();
			newInnerRenderer.color = startColor;

			if (outerCircles[2] != null)
			{
				Destroy(outerCircles[2]); 
			}

			time = 0f;
			duration = 0.2f;

			while (time < duration) 
			{
				time += Time.deltaTime;
				float t = time / duration;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				newInner.transform.localScale = Vector3.one * Mathf.Lerp(0.5f, outerScales[2], smoothT); 
				newInnerRenderer.color = Color.Lerp(startColor, targetColor, smoothT);
				yield return null;
			}

			// �ҷ� �߻�
			for (int i = 0; i < 12; i++)
			{
				float angle = i * 30f;
				Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
				Vector3 direction = rotation * Vector3.up;
				GameObject bullet = Instantiate(bulletPrefabs, pos, rotation);
			}

			time = 0f;
			duration = 0.2f;

			while (time < duration)
			{
				time += Time.deltaTime;
				float t = time / duration;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(outerScales[1], 0.5f, smoothT);
				innerRenderer.color = Color.Lerp(targetColor, startColor, smoothT);
				newInner.transform.localScale = Vector3.one * Mathf.Lerp(outerScales[2], 0.5f, smoothT);
				newInnerRenderer.color = Color.Lerp(targetColor, startColor, smoothT); 
				yield return null;
			}

			Destroy(inner);
			Destroy(newInner);
		}
		else
		{
			if (outerCircles[2] != null)
			{
				Destroy(outerCircles[2]); 
			}
			time = 0f;
			duration = 0.1f;

			while (time < duration) 
			{
				time += Time.deltaTime;
				float t = time / duration;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(outerScales[1], outerScales[2], smoothT);
				innerRenderer.color = Color.Lerp(startColor, targetColor, smoothT);
				yield return null;
			}

			time = 0f;
			duration = 0.1f;

			while (time < duration)
			{
				time += Time.deltaTime;
				float t = time / duration;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(outerScales[2], 0.5f, smoothT); 
				innerRenderer.color = Color.Lerp(targetColor, startColor, smoothT); 
				yield return null;
			}

			// �ҷ� �߻�
			for (int i = 0; i < 12; i++)
			{
				float angle = i * 30f;
				Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
				Vector3 direction = rotation * Vector3.up;
				GameObject bullet = Instantiate(bulletPrefabs, pos, rotation);
			}

			Destroy(inner);
			gameObject.SetActive(false);
		}
		
	}

	[SerializeField] GameObject dottedCirclePrefabs; 
	[SerializeField] GameObject bigCirclePrefabs;    
	[SerializeField] GameObject bulletPrefabs;        
	private static readonly System.Collections.Generic.Dictionary<Vector3, GameObject[]> OuterCircleStorage =
		new System.Collections.Generic.Dictionary<Vector3, GameObject[]>();

	private Vector3 pos1 = new Vector3(5.18f, 2.17f, 0f);
	private Vector3 pos2 = new Vector3(-4.66f, -2.08f, 0f);
}