using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class PersonSpawner : MonoBehaviour
{
    public GameObject personPrefab;      // ��� ������ ����
    public float minSpawnTime = 0.2f;      // �ּ� ���� �ð�
    public float maxSpawnTime = 1.0f;      // �ִ� ���� �ð�

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
        yield return new WaitForSeconds(0.5f); // ó�� ���

        while (isSpawning)
        {
            SpawnPerson();

            // �Ź� ���� ���� �ð� ����!
            float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Debug.Log($"���� �������� ��� �ð�: {nextSpawnTime}��");

            yield return new WaitForSeconds(nextSpawnTime);
        }
    }

    void SpawnPerson()
    {
        Debug.Log("���� �ð�: " + Time.time);
        float randomY = Random.Range(-3f, 3f);
        Instantiate(personPrefab, new Vector2(10, randomY), Quaternion.identity);
    }
}
