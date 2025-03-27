using System.Collections;
using UnityEngine;

public class SmallSnail : MonoBehaviour
{
	private void Awake()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform); // ��ü�� ����

		foreach (Transform child in childTransforms)
		{
			if (child.gameObject.name == "Water_tail")
			{
				waterTail = child;
				break;
			}
		}
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
		foreach (Transform childTransform in childTransforms)
		{
			if (childTransform != null)
			{
				childTransform.localScale = scale * 1.4f;
			}
		}
	}

	private void Update()
	{
		// ���� ȸ�� (��ü �� ����)
		transform.Rotate(0f, 0f, -bodyRotationSpeed * Time.deltaTime);

		// ���� ȸ�� (�θ� �߽����� ����)
		if (waterTail != null)
		{
			waterTail.RotateAround(transform.position, Vector3.forward, -tailRotationSpeed * Time.deltaTime);
		}
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

	[SerializeField] private float growTime = 0.03f;
	[SerializeField] private float maxScale = 0.7f;
	[SerializeField] private float bodyRotationSpeed = 45f; // ���� ȸ�� �ӵ� (��/��)
	[SerializeField] private float tailRotationSpeed = 360f; // ���� ȸ�� �ӵ� (��/��, ���뺸�� ����)

	private Transform[] childTransforms;
	private Transform waterTail; // ���� Transform
}