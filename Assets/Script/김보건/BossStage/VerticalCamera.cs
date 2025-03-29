using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class VerticalCamera : MonoBehaviour
{

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void OnEnable()
    {
        StartCoroutine(StartShake());
    }

    private IEnumerator StartShake()
    {
        while (true)
        {
            impulseSource.GenerateImpulse(Vector2.up * shakeForce);
            yield return new WaitForSeconds(shakeInterval);
        }
    }

    private CinemachineImpulseSource impulseSource;
    [SerializeField] private float shakeInterval = 0.2f;
    [SerializeField] private float shakeForce = 1f;
}
