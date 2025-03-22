using UnityEngine;

public class TriangleSpawner1 : MonoBehaviour
{
    public GameObject trianglePrefab;  // Triangle 프리팹 연결
    public Vector3 spawnPosition = new Vector3(0, -4, 0);  // 삼각형의 초기 위치

    void Start()
    {
        // 첫 번째 삼각형 생성
        Instantiate(trianglePrefab, spawnPosition, Quaternion.identity);
    }
}
