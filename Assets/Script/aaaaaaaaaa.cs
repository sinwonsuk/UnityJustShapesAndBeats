using System.Collections;
using UnityEngine;

public class AAAAAAAA : State<OneStage>
{

    public override void Enter(OneStage entity)
    {
        Debug.Log(gameObject.name);
        InvokeRepeating("SpawnBigBall", 0, 1);
    }

    public override void Execute(OneStage entity)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(test());
        }


        if (coroutineCheck == 10)
        {
            entity.ChangeState(StagePattern.Stage);
            return;
        }
    }
    public override void Exit(OneStage entity)
    {
        coroutineCheck = 0;
        CancelInvoke("SpawnBigBall");
    }

    IEnumerator test()
    {
        Debug.Log("³ª µÈ´Ù.");
        yield return new WaitForSeconds(1);
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
    public Coroutine spawnCoroutine;
}
