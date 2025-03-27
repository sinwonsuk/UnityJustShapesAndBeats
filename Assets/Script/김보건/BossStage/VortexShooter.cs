using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class VortexShooter : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;     // 발사할 미사일 프리팹
    [SerializeField] private float fireInterval = 1f;      // 발사 간격 (초)
    [SerializeField] private Transform firePoint;          // 발사 위치
    [SerializeField] private float fireDuration = 2f; 

    [SerializeField] private float rotationSpeed = 360f;

    private Coroutine fireCoroutine;
    private Coroutine rotateCoroutine;

    private void OnEnable()
    {
        fireCoroutine = StartCoroutine(FireRoutine());
        rotateCoroutine = StartCoroutine(RotateScythe());

        // 일정 시간 후 자동 종료
        StartCoroutine(DelayStop(fireDuration));
    }
    
    private IEnumerator FireRoutine()
    {
        while (true)
        {
            FireMissile();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private IEnumerator RotateScythe()
    {
        while (true)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void FireMissile()
    {
        Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
    }

    private IEnumerator DelayStop(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 코루틴 중지
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);

        // 필요 시 이 오브젝트도 제거
        // Destroy(gameObject);
    }

}
