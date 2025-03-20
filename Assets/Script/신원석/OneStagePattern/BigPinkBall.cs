using UnityEngine;

public class BigPinkBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotateZ = Time.deltaTime * speed;

        transform.Rotate(0, 0, rotateZ);
    }

    private float speed = 300.0f;

}
