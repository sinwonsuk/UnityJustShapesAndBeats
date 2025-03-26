using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakePattern
{
    public string patternName;
    public float shakeAmplitudeX = 2.0f;
    public float shakeFrequencyX = 4.0f;
    public float shakeAmplitudeY = 1.5f;
    public float shakeFrequencyY = 3.0f;
    public float shakeDuration = 0.5f;
}

public class PatternCameraShakeTrigger : MonoBehaviour
{
    public List<ShakePattern> shakePatterns = new List<ShakePattern>();
    public List<GameObject> triggerObjects = new List<GameObject>();

    private bool isShaking = false;
    private CinemachineCameraShake cineCameraShake;

    private void Awake()
    {
        cineCameraShake = FindObjectOfType<CinemachineCameraShake>();

        if (cineCameraShake == null)
        {
            Debug.LogError("CinemachineCameraShake �ν��Ͻ��� ã�� �� �����ϴ�. CinemachineCamera ������Ʈ�� �߰��Ǿ����� Ȯ���ϼ���.");
        }
    }

    private void TriggerShake(ShakePattern pattern)
    {
        if (cineCameraShake != null)
        {
            isShaking = true;
            cineCameraShake.ShakeCamera(
                pattern.shakeAmplitudeX,
                pattern.shakeFrequencyX,
                pattern.shakeAmplitudeY,
                pattern.shakeFrequencyY,
                pattern.shakeDuration
            );
        }
    }

    public void ActivateShake(string patternName, GameObject triggeringObject)
    {
        if (!triggerObjects.Contains(triggeringObject))
        {
            Debug.LogWarning($"'{triggeringObject.name}' �� Ʈ���� ������Ʈ ����Ʈ�� ���ԵǾ� ���� �ʽ��ϴ�.");
            return;
        }

        ShakePattern pattern = shakePatterns.Find(p => p.patternName == patternName);

        if (pattern != null)
        {
            TriggerShake(pattern);
        }
        else
        {
            Debug.LogWarning($"���� '{patternName}'�� ã�� �� �����ϴ�.");
        }
    }

    public void StopShake()
    {
        if (cineCameraShake != null && isShaking)
        {
            cineCameraShake.StopShake();
            isShaking = false;
        }
    }
}