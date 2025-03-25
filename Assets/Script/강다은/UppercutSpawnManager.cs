using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutSpawnManager : MonoBehaviour
{
	private List<float> activeXPositions = new List<float>(); // ���� Ȱ���� UpperCut�� X ��ġ ����
	private const float minDistance = 2f; // X�� �ּ� ����

	private void OnEnable()
	{
		Debug.Log(gameObject.name);
		activeXPositions.Clear();
		InvokeRepeating("UppercutSpawnPattern", 2f, 0.8f); // 2�� �� 0.8�� �������� ����
	}

	private void OnDisable()
	{
		CancelInvoke("UppercutSpawnPattern");
		activeXPositions.Clear();
		Debug.Log("SpawnManager ��Ȱ��ȭ");
	}

	private void UppercutSpawnPattern()
	{
		GameObject go = Instantiate(upperCutPattern);
		go.name = "UpperCutInstance";

		UpperCut upperCut = go.GetComponent<UpperCut>();
		float randomX = GetUniqueRandomX(); // ��ġ�� �ʴ� X ��ġ ���
		upperCut.SetInitialX(randomX); // UpperCut�� �ʱ� X ��ġ ����
		upperCut.StartPattern();

		activeXPositions.Add(randomX); // X ��ġ ���
		StartCoroutine(RemoveXPositionAfterDestroy(go, randomX));
	}

	private float GetUniqueRandomX()
	{
		float randomX;
		int maxAttempts = 10; // �ִ� �õ� Ƚ��
		float xRangeMin = -6f; // X�� ���� �ּ�
		float xRangeMax = 6f;  // X�� ���� �ִ�

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

		// �õ� Ƚ���� �ʰ��ϸ� ������ ��ġ ����
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
				adjustedX = Mathf.Clamp(adjustedX, -6f, 6f); // ���� ����
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