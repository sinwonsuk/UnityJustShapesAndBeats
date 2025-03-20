using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class PersonSpawner : MonoBehaviour
{
    public GameObject personPrefab;      // 사람 프리팹 연결
    public float minSpawnTime = 0.2f;      // 최소 생성 시간
    public float maxSpawnTime = 1.0f;      // 최대 생성 시간

    private bool isSpawning = false;
    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }
    private void OnDisable()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }


   

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(0.5f); // 처음 대기

        while (isSpawning)
        {
            SpawnPerson();

            // 매번 새로 랜덤 시간 뽑음!
            float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Debug.Log($"다음 스폰까지 대기 시간: {nextSpawnTime}초");

            yield return new WaitForSeconds(nextSpawnTime);
        }
    }

    void SpawnPerson()
    {
        Debug.Log("생성 시간: " + Time.time);
        float randomY = Random.Range(-3f, 3f);
        Instantiate(personPrefab, new Vector2(10, randomY), Quaternion.identity);
    }
}
