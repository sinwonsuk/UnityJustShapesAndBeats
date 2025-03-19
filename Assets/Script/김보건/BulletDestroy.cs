using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }
}
