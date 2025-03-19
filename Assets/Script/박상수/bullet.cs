using UnityEngine;

public class bullet : MonoBehaviour
{

    public float rotationSpeed = 200f; // 회전 속도 (값을 조정해보세요!)

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // 부드럽게 회전
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);  // 충돌 시 삭제
    }
}
