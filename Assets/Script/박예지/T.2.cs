using UnityEngine;
using System.Collections;

public class TriangleSpawner2 : MonoBehaviour
{
    public GameObject trianglePrefab;  // Triangle 프리팹 연결
    public Vector3 spawnPosition = new Vector3(0, -4, 0);  // 삼각형의 초기 위치

    void Start()
    {
        StartCoroutine(WaitAndExecute());
    }

    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(1f);  // 2초 대기
        Debug.Log("2초 후 삼각형 생성!");
        Instantiate(trianglePrefab, spawnPosition, Quaternion.identity);
    }

}
