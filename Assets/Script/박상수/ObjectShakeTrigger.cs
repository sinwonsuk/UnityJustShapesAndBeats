using UnityEngine;

public class ObjectShakeTrigger : MonoBehaviour
{
   public string targetTag = "Enemy";  // ��鸲�� �߻���ų ��� �±� (��: Enemy)
    public float detectionRadius = 5.0f;  // ������Ʈ���� ��ȣ�ۿ� �ݰ�
    public ShakePattern shakePattern;    // Inspector���� ������ ��鸲 ����

    private CinemachineCameraShake cameraShake;

    [System.Obsolete]
    private void Start()
    {
        cameraShake = FindObjectOfType<CinemachineCameraShake>();

        if (cameraShake == null)
        {
            Debug.LogError("CinemachineCameraShake �ν��Ͻ��� ã�� �� �����ϴ�. CinemachineCamera ������Ʈ�� �߰��Ǿ����� Ȯ���ϼ���.");
        }
    }

    private void Update()
    {
        // Ư�� �±׸� ���� ������Ʈ ã��
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);

            // Ư�� �ݰ� ���� ���� ��� ��鸲 �߻�
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