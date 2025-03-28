using UnityEngine;

public class NextSceneTriangleSpawner : MonoBehaviour
{
	void Start()
	{
		Camera cam = Camera.main;
		float camHeight = 2f * cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;

		screenAreaMin = new Vector2(cam.transform.position.x - camWidth / 2, cam.transform.position.y - camHeight / 2);
		screenAreaMax = new Vector2(cam.transform.position.x + camWidth / 2, cam.transform.position.y + camHeight / 2);

		SpawnTriangle();
	}

	void SpawnTriangle()
	{
		// 화면 밖 시작 위치 (랜덤 방향: 상, 하, 좌, 우)
		Vector2 startPos = GetRandomOutsidePosition();
		// 화면 안 목표 위치
		Vector2 targetPos = new Vector2(
			Random.Range(screenAreaMin.x, screenAreaMax.x),
			Random.Range(screenAreaMin.y, screenAreaMax.y)
		);

		GameObject triangle = Instantiate(trianglePrefab, startPos, Quaternion.identity);

		triangle.GetComponent<NextSceneAnimation>().entity = entity;


        NextSceneAnimation controller = triangle.GetComponent<NextSceneAnimation>();
		if (controller != null)
		{
			controller.StartMoveAnimation(startPos, targetPos, moveDuration);
		}
	}

	Vector2 GetRandomOutsidePosition()
	{
		Camera cam = Camera.main;
		float camHeight = 2f * cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;
		Vector2 camPos = cam.transform.position;

		int side = Random.Range(0, 4); // 0: 위, 1: 아래, 2: 왼쪽, 3: 오른쪽
		switch (side)
		{
			case 0: // 위
				return new Vector2(Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2), camPos.y + camHeight / 2 + 1f);
			case 1: // 아래
				return new Vector2(Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2), camPos.y - camHeight / 2 - 1f);
			case 2: // 왼쪽
				return new Vector2(camPos.x - camWidth / 2 - 1f, Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2));
			case 3: // 오른쪽
				return new Vector2(camPos.x + camWidth / 2 + 1f, Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2));
			default:
				return Vector2.zero;
		}
	}

	[SerializeField] private GameObject trianglePrefab;
	[SerializeField] private float spawnInterval = 2f;
	[SerializeField] private float moveDuration = 1f; // 화면 밖에서 안으로 이동하는 시간
	[SerializeField] private Vector2 screenAreaMin;   // 화면 안 목표 영역 최소 좌표
	[SerializeField] private Vector2 screenAreaMax;   // 화면 안 목표 영역 최대 좌표

	public BaseGameEntity entity;
}