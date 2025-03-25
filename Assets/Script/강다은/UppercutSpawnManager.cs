using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutSpawnManager : MonoBehaviour
{
	private List<float> activeXPositions = new List<float>(); // 현재 활성된 UpperCut의 X 위치 추적
	private const float minDistance = 2f; // X축 최소 간격

	private void OnEnable()
	{
		Debug.Log(gameObject.name);
		activeXPositions.Clear();
		InvokeRepeating("UppercutSpawnPattern", 2f, 0.8f); // 2초 후 0.8초 간격으로 스폰
	}

	private void OnDisable()
	{
		CancelInvoke("UppercutSpawnPattern");
		activeXPositions.Clear();
		Debug.Log("SpawnManager 비활성화");
	}

	private void UppercutSpawnPattern()
	{
		GameObject go = Instantiate(upperCutPattern);
		go.name = "UpperCutInstance";

		UpperCut upperCut = go.GetComponent<UpperCut>();
		float randomX = GetUniqueRandomX(); // 겹치지 않는 X 위치 계산
		upperCut.SetInitialX(randomX); // UpperCut에 초기 X 위치 설정
		upperCut.StartPattern();

		activeXPositions.Add(randomX); // X 위치 기록
		StartCoroutine(RemoveXPositionAfterDestroy(go, randomX));
	}

	private float GetUniqueRandomX()
	{
		float randomX;
		int maxAttempts = 10; // 최대 시도 횟수
		float xRangeMin = -6f; // X축 범위 최소
		float xRangeMax = 6f;  // X축 범위 최대

		for (int i = 0; i < maxAttempts; i++)
		{
			randomX = Random.Range(xRangeMin, xRangeMax);
			bool isTooClose = false;

			foreach (float existingX in activeXPositions)
			{
				if (Mathf.Abs(randomX - existingX) < minDistance)
				{
					isTooClose = true;
					break;
				}
			}

			if (!isTooClose)
			{
				return randomX;
			}
		}

		// 시도 횟수를 초과하면 강제로 위치 조정
		randomX = Random.Range(xRangeMin, xRangeMax);
		return AdjustXToAvoidOverlap(randomX);
	}

	private float AdjustXToAvoidOverlap(float proposedX)
	{
		float adjustedX = proposedX;
		foreach (float existingX in activeXPositions)
		{
			if (Mathf.Abs(adjustedX - existingX) < minDistance)
			{
				adjustedX += (adjustedX < existingX) ? -minDistance : minDistance;
				adjustedX = Mathf.Clamp(adjustedX, -6f, 6f); // 범위 제한
			}
		}
		return adjustedX;
	}

	private IEnumerator RemoveXPositionAfterDestroy(GameObject go, float xPos)
	{
		yield return new WaitUntil(() => go == null);
		activeXPositions.Remove(xPos);
	}

	[SerializeField]
	private GameObject upperCutPattern;
}