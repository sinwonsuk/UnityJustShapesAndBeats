using UnityEngine;

public class Vortex_Missile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // ���� �ð� �� �ı�
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // ���� �������� �̵�
    }
}
