using UnityEngine;

public class ThreeCircle : MonoBehaviour
{
    public GameObject parentPrefab;  // 부모 오브젝트 프리팹
    public GameObject childPrefab;   // 크기 증가할 자식 오브젝트 프리팹
    public Vector2 spawnAreaMin = new Vector2(-5, -5); // 최소 좌표
    public Vector2 spawnAreaMax = new Vector2(5, 5);   // 최대 좌표
    public float targetScale = 2f;   // 자식 오브젝트의 최종 크기
    public float scaleDuration = 2f; // 크기 커지는 시간

    void Start()
    {
        SpawnThreeObjects(); // 게임 시작 시 한 번 실행
    }

    void SpawnThreeObjects()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        for (int i = 0; i < 3; i++)
        {
            Vector2 offset = new Vector2(i * 2, 0); // 3개의 오브젝트를 옆으로 배치
            Vector2 spawnPosition = randomPosition + offset;

            // 부모 오브젝트 생성
            GameObject parentObj = Instantiate(parentPrefab, spawnPosition, Quaternion.identity);

            // 부모의 자식 오브젝트 생성
            GameObject childObj = Instantiate(childPrefab, parentObj.transform);
            childObj.transform.localPosition = Vector3.zero; // 부모의 중심에 배치

            // 자식 오브젝트 크기 증가 기능 추가
            ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
            scaleScript.targetScale = targetScale;
            scaleScript.duration = scaleDuration;
        }
    }
}
