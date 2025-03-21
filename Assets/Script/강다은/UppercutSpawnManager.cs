using System.Collections;
using UnityEngine;

public class UppercutSpawnManager : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(gameObject.name);

        // 2초 후부터 0.8초 간격으로 패턴 스폰 시작
        InvokeRepeating("UppercutSpawnPattern", 2f, 0.8f);
    }

    private void Update()
    {
    }

    private void OnDisable()
    {
        CancelInvoke("UppercutSpawnPattern");
        Debug.Log("SpawnManager 비활성화");
    }

    private void UppercutSpawnPattern()
    {
        GameObject go = Instantiate(upperCutPattern);

        UpperCut upperCut = go.GetComponent<UpperCut>();
        upperCut.StartPattern();

    }

    [SerializeField]
    private GameObject upperCutPattern;
}