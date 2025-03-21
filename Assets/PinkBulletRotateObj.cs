using System.Runtime.CompilerServices;
using UnityEngine;

public class PinkBulletRotateObj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, angleSpeed*Time.deltaTime);
    }

    [SerializeField]
    private float angleSpeed = 10;
}
