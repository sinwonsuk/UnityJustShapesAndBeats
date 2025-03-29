using Unity.Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    public void Shake(float force = 1f)
    {
        Vector3 randomDir = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        impulseSource.GenerateImpulse(randomDir * force);
    }
}
