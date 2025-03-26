using UnityEngine;

public class BossSquareRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed*Time.deltaTime);  
    }

    [SerializeField]
    private float speed = 5.0f;

}
