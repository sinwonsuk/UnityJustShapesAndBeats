using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossSpawner : MonoBehaviour
{
	private List<Vector3> usedPositions = new List<Vector3>();
	private List<GameObject> spawnedObjects = new List<GameObject>();
	private List<GameObject> fadeCircles = new List<GameObject>();

	private void OnEnable()
	{
		StartCoroutine(SpawnSequence());
	}

	private IEnumerator SpawnSequence()
	{
		usedPositions.Clear();
		spawnedObjects.Clear();
		fadeCircles.Clear();

		// 첫 번째 FadeCircle 스폰
		Vector3 position1 = GetNonOverlappingSpawnPosition();
		GameObject fadeCircle1 = Instantiate(fadeCirclePrefab, position1, Quaternion.identity);
		usedPositions.Add(position1);
		fadeCircles.Add(fadeCircle1);

		yield return new WaitForSeconds(0.5f);

		// 두 번째 FadeCircle 스폰
		Vector3 position2 = GetNonOverlappingSpawnPosition();
		GameObject fadeCircle2 = Instantiate(fadeCirclePrefab, position2, Quaternion.identity);
		usedPositions.Add(position2);
		fadeCircles.Add(fadeCircle2);

		yield return new WaitForSeconds(0.5f);

		// 세 번째 FadeCircle 스폰
		Vector3 position3 = GetNonOverlappingSpawnPosition();
		GameObject fadeCircle3 = Instantiate(fadeCirclePrefab, position3, Quaternion.identity);
		usedPositions.Add(position3);
		fadeCircles.Add(fadeCircle3);

		yield return new WaitForSeconds(0.5f);

		// 네 번째 FadeCircle 스폰
		Vector3 position4 = GetNonOverlappingSpawnPosition();
		GameObject fadeCircle4 = Instantiate(fadeCirclePrefab, position4, Quaternion.identity);
		usedPositions.Add(position4);
		fadeCircles.Add(fadeCircle4);


		// 첫 번째 SmallBbababam 스폰
		Vector3 pos1 = usedPositions[0];
		GameObject bbabam1 = Instantiate(smallBbababamPrefab, pos1, Quaternion.identity);
		bbabam1.GetComponent<SmallBbababam>().Initialize(bbabam1);
		spawnedObjects.Add(bbabam1);
		if (fadeCircles[0] != null)
		{
			Destroy(fadeCircles[0]);
			fadeCircles[0] = null;
		}

		yield return new WaitForSeconds(0.5f);

		// 첫 번째 SmallSnail 스폰
		yield return StartCoroutine(ShrinkAndDestroy(bbabam1));
		Vector3 pos2 = usedPositions[1];
		GameObject snail1 = Instantiate(SmallSnailPrefab, pos2, Quaternion.identity);
		snail1.GetComponent<SmallSnail>().Initialize(snail1);
		spawnedObjects.Add(snail1);
		if (fadeCircles[1] != null)
		{
			Destroy(fadeCircles[1]);
			fadeCircles[1] = null;
		}

		yield return new WaitForSeconds(0.5f);

		// 두 번째 SmallBbababam 스폰
		yield return StartCoroutine(ShrinkAndDestroy(snail1));
		Vector3 pos3 = usedPositions[2];
		GameObject bbabam2 = Instantiate(smallBbababamPrefab, pos3, Quaternion.identity);
		bbabam2.GetComponent<SmallBbababam>().Initialize(bbabam2);
		spawnedObjects.Add(bbabam2);
		if (fadeCircles[2] != null)
		{
			Destroy(fadeCircles[2]);
			fadeCircles[2] = null;
		}

		yield return new WaitForSeconds(0.5f);

		// 두 번째 SmallSnail 스폰
		yield return StartCoroutine(ShrinkAndDestroy(bbabam2));
		Vector3 pos4 = usedPositions[3];
		GameObject snail2 = Instantiate(SmallSnailPrefab, pos4, Quaternion.identity);
		snail2.GetComponent<SmallSnail>().Initialize(snail2);
		spawnedObjects.Add(snail2);
		if (fadeCircles[3] != null)
		{
			Destroy(fadeCircles[3]);
			fadeCircles[3] = null;
		}

		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(ShrinkAndDestroy(snail2));
	}

	private IEnumerator ShrinkAndDestroy(GameObject obj)
	{
		if (obj == null) yield break; // 오브젝트가 이미 제거된 경우 종료

		float shrinkTime = 0.2f;
		float time = 0f;
		Transform objTransform = obj.transform; // Transform 캐싱
		Vector3 initialScale = objTransform.localScale;

		while (time < shrinkTime)
		{
			if (obj == null) yield break; // 도중에 제거된 경우 종료
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / shrinkTime);
			float easeT = t * t;
			objTransform.localScale = Vector3.Lerp(initialScale, Vector3.zero, easeT);
			yield return null;
		}

		if (obj != null)
		{
			objTransform.localScale = Vector3.zero;
			Destroy(obj);
		}
	}

	private Vector3 GetNonOverlappingSpawnPosition()
	{
		Vector3 randomPosition;
		int attempts = 0;
		const int maxAttempts = 10;
		const float minDistance = 2f;

		do
		{
			float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
			float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
			randomPosition = new Vector3(x, y, 0f);
			attempts++;

		} while (IsOverlapping(randomPosition, minDistance));

		return randomPosition;
	}

	private bool IsOverlapping(Vector3 position, float minDistance)
	{
		foreach (Vector3 usedPos in usedPositions)
		{
			if (Vector3.Distance(position, usedPos) < minDistance)
			{
				return true;
			}
		}
		return false;
	}

	[SerializeField] private GameObject smallBbababamPrefab;
	[SerializeField] private GameObject SmallSnailPrefab;
	[SerializeField] private GameObject fadeCirclePrefab;
	[SerializeField] private Vector2 spawnAreaMin = new Vector2(-5f, -2f);
	[SerializeField] private Vector2 spawnAreaMax = new Vector2(5f, 1f);
}