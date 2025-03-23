using System.Collections;
using UnityEngine;

public class MoveUpBar : MonoBehaviour
{
    [SerializeField] private GameObject mapObj;

    [SerializeField] private Vector3 startPos; // ���� ��ġ (�ν����Ϳ��� ����)
    [SerializeField] private Vector3 endPos;   // ��ǥ ��ġ (�ν����Ϳ��� ����)

    [SerializeField] private float startTime = 1f; // ���� ��� �ð�
    [SerializeField] private float moveDuration = 2f; // �̵��ϴ� �� �ɸ��� �ð�

    void Start()
    {
        mapObj.transform.position = startPos;
        // �ڷ�ƾ ���� (�ν����Ϳ��� ������ ��ġ ���)
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
