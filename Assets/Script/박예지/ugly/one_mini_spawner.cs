using System.Collections;
using UnityEngine;

public class BulletTestShooter2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shootAngle = 90f;
    public Vector2 spawnOffset = Vector2.zero; 
    void Start()
    {
       
        StartCoroutine(ShootAfterDelay());
    }

    IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(1.3f);
        

       
        Vector3 spawnPosition = firePoint.position + (Vector3)spawnOffset;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Dynamic;

            float angleRad = shootAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;

            rb.linearVelocity = direction * bulletSpeed;  // linearVelocity �� velocity�� ����
            
        }
        else
        {
            
        }
    }

    void OnBecameInvisible()
    {
        
    }
}
