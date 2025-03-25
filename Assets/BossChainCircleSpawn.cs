using System.Collections;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
using static UnityEditor.PlayerSettings;

public class BossChainCircleSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circle.transform.position = transform.position;

        circle.transform.rotation = transform.rotation;

        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        pos = new Vector2(circle.transform.position.x, circle.transform.position.y);

        while (true)
        {
            GameObject go = Instantiate(circle, pos, Quaternion.identity);

            pos = new Vector2(go.transform.position.x+offsetX,go.transform.position.y);

            yield return new WaitForSeconds(spawnTime);

        }
    }

    [SerializeField]
    private float offsetX = 1.0f;

    Vector2 pos;

    [SerializeField]
    private float spawnTime = 1.0f;

    [SerializeField]
    private GameObject circle;




}
