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
		yield return StartCoroutine(SpawnSequence(pos1, true));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(SpawnSequence(pos2, false));
	}

	IEnumerator SpawnSequence(Vector3 position, bool isFirst)
	{
		// �ٱ� �� 3�� ����
		GameObject[] outerCircles = new GameObject[3];
		for (int i = 0; i < 3; i++)
		{
			outerCircles[i] = Instantiate(dottedCirclePrefabs, position, Quaternion.identity);
			float scale = 1.0f + (i * 0.5f); // ũ�� �ٸ��� ����
			outerCircles[i].transform.localScale = Vector3.one * scale;
		}

		// ���� �� ���� �� �ִϸ��̼�
		GameObject innerCircle = Instantiate(bigCirclePrefabs, position, Quaternion.identity);
		SpriteRenderer innerRenderer = innerCircle.GetComponent<SpriteRenderer>();
		Color startColor = new Color(255 / 255f, 32 / 255f, 112 / 255f, 0.4f);
		float timer = 0f;
		float duration = 1.5f;

		while (timer < duration)
		{
			timer += Time.deltaTime;
			float t = timer / duration;
			float smoothT = Mathf.SmoothStep(0f, 1f, t);

			// ũ�� ��ȭ
			float scale = Mathf.Lerp(0.5f, 2f, smoothT);
			innerCircle.transform.localScale = Vector3.one * scale;

			// ���� ��ȭ
			innerRenderer.color = Color.Lerp(startColor, targetColor, smoothT);

			yield return null;
		}

		// �ٱ� �� ������ ����
		for (int i = 0; i < outerCircles.Length; i++)
		{
			if (outerCircles[i] != null)
			{
				Destroy(outerCircles[i]);
				yield return new WaitForSeconds(0.2f);
			}
		}

		timer = 0f;
		duration = 1f;
		Vector3 initialScale = innerCircle.transform.localScale;

		while (timer < duration)
		{
			timer += Time.deltaTime;
			float t = timer / duration;
			float smoothT = Mathf.SmoothStep(0f, 1f, t);

			if (isFirst)
			{
				float scale = Mathf.Lerp(2f, 4f, smoothT);
				innerCircle.transform.localScale = Vector3.one * scale;
			}
			else
			{
				float scale = Mathf.Lerp(2f, t < 0.5f ? 4f : 0.5f, smoothT);
				innerCircle.transform.localScale = Vector3.one * scale;
			}

			yield return null;
		}

		// �ҷ� 12�� ���� �߻�
		for (int i = 0; i < 12; i++)
		{
			float angle = i * 30f; // 360/12 = 30�� ����
			Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
			Vector3 direction = rotation * Vector3.up;
			GameObject bullet = Instantiate(bulletPrefabs, position, rotation);
		}

		// ���� �� ����
		Destroy(innerCircle);
	}

	[SerializeField] GameObject dottedCirclePrefabs;  // ��Ʈ �� ������
	[SerializeField] GameObject bigCirclePrefabs;     // ū �� ������
	[SerializeField] GameObject bulletPrefabs;        // �Ѿ� ������
	[SerializeField] float bulletSpeed = 5f;

	private Vector3 pos1 = new Vector3(5.18f, 2.17f, 0f);
	private Vector3 pos2 = new Vector3(-4.66f, -2.08f, 0f);
	private Color targetColor = new Color(255 / 255f, 209 / 255f, 245 / 255f, 0.4f);
}
