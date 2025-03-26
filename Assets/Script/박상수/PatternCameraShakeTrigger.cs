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
            Debug.LogError("CinemachineCameraShake 인스턴스를 찾을 수 없습니다. CinemachineCamera 오브젝트에 추가되었는지 확인하세요.");
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
            Debug.LogWarning($"'{triggeringObject.name}' 은 트리거 오브젝트 리스트에 포함되어 있지 않습니다.");
            return;
        }

        ShakePattern pattern = shakePatterns.Find(p => p.patternName == patternName);

        if (pattern != null)
        {
            TriggerShake(pattern);
        }
        else
        {
            Debug.LogWarning($"패턴 '{patternName}'을 찾을 수 없습니다.");
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