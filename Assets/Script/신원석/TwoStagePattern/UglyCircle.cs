using UnityEngine;

public class UglyCircle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Targetvector2 = new Vector2(0.79f, 0.18f);

        Vector2 position = new Vector2(transform.position.x,transform.position.y);

        Vector2 moveDir = Targetvector2 - position;

        moveDir.Normalize();

        if(transform.position.x > 0.79f)
        {
            transform.Translate(moveDir * speed * Time.deltaTime);
        }


    }

    [SerializeField]
    private float speed = 5.0f;

}
