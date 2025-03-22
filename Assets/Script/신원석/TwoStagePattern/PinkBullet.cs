using UnityEngine;

public class PinkBullet : MonoBehaviour
{
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move * speed *Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    public void Move(Vector2 _move)
    {
        move = _move;
    }


    [SerializeField]
    float speed = 5.0f;

    Vector2 move;

}
