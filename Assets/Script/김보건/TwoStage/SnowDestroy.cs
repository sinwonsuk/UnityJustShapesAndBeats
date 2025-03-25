using UnityEngine;

public class SnowDestroy : MonoBehaviour
{
    private void OnBecameInvisible()
    { 
        Destroy(transform.root.gameObject); 
    }
}
