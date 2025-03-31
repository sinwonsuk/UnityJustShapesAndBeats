using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        for (int i = 0; i < 30; i++)
        {
            GameObject go = Instantiate(circle, pos, Quaternion.identity);

            pos = new Vector2(go.transform.position.x + offsetX, go.transform.position.y);

            circleList.Add(go);

            yield return new WaitForSeconds(spawnTime);
        }

        yield break;
    }

    public void StartReduceCircle()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(ReduceCircleProcess());
        }
    }

    IEnumerator ReduceCircleProcess()
    {
        for (int i = 0; i < circleList.Count; i++)
        {
            StartCoroutine(ReduceCircle(circleList[i]));
            yield return new WaitForSeconds(DeleteTime);
        }
    }

    IEnumerator ReduceCircle(GameObject circle)
    {
        while (true)
        {
            if (circle.transform.localScale.x <= 0.0f)
            {
                Destroy(circle);
                yield break;
            }

            circle.transform.localScale -= Vector3.one * reduceScaleTime*Time.deltaTime;

            yield return null;
       
        }

    }

    List<GameObject> circleList = new List<GameObject>();

    [SerializeField]
    private float offsetX = 1.0f;

    Vector2 pos;

    [SerializeField]
    private float spawnTime = 1.0f;

    [SerializeField]
    private float DeleteTime = 0.5f;

    [SerializeField]
    private float reduceScaleTime = 1.0f;

    [SerializeField]
    private GameObject circle;

    Coroutine coroutine;


}
