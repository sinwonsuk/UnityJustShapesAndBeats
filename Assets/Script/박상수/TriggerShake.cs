using UnityEngine;

public class TriggerShake : MonoBehaviour
{
    public CameraSnake cameraShakeScript; // CameraShake ��ũ��Ʈ�� ����

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �±׳� ���̾� ���� üũ�Ͽ� ���ϴ� ��ü�� ��鸮��
        if (other.CompareTag("Player"))
        {
            // ī�޶� ���� ȣ��
            cameraShakeScript.ShakeCamera(3f, 5f, 1f);
        }
    }
}
