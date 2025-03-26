using UnityEngine;

public class ObjectShakeTrigger : MonoBehaviour
{
   public string targetTag = "Enemy";  // 흔들림을 발생시킬 대상 태그 (예: Enemy)
    public float detectionRadius = 5.0f;  // 오브젝트와의 상호작용 반경
    public ShakePattern shakePattern;    // Inspector에서 설정할 흔들림 패턴

    private CinemachineCameraShake cameraShake;

    [System.Obsolete]
    private void Start()
    {
        cameraShake = FindObjectOfType<CinemachineCameraShake>();

        if (cameraShake == null)
        {
            Debug.LogError("CinemachineCameraShake 인스턴스를 찾을 수 없습니다. CinemachineCamera 오브젝트에 추가되었는지 확인하세요.");
        }
    }

    private void Update()
    {
        // 특정 태그를 가진 오브젝트 찾기
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);

            // 특정 반경 내에 있을 경우 흔들림 발생
            if (distance <= detectionRadius && cameraShake != null)
            {
                cameraShake.ShakeCamera(
                    shakePattern.shakeAmplitudeX,
                    shakePattern.shakeFrequencyX,
                    shakePattern.shakeAmplitudeY,
                    shakePattern.shakeFrequencyY,
                    shakePattern.shakeDuration
                );
            }
        }
    }
}