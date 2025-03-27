using UnityEngine;

public class Vortex_Missile : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); 
    }

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 5f;
}
