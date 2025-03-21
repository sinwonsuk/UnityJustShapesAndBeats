using System.Collections;
using UnityEngine;

public class UppercutSpawnManager : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(gameObject.name);

        // 2�� �ĺ��� 0.8�� �������� ���� ���� ����
        InvokeRepeating("UppercutSpawnPattern", 2f, 0.8f);
    }

    private void Update()
    {
    }

    private void OnDisable()
    {
        CancelInvoke("UppercutSpawnPattern");
        Debug.Log("SpawnManager ��Ȱ��ȭ");
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