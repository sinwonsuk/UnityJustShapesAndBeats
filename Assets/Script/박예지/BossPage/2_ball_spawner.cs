using UnityEngine;

public class ball_spawner2 : MonoBehaviour

{
    public GameObject Bbullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
