using System.Collections;
using UnityEngine;

public class BulletTestShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shootAngle = 90f;

    void Start()
    {
        Debug.Log("코루틴 시작");
        StartCoroutine(ShootAfterDelay());
    }

    IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(1.4f);
        Debug.Log("총알 생성 시도");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Dynamic;

            float angleRad = shootAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;

            rb.linearVelocity = direction * bulletSpeed;
            Debug.Log($"발사! 방향: {direction}, 속도: {rb.linearVelocity}");
        }
        else
        {
            Debug.LogWarning("Rigidbody2D가 총알에 없음!");
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
