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
		// ȭ�� �� ���� ��ġ (���� ����: ��, ��, ��, ��)
		Vector2 startPos = GetRandomOutsidePosition();
		// ȭ�� �� ��ǥ ��ġ
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

		int side = Random.Range(0, 4); // 0: ��, 1: �Ʒ�, 2: ����, 3: ������
		switch (side)
		{
			case 0: // ��
				return new Vector2(Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2), camPos.y + camHeight / 2 + 1f);
			case 1: // �Ʒ�
				return new Vector2(Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2), camPos.y - camHeight / 2 - 1f);
			case 2: // ����
				return new Vector2(camPos.x - camWidth / 2 - 1f, Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2));
			case 3: // ������
				return new Vector2(camPos.x + camWidth / 2 + 1f, Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2));
			default:
				return Vector2.zero;
		}
	}

	[SerializeField] private GameObject trianglePrefab;
	[SerializeField] private float spawnInterval = 2f;
	[SerializeField] private float moveDuration = 1f; // ȭ�� �ۿ��� ������ �̵��ϴ� �ð�
	[SerializeField] private Vector2 screenAreaMin;   // ȭ�� �� ��ǥ ���� �ּ� ��ǥ
	[SerializeField] private Vector2 screenAreaMax;   // ȭ�� �� ��ǥ ���� �ִ� ��ǥ

	public BaseGameEntity entity;
}