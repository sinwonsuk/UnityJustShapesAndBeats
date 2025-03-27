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
		Coroutine outer1 = StartCoroutine(SpawnOuterCircles(pos1));
		Coroutine outer2 = StartCoroutine(SpawnOuterCircles(pos2));

		// 두 위치의 바깥 원 생성이 끝날 때까지 대기
		yield return outer1;
		yield return outer2;

		// pos1 시퀀스 (안쪽 원부터)
		yield return StartCoroutine(InnerSequence(pos1, true));

		// 0.5초 대기 후 pos2 시퀀스
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(InnerSequence(pos2, false));
	}

	// 바깥 원 3개 순차 생성 (작은 원 → 중간 원 → 큰 원)
	IEnumerator SpawnOuterCircles(Vector3 pos)
	{
		GameObject[] outerCircles = new GameObject[3];
		float[] scales = { 1f, 1.5f, 2f }; // 작은 원, 중간 원, 큰 원 크기

		for (int i = 0; i < 3; i++)
		{
			outerCircles[i] = Instantiate(dottedCirclePrefabs, pos, Quaternion.identity);
			outerCircles[i].transform.localScale = Vector3.one * scales[i];
			yield return new WaitForSeconds(0.2f); // 각 원 생성 간격
		}

		// 바깥 원 배열을 다음 시퀀스로 전달하기 위해 저장
		OuterCircleStorage[pos] = outerCircles;
	}

	// 안쪽 원 애니메이션 및 나머지 시퀀스
	IEnumerator InnerSequence(Vector3 pos, bool isFirst)
	{
		GameObject[] outerCircles = OuterCircleStorage[pos];

		// 안쪽 원 애니메이션 (초기 커짐)
		GameObject inner = Instantiate(bigCirclePrefabs, pos, Quaternion.identity);
		SpriteRenderer innerRenderer = inner.GetComponent<SpriteRenderer>();
		float time = 0f;
		float duration = 1.5f;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;
			float smoothT = Mathf.SmoothStep(0f, 1f, t);
			inner.transform.localScale = Vector3.one * Mathf.Lerp(0.5f, 2f, smoothT);
			innerRenderer.color = Color.Lerp(Color.white, Color.red, smoothT);
			yield return null;
		}

		// 바깥 원 순차 삭제
		foreach (GameObject outer in outerCircles)
		{
			if (outer != null)
			{
				Destroy(outer);
				yield return new WaitForSeconds(0.2f);
			}
		}

		// 안쪽 원 마지막 애니메이션
		time = 0f;
		duration = 1f;

		if (isFirst)
		{
			// 첫 번째 원: 커지기만 함
			while (time < duration)
			{
				time += Time.deltaTime;
				float t = time / duration;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(2f, 4f, smoothT);
				yield return null;
			}
		}
		else
		{
			// 두 번째 원: 커졌다가 작아짐
			time = 0f;
			while (time < 0.5f) // 커짐
			{
				time += Time.deltaTime;
				float t = time / 0.5f;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(2f, 4f, smoothT);
				yield return null;
			}

			time = 0f;
			while (time < 0.5f) // 작아짐
			{
				time += Time.deltaTime;
				float t = time / 0.5f;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(4f, 0.5f, smoothT);
				yield return null;
			}
		}

		// 불렛 12개 원형 발사
		for (int i = 0; i < 12; i++)
		{
			float angle = i * 30f; // 360/12 = 30도 간격
			Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
			Vector3 direction = rotation * Vector3.up;
			GameObject bullet = Instantiate(bulletPrefabs, pos, rotation);
		}

		// 안쪽 원 삭제
		Destroy(inner);
	}

	[SerializeField] GameObject dottedCirclePrefabs;  // 도트 원 프리팹
	[SerializeField] GameObject bigCirclePrefabs;     // 큰 원 프리팹
	[SerializeField] GameObject bulletPrefabs;        // 총알 프리팹
	private static readonly System.Collections.Generic.Dictionary<Vector3, GameObject[]> OuterCircleStorage =
		new System.Collections.Generic.Dictionary<Vector3, GameObject[]>();

	private Vector3 pos1 = new Vector3(5.18f, 2.17f, 0f);
	private Vector3 pos2 = new Vector3(-4.66f, -2.08f, 0f);
	private Color targetColor = new Color(255 / 255f, 209 / 255f, 245 / 255f, 0.4f);
}
