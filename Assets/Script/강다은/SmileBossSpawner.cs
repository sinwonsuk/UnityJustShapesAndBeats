using System.Collections;
using UnityEngine;

public class SmileBossSpawner : MonoBehaviour
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

		GameObject bossSmile = Instantiate(BossSmilePrefab, spawnPosition, Quaternion.identity);
		bossSmile.GetComponent<BossSmile>().Initialize(bossSmile);

		if (fadeCircle != null)
		{
			Destroy(fadeCircle);
		}
	}

	[SerializeField] private GameObject BossSmilePrefab;
	[SerializeField] private GameObject fadeCirclePrefab;
}
