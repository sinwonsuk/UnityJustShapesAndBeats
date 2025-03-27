using System.Collections;
using UnityEngine;

public class BossSmile : MonoBehaviour
{
	private void Awake()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform && t.gameObject.name == "Water_tail");

	}


	public void Initialize(GameObject spawnedObject)
	{
		transform.position = spawnedObject.transform.position;
		transform.localScale = Vector3.zero;
		StartCoroutine(AppearSlayer());
	}

	private void SetScale(Vector3 scale)
	{
		transform.localScale = scale;
	}

	private void Update()
	{
		float angle = Mathf.PingPong(Time.time * bodyRotationSpeed, 20f) - 10f; // 0 ~ 90 → -45 ~ 45
		transform.rotation = Quaternion.Euler(0f, 0f, angle);

	}

	private IEnumerator AppearSlayer()
	{
		float time = 0f;
		while (time < growTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / growTime);
			Vector3 newScale = Vector3.Lerp(Vector3.zero, Vector3.one * maxScale, t);
			SetScale(newScale);
			yield return null;
		}
		SetScale(Vector3.one * maxScale);
	}


	private IEnumerator DisappearBoss()
	{
		float time = 0f;
		float duration = 0.4f;
		Vector3 initialScale = transform.localScale;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;
			transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
			yield return null;
		}

		Destroy(gameObject); // 보스 삭제
	}



	[SerializeField] private float growTime = 0.03f;
	[SerializeField] private float maxScale = 1.0f;
	[SerializeField] private float bodyRotationSpeed = 45f; // 몸통 회전 속도 (도/초, 왕복 속도 조절)

	private Transform[] childTransforms;
}