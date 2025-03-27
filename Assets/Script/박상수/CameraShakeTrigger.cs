using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
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
            initialPosition = cameraTransform.localPosition;
        }

        private void Update()
        {
            if (shakeElapsedTime > 0)
            {
                shakeElapsedTime -= Time.deltaTime;

                float offsetX = Mathf.Sin(Time.time * shakeFrequencyX) * shakeAmplitudeX;
                float offsetY = Mathf.Sin(Time.time * shakeFrequencyY) * shakeAmplitudeY;

                cameraTransform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0);

                if (shakeElapsedTime <= 0)
                {
                    StopShake();
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

        public void StopShake()
        {
            shakeElapsedTime = 0;
            cameraTransform.localPosition = initialPosition;
        }
    }
}