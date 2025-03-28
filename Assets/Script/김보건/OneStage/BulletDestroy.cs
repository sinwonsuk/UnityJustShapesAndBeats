using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    public void Update()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }
}
