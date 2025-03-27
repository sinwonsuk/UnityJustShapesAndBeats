using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSnake : MonoBehaviour
{
    public CinemachineCamera cineCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    void Awake()
    {
        noise = cineCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }

    // �ݵ�� public���� ����
    public void ShakeCamera(float amplitude, float frequency, float duration)
    {
        StartCoroutine(Shake(amplitude, frequency, duration));
    }

    private IEnumerator Shake(float amplitude, float frequency, float duration)
    {
        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
}