using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ParentController : MonoBehaviour
{
    public GameObject childPrefab;       // 자식 오브젝트 프리팹
    public float targetScale = 2f;       // 자식 오브젝트의 최종 크기
    public float scaleDuration = 2f;     // 크기 커지는 시간
    public float intervalBetweenChildren = 1f; // 자식 실행 간격
    internal bool isFinished;

    public void StartChildEffect()  // 🔥 여기 수정 (private → public)
    {
        StartCoroutine(SpawnAndControlChild());
    }

    IEnumerator SpawnAndControlChild()
    {
        // 🔥 자식 오브젝트 생성
        GameObject childObj = Instantiate(childPrefab, transform);
        childObj.transform.localPosition = Vector3.zero;

        // 🔥 자식 오브젝트의 이름 설정
        childObj.name = $"Child_{gameObject.name}";

        // 🔥 자식 오브젝트 크기 증가 기능 추가
        ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
        scaleScript.targetScale = targetScale;
        scaleScript.duration = scaleDuration;

        // 🔥 자식이 다 커질 때까지 대기
        yield return new WaitForSeconds(scaleDuration + intervalBetweenChildren);
    }
}




