using System.Collections;
using UnityEngine;

public class BigSnailSpawner : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine(SpawnSequence());
	}

	private IEnumerator SpawnSequence()
	{
		
		Vector3 spawnPosition = Vector3.zero;
		GameObject fadeCircle = Instantiate(fadeCirclePrefab, spawnPosition, Quaternion.identity);

		yield return new WaitForSeconds(1f); 

		GameObject bigSnail = Instantiate(bigSnailPrefab, spawnPosition, Quaternion.identity);
		bigSnail.GetComponent<BigSnail>().Initialize(bigSnail);

		if (fadeCircle != null)
		{
			Destroy(fadeCircle);
		}

	}

	[SerializeField] private GameObject bigSnailPrefab;
	[SerializeField] private GameObject fadeCirclePrefab;
}
