using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSnake : MonoBehaviour
{
    public CinemachineCamera cineCamera; // "Cinemachine Camera" ������Ʈ�� �巡��

    private CinemachineBasicMultiChannelPerlin noise;

    void Awake()
    {
        // ����(Ȥ�� �ڽ�)�� ���� Noise ������Ʈ�� ã���ϴ�.
        noise = cineCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float amplitude, float frequency, float duration)
    {
        StartCoroutine(Shake(amplitude, frequency, duration));
    }

    private System.Collections.IEnumerator Shake(float amplitude, float frequency, float duration)
    {
        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
}