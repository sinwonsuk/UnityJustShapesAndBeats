using UnityEngine;

public class Vortex_Missile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // 일정 시간 후 파괴
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // 정면 방향으로 이동
    }
}
