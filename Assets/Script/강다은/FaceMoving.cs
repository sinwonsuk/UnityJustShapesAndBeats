using UnityEngine;

public class FaceMoving : MonoBehaviour
{
	void Start()
	{
		// 초기 localPosition을 localOffset으로 설정
		if (localOffset == Vector2.zero)
			localOffset = transform.localPosition; // 기본적으로 현재 localPosition 사용
	}

	void Update()
	{
		// 타원 경로를 따라 움직이기
		angle += Time.deltaTime * speed;

		// 타원 경로에서 X, Y 계산 (부모 기준 상대적 위치)
		float x = localOffset.x + Mathf.Cos(angle) * horizontalRadius;
		float y = localOffset.y + Mathf.Sin(angle) * verticalRadius;

		// 위아래로 끄덕이는 효과 추가
		y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

		// localPosition 업데이트
		transform.localPosition = new Vector2(x, y);
	}

	// 중심 오프셋을 변경하는 함수 (필요 시 사용)
	public void SetLocalOffset(Vector2 newOffset)
	{
		localOffset = newOffset;
	}

	public float speed = 2f;             
	public float horizontalRadius = 3f;  
	public float verticalRadius = 5f;    
	public float bobbingSpeed = 1f;     
	public float bobbingAmount = 0.5f;  
	public Vector2 localOffset;         

	private float angle = 0f;          
}