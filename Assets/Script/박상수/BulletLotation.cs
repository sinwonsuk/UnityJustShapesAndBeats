using UnityEngine;

public class BulletLotation : MonoBehaviour
{
    [SerializeField]
 private float speed = 10;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    } 
}

