using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class CircleEffect : MonoBehaviour
{
    public GameObject newObjectPrefab; // 변경할 오브젝트 프리팹
    public float targetScale = 2f;     // 최종 크기
    public float scaleDuration = 2f;   // 크기 커지는 시간
    public float replaceDelay = 4f;    // 변경 시간 (4초 후)

    void Start()
    {
        Invoke(nameof(ReplaceObject), replaceDelay); // 4초 후 오브젝트 변경
    }

    void ReplaceObject()
    {
        if (newObjectPrefab != null)
        {
            GameObject newObj = Instantiate(newObjectPrefab, transform.position, Quaternion.identity);

            // 새 오브젝트에 ScaleUp 스크립트 추가 후 크기 증가 시작
            ScaleUp scaler = newObj.AddComponent<ScaleUp>();
            scaler.targetScale = targetScale;
            scaler.duration = scaleDuration;
        }

        Destroy(gameObject); // 기존 오브젝트 삭제
    }
}



