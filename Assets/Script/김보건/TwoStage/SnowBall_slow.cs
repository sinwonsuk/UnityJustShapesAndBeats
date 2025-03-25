using System.Collections;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class SnowBall_slow : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(LoopSnowFall());

    }


    private IEnumerator LoopSnowFall()
    {
        while (true)
        {
            FireSpread(transform.position);
            yield return new WaitForSeconds(0.55f);
        }
    }

    private void FireSpread(Vector3 spawnPosition)
    {
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(-45f, 45f);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.left;

            // Y ¹üÀ§ ·£´ý
            float randomY = Random.Range(-4.7f, 4.7f);
            Vector3 spawnPos = new Vector3(8.83f, randomY, 0f);

            GameObject go = Instantiate(square, spawnPos, Quaternion.identity);

            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randomSpeed = Random.Range(4f, 6f);
                rb.linearVelocity = dir * randomSpeed;

                float spinSpeed = 600f;
                rb.angularVelocity = spinSpeed;
            }
        }
    }

    [SerializeField] private GameObject square;

}
