using UnityEngine;

public class Invisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
