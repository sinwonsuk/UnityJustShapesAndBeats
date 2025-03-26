using System.Collections;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float scaleTarget = 2f;         // 자식이 최종적으로 도달할 스케일
    public float scaleUpDuration = 2f;       // 스케일업에 걸리는 시간
    public float scaleDownDuration = 0.5f;   // 스케일다운에 걸리는 시간 (빠르게)

    private void Start()
    {
        Debug.Log($"{gameObject.name} - ScaleUp 시작됨");
        StartCoroutine(ScaleUpAndDown());
    }

    IEnumerator ScaleUpAndDown()
    {
        // 스케일업 단계
        Vector3 initialScale = transform.localScale;
        // 3D 오브젝트의 경우 모든 축에 targetScale 적용 (2D면 z값은 1로 해도 무관)
        Vector3 maxScale = new Vector3(scaleTarget, scaleTarget, scaleTarget);
        float elapsedTime = 0f;
        while (elapsedTime < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, maxScale, elapsedTime / scaleUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = maxScale;
        Debug.Log($"{gameObject.name} - Scale업 완료");

        // 스케일다운 단계 (빠르게 작아짐)
        elapsedTime = 0f;
        Vector3 minScale = Vector3.zero;
        while (elapsedTime < scaleDownDuration)
        {
            transform.localScale = Vector3.Lerp(maxScale, minScale, elapsedTime / scaleDownDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = minScale;
        Debug.Log($"{gameObject.name} - Scale다운 완료, 삭제됨");
        Destroy(gameObject);
    }
}