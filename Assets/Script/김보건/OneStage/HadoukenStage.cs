using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HadoukenStage : MonoBehaviour
{

    private void OnEnable()
    {
        Debug.Log("HadoukenStage");

        time = 0f;
        hadoukenAniObj.SetActive(true);

        //처음 위치
        hadoukenAniObj.transform.position = new Vector3(7.47f, -8f, hadoukenAniObj.transform.position.z);

        // 올라가기, 발사, 내려가기
        StartCoroutine(MoveUp(hadoukenAniObj, 0.5f));
        StartCoroutine(DelayFire());
        StartCoroutine(MoveDown(hadoukenAniObj, 0.5f));
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 5f)
        {
            hadoukenAniObj.SetActive(false);
 
        }
    }

    public void OnDisable()
    {
        time = 0;
    }


    // 오브젝트 위로 이동
    private IEnumerator MoveUp(GameObject obj, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = new Vector3(startPos.x, -3.75f, startPos.z);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        obj.transform.position = endPos;
    }

    // 오브젝트 일정 시간 후 아래로 이동
    private IEnumerator MoveDown(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(4.1f);

        float elapsed = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = new Vector3(obj.transform.position.x, -8f, obj.transform.position.z);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        obj.transform.position = endPos;
    }

    // 발사를 지연 후 시작, 그리고 일정 시간 후 중지
    private IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(1.8f);
        Coroutine fireRoutine = StartCoroutine(ContinuousFire());
        yield return new WaitForSeconds(2f);
        StopCoroutine(fireRoutine);
    }

    // 총알 계속 발사
    private IEnumerator ContinuousFire()
    {
        while (true)
        {
            StartCoroutine(FireWithGrow(bullet1));
            StartCoroutine(FireWithGrow(bullet2));
            StartCoroutine(FireWithGrow(bullet3));

            yield return new WaitForSeconds(fireInterval);
        }
    }

    // 총알 생성 -> 크기 증가 -> 발사
    private IEnumerator FireWithGrow(GameObject bulletPrefab)
    {
        float randomAngle = fireAngle + Random.Range(-15f, 15f);
        Vector3 originalScale = bulletPrefab.transform.localScale;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, randomAngle));

        bullet.transform.localScale = Vector3.zero;

        yield return StartCoroutine(GrowBullet(bullet, originalScale));

        FireBullet(bullet, randomAngle);
    }

    // 발사 전에 총알 크기를 서서히 키움
    private IEnumerator GrowBullet(GameObject bullet, Vector3 targetScale)
    {
        float current = 0f;

        while (current < growTime)
        {
            current += Time.deltaTime;
            bullet.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, current / growTime);
            yield return null;
        }

        bullet.transform.localScale = targetScale;
    }

    // 각도랜덤으로 총알 발사
    private void FireBullet(GameObject bullet, float angle)
    {
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 launchDirection = new Vector2(
                -Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            rb.AddForce(launchDirection * launchPower, ForceMode2D.Impulse);
        }
    }

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet1;
    [SerializeField] private GameObject bullet2;
    [SerializeField] private GameObject bullet3;
    [SerializeField] private GameObject bullet4;

    [SerializeField] private GameObject hadoukenAniObj;

    [SerializeField] private float fireAngle = 0f;
    [SerializeField] private float growTime = 0.1f;
    [SerializeField] private float launchPower = 10.0f;
    [SerializeField] private float fireInterval = 0.05f;
    [SerializeField] private float time = 0.0f;
}
