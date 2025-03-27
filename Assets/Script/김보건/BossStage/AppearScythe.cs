using System.Collections;
using UnityEngine;

public class AppearScythe : MonoBehaviour
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
        float time = 0f;
        float direction = 1f;
        Vector3 startPos = transform.position;

        while (true)
        {

            if (!isMoving) yield break;


            time += Time.deltaTime;

            float x = transform.position.x + direction * moveSpeed * Time.deltaTime;
            float y = startPos.y + Mathf.Sin(time * 2f) * 0.5f;

            if (x <= minX || x >= maxX)
            {
                direction *= -1f;
                time = 0f;
                startPos.y = y;
            }

            transform.position = new Vector3(x, y, transform.position.z);

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



    public void Stop()
    {
        isMoving = false;
        isRotating = false;
        StartCoroutine(ShakeStop());

        StartCoroutine(DestroyScythe(3f));
    }
    private IEnumerator ShakeStop()
    {
        float duration = 0.1f;   // 진동 지속 시간
        float time = 0f;
        float intensity = 0.2f;  // 진폭
        float frequency = 30f;   // 진동 횟수

        Vector3 originalPos = transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;

            float damper = Mathf.Lerp(1f, 0f, time / duration);
            float offsetY = Mathf.Sin(time * frequency) * intensity * damper;

            transform.position = originalPos + new Vector3(0f, offsetY, 0f);

            yield return null;
        }

        transform.position = originalPos;
    }

    private IEnumerator DestroyScythe(float delay)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float fadeDuration = delay;
        float currentTime = 0f;

        Color originalColor = sr.color;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }

    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private float growTime = 0.1f;
    [SerializeField] private float shrinkTime = 0.5f;

    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 360f;

    private Vector2 moveDir;
    private bool isMoving = true;
    private bool isRotating = true;

}
