using System.Collections;
using UnityEngine;

public class ShakeBar : MonoBehaviour
{
    [SerializeField] private float interval = 0.7f; // 시작 대기 시간
    [SerializeField] private float startTime = 0.3f; // 시작 대기 시간

    void OnEnable()
    {
        StartCoroutine(MoveUpDown(gameObject, 0.1f, 0.2f, interval, startTime));
    }

    void Update()
    {
        
    }

    private IEnumerator MoveUpDown(GameObject obj, float duration, float moveAmount, float interval, float startTime)
    {
        Vector3 originalPos = obj.transform.position;
        Vector3 upPos = originalPos + new Vector3(0f, moveAmount, 0f);

        yield return new WaitForSeconds(startTime);

        while (true)
        {
            // 위로 이동
            yield return LerpMovement(obj, duration, originalPos, upPos);

            // 아래로 이동 (원래 위치)
            yield return LerpMovement(obj, duration, upPos, originalPos);

            // 대기
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator LerpMovement(GameObject obj, float duration, Vector3 startPos, Vector3 targetPos)
    {
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
