using System.Collections;
using UnityEngine;

public class BigSnail : MonoBehaviour
{
	private void Awake()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		childTransforms = System.Array.FindAll(childTransforms, t => t != transform);

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
		float angle = Mathf.PingPong(Time.time * bodyRotationSpeed, 90f) - 45f; // 0 ~ 90 → -45 ~ 45
		transform.rotation = Quaternion.Euler(0f, 0f, angle);

		// 꼬리 회전 (부모를 중심으로 공전)
		if (waterTail != null)
		{
			waterTail.RotateAround(transform.position, Vector3.forward, -tailRotationSpeed * Time.deltaTime);
		}
	}

	private void Start()
	{
		// 불렛 발사 시작
		StartCoroutine(FireBullets());
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

	private IEnumerator FireBullets()
	{
		float currentAngle = 0f; // 회전 누적값
		float totalRotation = 0f; // 꼬리가 회전한 총 각도

		while (totalRotation < 720f) // 2바퀴(360 * 2)
		{
			if (waterTail != null && bulletPrefab != null)
			{
				float tailRotation = waterTail.eulerAngles.z;
				Quaternion bulletRotation = Quaternion.Euler(0f, 0f, tailRotation);

				// 불렛 생성
				Instantiate(bulletPrefab, waterTail.position, bulletRotation);

				// 꼬리 회전 각도 누적
				float rotationStep = tailRotationSpeed * bulletSpawnInterval;
				totalRotation += rotationStep;

				yield return new WaitForSeconds(bulletSpawnInterval);
			}
		}

		// 불렛 발사 종료 후 보스 사라짐
		StartCoroutine(DisappearBoss());
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
	[SerializeField] private float tailRotationSpeed = 380f; // 꼬리 회전 속도 (도/초)
	[SerializeField] private GameObject bulletPrefab; // 불렛 프리팹
	[SerializeField] private float bulletSpawnInterval = 0.1f; // 불렛 발사 간격

	private Transform[] childTransforms;
	private Transform waterTail;
}