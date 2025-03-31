using UnityEngine;

public class UglySpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(uglyObject, new Vector2(11f, 4.9f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    GameObject uglyObject;
}
