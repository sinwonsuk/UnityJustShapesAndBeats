using UnityEngine;

public class SetPositionForAll : MonoBehaviour
{
    // ��� �������� �̵��� ��ǥ ��ġ
    public Vector3 targetPosition;

    void Start()
    {
        // �θ� ������Ʈ�� �ڽĵ��� ��ȸ�ϸ鼭 ��ġ ����
        foreach (Transform child in transform)
        {
            // �ڽĵ��� ��ġ�� ������ ��ǥ ��ġ�� ����
            child.position = targetPosition;
        }
    }
}
