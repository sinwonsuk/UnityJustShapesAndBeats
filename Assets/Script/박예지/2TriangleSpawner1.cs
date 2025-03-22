using UnityEngine;
using System.Collections;
public class TriangleSpawner : MonoBehaviour
{
    public GameObject trianglePrefab;  // Triangle 프리팹 연결
    public Vector3 spawnPosition = new Vector3(0, -4, 0);  // 삼각형의 초기 위치

    void Start()
    {
        // 3초 기다린 후 실행
        StartCoroutine(WaitAndDoSomething());
    }

    IEnumerator WaitAndDoSomething()
    {
        // 3초 기다리기
        yield return new WaitForSeconds(1f);  // 1초 대기
        Instantiate(trianglePrefab, spawnPosition, Quaternion.identity);
        
    }
}
