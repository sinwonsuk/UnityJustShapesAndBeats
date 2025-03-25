using UnityEngine;

public class SetPositionForAll : MonoBehaviour
{
    // 모든 프리팹이 이동할 목표 위치
    public Vector3 targetPosition;

    void Start()
    {
        // 부모 오브젝트의 자식들을 순회하면서 위치 설정
        foreach (Transform child in transform)
        {
            // 자식들의 위치를 동일한 목표 위치로 설정
            child.position = targetPosition;
        }
    }
}
