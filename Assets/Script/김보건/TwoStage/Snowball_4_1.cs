using System.Collections;
using UnityEngine;

public class Snowball_4_1 : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(FireSpreadFinalRepeat());
    }

    // 처음 3번 흩뿌리기
    private IEnumerator FireSpreadPattern()
    {
        FireSpread(new Vector3(8.83f, -3f, 0f));
        yield return new WaitForSeconds(0.3f);
        FireSpread(new Vector3(8.83f, -3f, 0f));
        yield return new WaitForSeconds(0.85f);
        FireSpread(new Vector3(8.83f, -3f, 0f));
    }

    // 두번째 3번 흩뿌리기 + 3번째에 위에 한 번 흩뿌리기
    private IEnumerator FireSpreadTogether()
    {
        FireSpread(new Vector3(8.83f, -3f, 0f));
        yield return new WaitForSeconds(0.3f);
        FireSpread(new Vector3(8.83f, -3f, 0f));
        yield return new WaitForSeconds(0.85f);
        FireSpread(new Vector3(8.83f, -3f, 0f));
        FireSpread(new Vector3(8.83f, 3f, 0f));
    }

    // 처음 3번 흩뿌리기 + 두번째 3번 흩뿌리기 + 3번째에 위에 한 번 흩뿌리기
    private IEnumerator FireSpreadRepeat()
    {

        yield return StartCoroutine(FireSpreadPattern());

        yield return new WaitForSeconds(0.8f);

        yield return StartCoroutine(FireSpreadTogether());
    }

    // 처음 3번 흩뿌리기 + 두번째 3번 흩뿌리기 + 3번째에 위에 한 번 흩뿌리기 => 여섯번반복

    private IEnumerator FireSpreadFinalRepeat()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.8f);

            yield return FireSpreadRepeat();
        }
    }


    // 흩뿌리기 각도 방향 스폰위치
    private void FireSpread(Vector3 spawnPosition)
    {
        int count = Random.Range(6, 8); 

        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(-45f, 45f);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.left;

            GameObject go = Instantiate(square, spawnPosition, Quaternion.identity);

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

    [SerializeField]
    private GameObject square;

}
