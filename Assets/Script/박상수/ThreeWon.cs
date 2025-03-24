using System.Collections;
using UnityEngine;

public class ThreeWon : MonoBehaviour
{
    public GameObject childPrefab;       // Inspector에 자식 프리팹 할당
    public float delayBeforeChild = 4f;    // 부모 생성 후 자식 생성 전 딜레이 (초)
    public float targetScale = 2f;         // 자식의 최종 스케일
    public float scaleDuration = 2f;       // 자식의 스케일업 시간
    public float childSpeed = 1f;          // 자식 작업 속도 조절 (1이면 기본)

    private IEnumerator Start()
    {
        Debug.Log($"[ParentController] {gameObject.name} 시작됨.");
        // delay 후 자식 생성 시작
        yield return new WaitForSeconds(delayBeforeChild);

        if (childPrefab == null)
        {
            Debug.LogError("ChildPrefab이 할당되지 않았습니다.");
            yield break;
        }

        // 자식 오브젝트 생성 (부모의 자식으로)
        GameObject childObj = Instantiate(childPrefab, transform);
        childObj.transform.localPosition = Vector3.zero;
        Debug.Log($"[ParentController] {gameObject.name} 자식 생성됨: {childObj.name}");

        // ScaleUp 스크립트 추가하여 자식 작업 실행
        ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
        scaleScript.targetScale = targetScale;
        scaleScript.duration = scaleDuration;
        scaleScript.speedMultiplier = childSpeed;

        // 자식 작업 완료 예상 시간만큼 대기 (버퍼 0.2초 추가)
        float waitTime = (scaleDuration / childSpeed) + 0.2f;
        yield return new WaitForSeconds(waitTime);

        Debug.Log($"[ParentController] {gameObject.name} 자식 작업 완료됨. 부모 삭제.");
        Destroy(gameObject); // 자식 작업이 끝나면 부모 오브젝트 삭제
    }
}
