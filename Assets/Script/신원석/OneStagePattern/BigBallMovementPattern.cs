using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BigBallMovementPattern : MonoBehaviour
{
    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InvokeRepeating("SpawnBigBall", 0, 1);
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        coroutineCheck = 0;
        CancelInvoke("SpawnBigBall");
    }

    public void SpawnBigBall()
    {
        coroutineCheck++;
        float posY = Random.Range(-5.0f, 5.0f);
        Instantiate(bigBall, new Vector2(10, posY), Quaternion.identity);      
    }

    [SerializeField]
    private GameObject bigBall;

    private int coroutineCheck = 0;
    public  Coroutine spawnCoroutine;
}
