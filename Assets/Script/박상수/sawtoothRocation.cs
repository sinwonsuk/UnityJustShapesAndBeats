using UnityEngine;

public class sawtoothRocation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;
   

  
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
