using UnityEngine;

public class bullet : MonoBehaviour
{

    public float rotationSpeed = 200f; // ȸ�� �ӵ� (���� �����غ�����!)

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // �ε巴�� ȸ��
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);  // �浹 �� ����
    }
}
