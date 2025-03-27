using System.Collections;
using UnityEngine;

public class SmallBbababam : MonoBehaviour
{
	private Transform[] childTransforms;

	private void Awake()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform && t.gameObject.name != "Water_tail"); // ∫∏Ω∫ ∫ª√º, ≤ø∏Æ ¡¶ø‹
	}

	public void Initialize(GameObject spawnedObject)
	{
		transform.position = spawnedObject.transform.position;
		transform.localScale = Vector3.zero; // √ ±‚ ≈©±‚ 0
		StartCoroutine(AppearSlayerWithFadeCircle());
	}

	private void SetScale(Vector3 scale)
	{
		transform.localScale = scale;
		foreach (Transform childTransform in childTransforms)
		{
			if (childTransform != null)
			{
				childTransform.localScale = scale * 1.4f;
			}
		}
	}

	private IEnumerator AppearSlayerWithFadeCircle()
	{
		float time = 0f;

		// SmallBbababam µÓ¿Â: 0 °Ê 0.5
		time = 0f;
		while (time < growTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / growTime);
			Vector3 newScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, t);
			SetScale(newScale);
			yield return null;
		}
		SetScale(Vector3.one * 0.5f);

		// 1¬˜ ƒø¡¸: 0.5 °Ê maxScale
		time = 0f;
		while (time < growTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / growTime);
			Vector3 newScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one * maxScale, t);
			SetScale(newScale);
			yield return null;
		}
		SetScale(Vector3.one * maxScale);

		// 1¬˜ ¿€æ∆¡¸: maxScale °Ê 0.5
		time = 0f;
		while (time < shrinkTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / shrinkTime);
			float easeT = t * t; // ¿Ã¬° ¿˚øÎ
			Vector3 newScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * 0.5f, easeT);
			SetScale(newScale);
			yield return null;
		}
		SetScale(Vector3.one * 0.5f);

		// 2¬˜ ƒø¡¸: 0.5 °Ê maxScale
		time = 0f;
		while (time < growTime)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / growTime);
			Vector3 newScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one * maxScale, t);
			SetScale(newScale);
			yield return null;
		}
		SetScale(Vector3.one * maxScale);

		

		Destroy(gameObject);
	}

	[SerializeField] private float growTime = 0.03f;
	[SerializeField] private float shrinkTime = 0.5f;
	[SerializeField] private float maxScale = 0.7f;
	[SerializeField] private GameObject fadeCirclePrefab; // FadeCircle «¡∏Æ∆’
}