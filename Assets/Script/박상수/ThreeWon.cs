using System.Collections;
using UnityEngine;

public class ThreeWon : MonoBehaviour
{
    public GameObject childPrefab;               // 자식 오브젝트 프리팹
    public float targetScale = 2f;               // 자식 오브젝트의 최종 스케일
    public float scaleUpDuration = 2f;           // 스케일업에 걸리는 시간
    public float scaleDownDuration = 0.5f;       // 스케일다운에 걸리는 시간 (빠르게)
    public float intervalBetweenChildren = 1f;   // 자식 실행 간격
    internal bool isFinished;

    public void StartChildEffect()  // 외부에서 호출 가능하도록 public
    {
        StartCoroutine(SpawnAndControlChild());
    }

    IEnumerator SpawnAndControlChild()
    {
        if (childPrefab == null)
        {
            Debug.LogError("Child prefab is not assigned.");
            yield break;
        }

        // 자식 오브젝트 생성 및 초기화
        GameObject childObj = Instantiate(childPrefab, transform);
        childObj.transform.localPosition = Vector3.zero;
        childObj.name = $"Child_{gameObject.name}";

        // 자식 오브젝트에 ScaleUp 스크립트 추가 (수정된 스케일업/다운 효과)
        ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
        scaleScript.scaleTarget = targetScale;
        scaleScript.scaleUpDuration = scaleUpDuration;
        scaleScript.scaleDownDuration = scaleDownDuration;

        // 자식 효과 전체(스케일업 + 스케일다운) 시간과 다음 자식 생성 간격만큼 대기
        yield return new WaitForSeconds(scaleUpDuration + scaleDownDuration + intervalBetweenChildren);
    }
}