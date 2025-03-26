using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
  
    void Update()
    {
        angle += speed * Time.deltaTime; // ������ ���������� ����
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x,y, transform.position.z)+ center;
    }

    //public Transform center; // ȸ�� �߽�
    public float radius = 5f; // ������  
    private float angle = 0f;

    [SerializeField]
    Vector3 center;

    [SerializeField]
    float speed = -1.0f; 
}
