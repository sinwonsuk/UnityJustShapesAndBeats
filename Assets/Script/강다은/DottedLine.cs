using UnityEngine;

public class DottedLine : MonoBehaviour
{
	void Update()
	{
		// 시계 방향으로 회전
		transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
	}


	[SerializeField] private float rotationSpeed = 90f; // 초당 회전 속도 (기본값: 90도)
}
