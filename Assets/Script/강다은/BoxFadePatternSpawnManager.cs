using UnityEngine;
using System.Collections;

public enum T
{
	// 빠르게 가로줄 생성
	One,
	Two,
	Three
}


public class BoxFadePatternSpawnManager : MonoBehaviour, PatternChoiceInterface
{
	private Coroutine coroutine;

	public void SetPattern(int value)
	{
		select = (T)value;
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
	}

	private void OnEnable()
	{
		Debug.Log("BoxFadePatternSpawnManager 시작");
		if (coroutine == null)
		{
			StartPattern(); // 활성화 시 코루틴 시작
		}
	}

	private void OnDisable()
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
			coroutine = null;
		}
		select = T.One;
		Debug.Log("BoxFadePatternSpawnManager 비활성화");
	}

	private void StartPattern()
	{
		switch (select)
		{
			case T.One:
				coroutine = StartCoroutine(SpawnPattern1());
				break;
			case T.Two:
				coroutine = StartCoroutine(SpawnPattern2());
				break;
			case T.Three:
				coroutine = StartCoroutine(SpawnPattern3());
				break;
		}
	}

	private IEnumerator SpawnPattern1()
	{
		spawnInterval = 0.5f;
		while (true)
		{
			SpawnPattern(pattern1Prefab);
			yield return new WaitForSeconds(spawnInterval);
		}
	}
	private IEnumerator SpawnPattern2()
	{
		spawnInterval = 0.9f; // 패턴 간 간격
		float patternDuration = 1.7f; // BoxFadePattern2의 전체 지속 시간
		float timeBeforeEnd = 0.1f; // 패턴이 끝나기 0.1초 전

		while (true)
		{
			GameObject patternObj = SpawnPattern(pattern2Prefab);

			// 패턴이 끝나기 직전까지 대기
			yield return new WaitForSeconds(patternDuration - timeBeforeEnd);

			// 다음 패턴 스폰
			patternObj = SpawnPattern(pattern2Prefab);

			// 패턴이 완전히 끝날 때까지 대기
			yield return new WaitForSeconds(timeBeforeEnd);

			// 패턴 간 간격 대기
			yield return new WaitForSeconds(spawnInterval);
		}
	}
	private IEnumerator SpawnPattern3()
	{
		spawnInterval = 1f; // 패턴 간 간격
		float patternDuration = 3f; // BoxFadePattern3의 전체 지속 시간
		float timeBeforeEnd = 0.1f; // 패턴이 끝나기 0.1초 전

		while (true)
		{
			GameObject patternObj = SpawnPattern(pattern3Prefab);

			// 패턴이 끝나기 직전까지 대기
			yield return new WaitForSeconds(patternDuration - timeBeforeEnd);

			// 다음 패턴 스폰
			patternObj = SpawnPattern(pattern3Prefab);

			// 패턴이 완전히 끝날 때까지 대기
			yield return new WaitForSeconds(timeBeforeEnd);

			// 패턴 간 간격 대기
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	// 패턴 스폰 메서드
	private GameObject SpawnPattern(GameObject prefab)
	{
		GameObject patternObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
		Debug.Log($"Spawned: {patternObj.name} at (0, 0)");
		return patternObj;
	}

	[SerializeField] private T select = T.One;
	[SerializeField] private GameObject pattern1Prefab;
	[SerializeField] private GameObject pattern2Prefab;
	[SerializeField] private GameObject pattern3Prefab;

	private float spawnInterval;
}