using System.Collections;
using UnityEngine;

public class MoveUpBar : MonoBehaviour
{
    [SerializeField] private GameObject mapObj;

    [SerializeField] private Vector3 startPos; // 시작 위치 (인스펙터에서 설정)
    [SerializeField] private Vector3 endPos;   // 목표 위치 (인스펙터에서 설정)

    [SerializeField] private float startTime = 1f; // 시작 대기 시간
    [SerializeField] private float moveDuration = 2f; // 이동하는 데 걸리는 시간

    void Start()
    {
        mapObj.transform.position = startPos;
        // 코루틴 실행 (인스펙터에서 설정한 위치 사용)
        StartCoroutine(MoveUp(mapObj, startTime, moveDuration, startPos, endPos));
    }

    private IEnumerator MoveUp(GameObject obj, float startTime, float duration, Vector3 startPos, Vector3 targetPos)
    {
        yield return new WaitForSeconds(startTime);

        obj.transform.position = startPos;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            obj.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        obj.transform.position = targetPos;
    }

}
