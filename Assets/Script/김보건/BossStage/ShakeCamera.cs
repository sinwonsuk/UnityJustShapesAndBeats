using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(ShakeCycle());
    }

    IEnumerator ShakeCycle()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            startShakeCo = StartCoroutine(StartShake());
            yield return new WaitForSeconds(4f); 
    
            StopCoroutine(startShakeCo);
            yield return new WaitForSeconds(4f); 
        }
    }

    IEnumerator StartShake()
    {
        while (true)
        {
            impulseSource.GenerateImpulse(Vector2.up * shakeForce);
            yield return new WaitForSeconds(shakeInterval);
        }
    }

    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float shakeInterval = 0.2f;
    [SerializeField] private float shakeForce = 1f;

    [SerializeField] private Coroutine startShakeCo;
}
