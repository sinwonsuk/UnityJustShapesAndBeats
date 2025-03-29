
using UnityEngine;
using System.Collections;

public class boss_ssoajigi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        while (true) 
        {
            transform.Translate(Vector2.left * Time.deltaTime * 5);
            yield return null; 
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}