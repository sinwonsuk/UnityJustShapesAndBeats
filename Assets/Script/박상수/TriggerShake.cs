using UnityEngine;

public class TriggerShake : MonoBehaviour
{
    public CameraSnake cameraShakeScript; // CameraShake 스크립트를 참조

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그나 레이어 등을 체크하여 원하는 객체만 흔들리게
        if (other.CompareTag("Player"))
        {
            // 카메라 흔들기 호출
            cameraShakeScript.ShakeCamera(3f, 5f, 1f);
        }
    }
}
