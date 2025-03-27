using UnityEngine;

public class FaceMoving : MonoBehaviour
{
	void Start()
	{
		// �ʱ� localPosition�� localOffset���� ����
		if (localOffset == Vector2.zero)
			localOffset = transform.localPosition; // �⺻������ ���� localPosition ���
	}

	void Update()
	{
		// Ÿ�� ��θ� ���� �����̱�
		angle += Time.deltaTime * speed;

		// Ÿ�� ��ο��� X, Y ��� (�θ� ���� ����� ��ġ)
		float x = localOffset.x + Mathf.Cos(angle) * horizontalRadius;
		float y = localOffset.y + Mathf.Sin(angle) * verticalRadius;

		// ���Ʒ��� �����̴� ȿ�� �߰�
		y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

		// localPosition ������Ʈ
		transform.localPosition = new Vector2(x, y);
	}

	// �߽� �������� �����ϴ� �Լ� (�ʿ� �� ���)
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