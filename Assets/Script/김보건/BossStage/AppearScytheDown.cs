using System.Collections;
using UnityEngine;

public class AppearScytheDown : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        moveDir = Random.insideUnitCircle.normalized;

        StartCoroutine(AppearScy());
        StartCoroutine(RotateScythe());

    }

    private IEnumerator AppearScy()
    {

        yield return new WaitForSeconds(1.4f);

        transform.localScale = originalScale;

        StartCoroutine(ShakeRotation());

        yield return new WaitForSeconds(0.8f);
        StartCoroutine(MoveScythe());
    }

    private IEnumerator RotateScythe()
    {
        yield return new WaitForSeconds(2.2f);
        while (true)
        {
            if (!isRotating) yield break;

            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveScythe()
    {
        while (true)
        {
            if (!isMoving) yield break;

            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator ShakeRotation()
    {
        float duration = 0.3f;   // 진동 지속 시간
        float time = 0f;
        float angle = 10f;       // 회전 각도
        float frequency = 30f;   // 진동 속도

        Quaternion originalRotation = transform.rotation;

        while (time < duration)
        {
            time += Time.deltaTime;

            float damper = Mathf.Lerp(1f, 0f, time / duration);
            float zRotation = Mathf.Sin(time * frequency) * angle * damper;

            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
            yield return null;

        }

        transform.rotation = originalRotation;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private float growTime = 0.1f;
    [SerializeField] private float shrinkTime = 0.5f;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 360f;

    private Vector2 moveDir;
    private bool isMoving = true;
    private bool isRotating = true;
}
