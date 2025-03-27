using UnityEngine;

public class Invisible : MonoBehaviour
{
    
    private void Start()
    {
        
        
    }

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * 10);
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
