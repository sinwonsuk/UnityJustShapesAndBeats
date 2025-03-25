using UnityEngine;

public class Small_sawtooth : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    
    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
