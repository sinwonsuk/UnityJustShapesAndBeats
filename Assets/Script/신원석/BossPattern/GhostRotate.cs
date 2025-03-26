using UnityEngine;

public class Ghost : MonoBehaviour
{

    void Start()
    {

    }
 
    void Update()
    {


        if(transform.localScale.x > 0.3)
        {
            transform.localScale -= new Vector3(localScale, localScale, localScale) * Time.deltaTime;
        }

        angle += speed * Time.deltaTime; // degree ������ ����
        float radian = angle * Mathf.Deg2Rad; // �������� ��ȯ
        float x = Mathf.Cos(radian) * radius;
        float y = Mathf.Sin(radian) * radius;
        transform.position = new Vector3(x,y, transform.position.z);
    }

    public float radius = 5f; // ������  
  
    [SerializeField]
    float localScale;

    [SerializeField]
    private float angle = 0f;

    [SerializeField]
    float speed = -1.0f; 
}
