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
        angle += speed * Time.deltaTime; // 각도를 지속적으로 증가
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x,y, transform.position.z)+ center;
    }

    //public Transform center; // 회전 중심
    public float radius = 5f; // 반지름  
    private float angle = 0f;

    [SerializeField]
    Vector3 center;

    [SerializeField]
    float speed = -1.0f; 
}
