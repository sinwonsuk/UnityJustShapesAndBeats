using UnityEngine;

public class Middle_sawtoothLeft : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10f;
  

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
