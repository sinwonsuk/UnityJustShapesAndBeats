using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class VortexShooter : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;     // �߻��� �̻��� ������
    [SerializeField] private float fireInterval = 1f;      // �߻� ���� (��)
    [SerializeField] private Transform firePoint;          // �߻� ��ġ
    [SerializeField] private float fireDuration = 2f; 

    [SerializeField] private float rotationSpeed = 360f;

    private Coroutine fireCoroutine;
    private Coroutine rotateCoroutine;

    private void OnEnable()
    {
        fireCoroutine = StartCoroutine(FireRoutine());
        rotateCoroutine = StartCoroutine(RotateScythe());

        // ���� �ð� �� �ڵ� ����
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

        // �ڷ�ƾ ����
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);

        // �ʿ� �� �� ������Ʈ�� ����
        // Destroy(gameObject);
    }

}
