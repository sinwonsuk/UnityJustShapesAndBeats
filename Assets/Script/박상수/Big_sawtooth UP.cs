using UnityEngine;

public class Big_sawtoothUp: MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
   

  
    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
