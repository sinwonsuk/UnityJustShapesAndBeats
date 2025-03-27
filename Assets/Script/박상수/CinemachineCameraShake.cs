using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraShake : MonoBehaviour
{
    public static CinemachineCameraShake Instance;

    private Transform cameraTransform;
    private Vector3 initialPosition;

    private float shakeDuration;
    private float shakeElapsedTime;

    private float shakeAmplitudeX;
    private float shakeFrequencyX;
    private float shakeAmplitudeY;
    private float shakeFrequencyY;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        cameraTransform = GetComponent<Transform>();
        initialPosition = cameraTransform.localPosition;  // 초기 위치 저장
    }

    private void Update()
    {
        if (shakeElapsedTime > 0)
        {
            shakeElapsedTime -= Time.deltaTime;

            // 흔들림 계산
            float offsetX = Mathf.Sin(Time.time * shakeFrequencyX) * shakeAmplitudeX;
            float offsetY = Mathf.Sin(Time.time * shakeFrequencyY) * shakeAmplitudeY;

            cameraTransform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0);

            if (shakeElapsedTime <= 0)
            {
                StopShake(); // 흔들림이 끝나면 자동으로 정지
            }
        }
    }

    public void ShakeCamera(float amplitudeX, float frequencyX, float amplitudeY, float frequencyY, float duration)
    {
        shakeAmplitudeX = amplitudeX;
        shakeFrequencyX = frequencyX;
        shakeAmplitudeY = amplitudeY;
        shakeFrequencyY = frequencyY;

        shakeDuration = duration;
        shakeElapsedTime = duration;
    }

    public void StopShake()  // 흔들림을 즉시 멈추기
    {
        shakeElapsedTime = 0; // 흔들림 지속 시간을 0으로 설정하여 흔들림 중단
        cameraTransform.localPosition = initialPosition; // 카메라 위치를 원래 위치로 되돌림
    }
}