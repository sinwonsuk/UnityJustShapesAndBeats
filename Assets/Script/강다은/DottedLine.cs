using UnityEngine;

public class DottedLine : MonoBehaviour
{
	void Update()
	{
		// �ð� �������� ȸ��
		transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
	}


	[SerializeField] private float rotationSpeed = 90f; // �ʴ� ȸ�� �ӵ� (�⺻��: 90��)
}
