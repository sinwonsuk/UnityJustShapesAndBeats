using UnityEngine;
using System.Collections;

public enum T
{
	One,    // ������ ������ ���� (���� -> ������)
	Two,    // ������ ���� + ������ ���� (�����¿�)
	Three,  // ������ �β��� ���� ������ ���� (�����¿�)
	Four,   // ���� �� �پ� ���� (���� -> ������)
	Five,   // õõ�� ���� + ���� �� �پ� ����
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
		Debug.Log("BoxFadePatternSpawnManager ����");
		if (coroutine == null)
		{
			StartPattern(); // Ȱ��ȭ �� �ڷ�ƾ ����
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
		Debug.Log("BoxFadePatternSpawnManager ��Ȱ��ȭ");
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
			case T.Four:
				coroutine = StartCoroutine(SpawnPattern4());
				break;
			case T.Five:
				coroutine = StartCoroutine(SpawnPattern5());
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
		spawnInterval = 0.9f; // ���� �� ����
		float patternDuration = 1.7f; // BoxFadePattern2�� ��ü ���� �ð�
		float timeBeforeEnd = 0.1f; // ������ ������ 0.1�� ��

		while (true)
		{
			GameObject patternObj = SpawnPattern(pattern2Prefab);
			yield return new WaitForSeconds(patternDuration - timeBeforeEnd);
			patternObj = SpawnPattern(pattern2Prefab);
			yield return new WaitForSeconds(timeBeforeEnd);
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	private IEnumerator SpawnPattern3()
	{
		spawnInterval = 1f; // ���� �� ����
		float patternDuration = 3f; // BoxFadePattern3�� ��ü ���� �ð�
		float timeBeforeEnd = 0.1f; // ������ ������ 0.1�� ��

		while (true)
		{
			GameObject patternObj = SpawnPattern(pattern3Prefab);
			yield return new WaitForSeconds(patternDuration - timeBeforeEnd);
			patternObj = SpawnPattern(pattern3Prefab);
			yield return new WaitForSeconds(timeBeforeEnd);
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	private IEnumerator SpawnPattern4()
	{
		spawnInterval = 2f;
		while (true)
		{
			SpawnPattern(pattern1Prefab);
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	private IEnumerator SpawnPattern5()
	{
		spawnInterval = 2.8f; // ���� �� ����
		float patternDuration = 3f;
		float timeBeforeEnd = 0.1f; // ������ ������ 0.1�� ��

		while (true)
		{
			GameObject patternObj = SpawnPattern(pattern4Prefab);
			yield return new WaitForSeconds(patternDuration - timeBeforeEnd);
			patternObj = SpawnPattern(pattern4Prefab);
			yield return new WaitForSeconds(timeBeforeEnd);
			yield return new WaitForSeconds(spawnInterval);
		}
	}


	// ���� ���� �޼���
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
	[SerializeField] private GameObject pattern4Prefab;

	private float spawnInterval;
}