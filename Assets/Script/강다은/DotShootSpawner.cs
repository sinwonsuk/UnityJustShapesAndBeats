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

		// �� ��ġ�� �ٱ� �� ������ ���� ������ ���
		yield return outer1;
		yield return outer2;

		// pos1 ������ (���� ������)
		yield return StartCoroutine(InnerSequence(pos1, true));

		// 0.5�� ��� �� pos2 ������
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(InnerSequence(pos2, false));
	}

	// �ٱ� �� 3�� ���� ���� (���� �� �� �߰� �� �� ū ��)
	IEnumerator SpawnOuterCircles(Vector3 pos)
	{
		GameObject[] outerCircles = new GameObject[3];
		float[] scales = { 1f, 1.5f, 2f }; // ���� ��, �߰� ��, ū �� ũ��

		for (int i = 0; i < 3; i++)
		{
			outerCircles[i] = Instantiate(dottedCirclePrefabs, pos, Quaternion.identity);
			outerCircles[i].transform.localScale = Vector3.one * scales[i];
			yield return new WaitForSeconds(0.2f); // �� �� ���� ����
		}

		// �ٱ� �� �迭�� ���� �������� �����ϱ� ���� ����
		OuterCircleStorage[pos] = outerCircles;
	}

	// ���� �� �ִϸ��̼� �� ������ ������
	IEnumerator InnerSequence(Vector3 pos, bool isFirst)
	{
		GameObject[] outerCircles = OuterCircleStorage[pos];

		// ���� �� �ִϸ��̼� (�ʱ� Ŀ��)
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

		// �ٱ� �� ���� ����
		foreach (GameObject outer in outerCircles)
		{
			if (outer != null)
			{
				Destroy(outer);
				yield return new WaitForSeconds(0.2f);
			}
		}

		// ���� �� ������ �ִϸ��̼�
		time = 0f;
		duration = 1f;

		if (isFirst)
		{
			// ù ��° ��: Ŀ���⸸ ��
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
			// �� ��° ��: Ŀ���ٰ� �۾���
			time = 0f;
			while (time < 0.5f) // Ŀ��
			{
				time += Time.deltaTime;
				float t = time / 0.5f;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(2f, 4f, smoothT);
				yield return null;
			}

			time = 0f;
			while (time < 0.5f) // �۾���
			{
				time += Time.deltaTime;
				float t = time / 0.5f;
				float smoothT = Mathf.SmoothStep(0f, 1f, t);
				inner.transform.localScale = Vector3.one * Mathf.Lerp(4f, 0.5f, smoothT);
				yield return null;
			}
		}

		// �ҷ� 12�� ���� �߻�
		for (int i = 0; i < 12; i++)
		{
			float angle = i * 30f; // 360/12 = 30�� ����
			Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
			Vector3 direction = rotation * Vector3.up;
			GameObject bullet = Instantiate(bulletPrefabs, pos, rotation);
		}

		// ���� �� ����
		Destroy(inner);
	}

	[SerializeField] GameObject dottedCirclePrefabs;  // ��Ʈ �� ������
	[SerializeField] GameObject bigCirclePrefabs;     // ū �� ������
	[SerializeField] GameObject bulletPrefabs;        // �Ѿ� ������
	private static readonly System.Collections.Generic.Dictionary<Vector3, GameObject[]> OuterCircleStorage =
		new System.Collections.Generic.Dictionary<Vector3, GameObject[]>();

	private Vector3 pos1 = new Vector3(5.18f, 2.17f, 0f);
	private Vector3 pos2 = new Vector3(-4.66f, -2.08f, 0f);
	private Color targetColor = new Color(255 / 255f, 209 / 255f, 245 / 255f, 0.4f);
}
