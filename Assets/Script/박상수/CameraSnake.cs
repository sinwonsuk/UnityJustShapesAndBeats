using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSnake : MonoBehaviour
{
    public CinemachineCamera cineCamera; // "Cinemachine Camera" 오브젝트를 드래그

    private CinemachineBasicMultiChannelPerlin noise;

    void Awake()
    {
        // 하위(혹은 자신)에 붙은 Noise 컴포넌트를 찾습니다.
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