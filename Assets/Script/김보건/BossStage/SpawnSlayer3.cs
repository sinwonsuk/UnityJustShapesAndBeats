using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlayer3 : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SpawnBigSlayer()); 
        StartCoroutine(SpawnVortex());
        StartCoroutine(SpawnSlayers_one());
        StartCoroutine(SpawnSlayers_two());

    }

    private IEnumerator SpawnBigSlayer()
    {
        GameObject slayer = Instantiate(bigBossPrefab, spawnPositions_one[0], Quaternion.identity);
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator SpawnVortex()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject vortex = Instantiate(vortexPrefab, spawnPositions_one[0], Quaternion.identity);
    }

    private IEnumerator SpawnSlayers_one()
    {
        yield return new WaitForSeconds(3.9f);
        for (int i = 0; i < 7; i++)
        {
            if (i == 0)
            {
                GameObject slayer1 = Instantiate(singleSlayerPrefab, spawnPositions_one[i], Quaternion.identity);
                Transform scytheTransform = slayer1.transform.Find("Scythe_0");
                AppearScythe scytheScript = scytheTransform.GetComponent<AppearScythe>();
                scythes_one.Add(scytheScript);
                yield return new WaitForSeconds(1.8f);
            }
            else
            {
                GameObject slayer1 = Instantiate(slayerDownPrefab, spawnPositions_one[i], Quaternion.identity);
                yield return new WaitForSeconds(SpawnSlayers_oneDelay);

            }
        }

    }

    private IEnumerator SpawnSlayers_two()
    {
        yield return new WaitForSeconds(8f);

        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                GameObject slayer2 = Instantiate(slayerSideBigPrefab, spawnPositions_two[i], Quaternion.identity);
                Transform scytheTransform = slayer2.transform.Find("Scythe_0");
                AppearScythe scytheScript = scytheTransform.GetComponent<AppearScythe>();
                scythes_two.Add(scytheScript);
                yield return new WaitForSeconds(SpawnSlayers_twoDelay);
            }
            else
            {
                GameObject slayer2 = Instantiate(slayerSidePrefab, spawnPositions_two[i], Quaternion.identity);
                Transform scytheTransform = slayer2.transform.Find("Scythe_0");
                AppearScythe scytheScript = scytheTransform.GetComponent<AppearScythe>();
                scythes_two.Add(scytheScript);
                yield return new WaitForSeconds(SpawnSlayers_twoDelay);

            }

        }

        yield return new WaitForSeconds(5f);

        foreach (var scythe in scythes_two)
        {
            scythe.Stop();
        }
        foreach (var scythe in scythes_one)
        {
            scythe.Stop();
        }
    }

    private List<AppearScythe> scythes_one = new List<AppearScythe>();
    private List<AppearScythe> scythes_two = new List<AppearScythe>();


    [SerializeField] private GameObject vortexPrefab;
    [SerializeField] private GameObject bigBossPrefab;
    [SerializeField] private GameObject slayerDownPrefab;
    [SerializeField] private GameObject slayerSideBigPrefab;
    [SerializeField] private GameObject slayerSidePrefab;
    [SerializeField] private GameObject singleSlayerPrefab;

    [SerializeField] private float SpawnSlayers_oneDelay = 0.2f;
    [SerializeField] private float SpawnSlayers_twoDelay = 2.5f;

    private Vector3[] spawnPositions_one = new Vector3[]
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(8.2f, 3.8f, 0f),
        new Vector3(5.25f, 3.8f, 0f),
        new Vector3(2.3f, 3.8f, 0f),
        new Vector3(-0.65f, 3.8f, 0f),
        new Vector3(-3.6f, 3.8f, 0f),
        new Vector3(-6.55f, 3.8f, 0f)
    };

    private Vector3[] spawnPositions_two = new Vector3[]
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, 2.2f, 0f),
        new Vector3(0f, -3.1f, 0f),
        new Vector3(0f, -0.4f, 0f),
        new Vector3(0f, 2.2f, 0f),
        new Vector3(0f, -3.1f, 0f),
    };
}
