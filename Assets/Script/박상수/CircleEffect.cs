using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ParentController : MonoBehaviour
{
    [SerializeField]
    private GameObject childPrefab;       // Inspector에 할당된 자식 프리팹
    [SerializeField]
    private float delayBeforeChild = 4f;    // 부모 생성 후 자식 생성 전 딜레이 (초)
    [SerializeField]
    private float targetScale = 2f;         // 자식의 최종 스케일
    [SerializeField]
    private float scaleUpDuration = 2f;     // 자식 스케일업 시간
    [SerializeField]
    private float scaleDownDuration = 0.5f; // 자식 스케일다운 시간 (빠르게)
    internal bool isFinished;

    private void Start()
    {
        StartCoroutine(SpawnChildAndDeleteParent());
    }

    IEnumerator SpawnChildAndDeleteParent()
    {
        Debug.Log($"[ParentController] {gameObject.name} 시작됨.");
        yield return new WaitForSeconds(delayBeforeChild);

        if (childPrefab == null)
        {
            Debug.LogError("ChildPrefab이 할당되지 않았습니다.");
            yield break;
        }

        // 자식 오브젝트를 부모의 위치에서 생성하고, 부모와의 관계를 해제하여 독립시키기
        GameObject childObj = Instantiate(childPrefab, transform.position, Quaternion.identity);
        childObj.transform.SetParent(null);
        childObj.name = $"Child_{gameObject.name}";
        Debug.Log($"[ParentController] {gameObject.name} 자식 생성됨: {childObj.name}");

        // 자식에 ScaleUp 효과 스크립트 추가
        ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
        scaleScript.scaleTarget = targetScale;
        scaleScript.scaleUpDuration = scaleUpDuration;
        scaleScript.scaleDownDuration = scaleDownDuration;

        // 자식 생성 직후 부모 삭제
        Debug.Log($"[ParentController] {gameObject.name} 즉시 삭제됨.");
        Destroy(gameObject);
    }
}
