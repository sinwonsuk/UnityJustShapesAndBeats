using UnityEngine;

public class BigPinkBallMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left*speed * Time.deltaTime);
    }

    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    [SerializeField]
    private float speed = 5.0f;
}
