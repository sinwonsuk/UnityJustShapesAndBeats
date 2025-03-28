using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlayer1 : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SpawnSlayers());
    }

    private IEnumerator SpawnSlayers()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                GameObject slayer = Instantiate(singleSlayerPrefab, spawnPositions[i], Quaternion.identity);
                Transform scytheTransform = slayer.transform.Find("Scythe_0");
                AppearScythe scytheScript = scytheTransform.GetComponent<AppearScythe>();
                scythes.Add(scytheScript);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                GameObject slayer = Instantiate(slayerPrefab, spawnPositions[i], Quaternion.identity);
                Transform scytheTransform = slayer.transform.Find("Scythe_0");
                AppearScythe scytheScript = scytheTransform.GetComponent<AppearScythe>();
                scythes.Add(scytheScript);
                yield return new WaitForSeconds(delay);

            }
        }

        yield return new WaitForSeconds(2f);

        for (int i = 3; i < spawnPositions.Length; i++)
        {
            GameObject slayer = Instantiate(slayerPrefab, spawnPositions[i], Quaternion.identity);

            Transform scytheTransform = slayer.transform.Find("Scythe_0");
            AppearScythe scytheScript = scytheTransform.GetComponent<AppearScythe>();
            scythes.Add(scytheScript);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(4f);

        foreach (var scythe in scythes)
        {
            scythe.Stop();
        }
    }
    private List<AppearScythe> scythes = new List<AppearScythe>();

    [SerializeField] private GameObject slayerPrefab;
    [SerializeField] private GameObject singleSlayerPrefab;

    [SerializeField] private float delay = 22f;
    [SerializeField] private float betweenDelay = 2f;

    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, 2.2f, 0f),
        new Vector3(0f, -3.1f, 0f),
        new Vector3(0f, -0.4f, 0f),
        new Vector3(0f, 2.2f, 0f),
        new Vector3(0f, -3.1f, 0f),
    };
}
