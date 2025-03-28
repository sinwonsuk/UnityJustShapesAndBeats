using UnityEngine;

public class UpDown : MonoBehaviour
{
    public Vector2 initialPosition = new Vector2(0, 0); // �ʱ� ��ġ (�ν����Ϳ��� ����)

    void Start()
    {
        // �ʱ� ��ġ ����
        transform.position = new Vector3(initialPosition.x, initialPosition.y, transform.position.z);
    }

    void Update()
    {
        // �Ʒ��� �̵�
        transform.Translate(Vector2.up * Time.deltaTime * 3);
    }
}
