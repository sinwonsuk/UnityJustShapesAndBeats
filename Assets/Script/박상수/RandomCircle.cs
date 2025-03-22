using System.Collections;
using UnityEngine;

public class RandomCircle : MonoBehaviour
{
    public GameObject parentPrefab;  // 랜덤한 위치에 생성될 부모 오브젝트 프리팹
    public GameObject childPrefab;   // 부모 오브젝트의 자식으로 생성될 오브젝트 프리팹
    public int spawnCount = 6;       // 총 생성 횟수
    public float spawnInterval = 1f; // 생성 간격 (초)
    public Vector2 spawnAreaMin = new Vector2(-5, -5); // 최소 좌표
    public Vector2 spawnAreaMax = new Vector2(5, 5);   // 최대 좌표
    public float targetScale = 2f;   // 자식 오브젝트의 최종 크기
    public float scaleDuration = 2f; // 크기 커지는 시간

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 랜덤 위치 계산
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            // 부모 오브젝트 생성
            GameObject parentObj = Instantiate(parentPrefab, randomPosition, Quaternion.identity);

            // 부모의 자식 오브젝트 생성
            GameObject childObj = Instantiate(childPrefab, parentObj.transform);
            childObj.transform.localPosition = Vector3.zero; // 부모의 중앙에 배치

            // 자식 오브젝트 크기 증가 기능 추가
            ScaleUp scaleScript = childObj.AddComponent<ScaleUp>();
            scaleScript.targetScale = targetScale;
            scaleScript.duration = scaleDuration;

            yield return new WaitForSeconds(spawnInterval); // 일정 간격으로 생성
        }
    }
}
