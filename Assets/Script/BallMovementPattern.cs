using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BallMovementPattern : State<OneStage>
{   
    public override void Enter(OneStage entity)
    {
        Debug.Log(gameObject.name);
        Invoke("CreateObject", 2);
      
    }

    public override void Execute(OneStage entity)
    {             
        time += Time.deltaTime;

        if(time > 6)
        {
            entity.ChangeState(StagePattern.Stage1);
            return;
        }
    }

    public override void Exit(OneStage entity)
    {
        time = 0;
    }

    public void CreateObject()
    {
        for (int i = 0; i < 20; i++)
        {
            float randomPosY = Random.Range(5.0f, 13.0f);

            float randomPosX = Random.Range(10.0f, 18.0f);

            GameObject go = Instantiate(square, new Vector3(randomPosX, randomPosY), Quaternion.identity);

            ParabolaMovement parabolaMovement = go.GetComponent<ParabolaMovement>();

            parabolaMovement.dir = Dir.LeftDown;
        }

        for (int i = 0; i < 20; i++)
        {
            float randomPosY = Random.Range(-5.0f, -13.0f);

            float randomPosX = Random.Range(10.0f, 18.0f);

            GameObject go = Instantiate(square, new Vector3(randomPosX, randomPosY), Quaternion.identity);

            ParabolaMovement parabolaMovement = go.GetComponent<ParabolaMovement>();

            parabolaMovement.dir = Dir.LeftUp;
        }

        for (int i = 0; i < 15; i++)
        {
            float randomPosY = Random.Range(-5.0f, 5.0f);

            float randomPosX = Random.Range(10.0f, 22.0f);

            GameObject go = Instantiate(square, new Vector3(randomPosX, randomPosY), Quaternion.identity);

            ParabolaMovement parabolaMovement = go.GetComponent<ParabolaMovement>();

            float randomAngle = Random.Range(0, 9.0f);

            parabolaMovement.SetAngleSpeed(randomAngle);

            float randomposXSpeed = Random.Range(-8.0f, -5.0f);

            parabolaMovement.SetSpeedX(randomposXSpeed);

            float randomposYSpeed = Random.Range(5.0f, 10.0f);

            parabolaMovement.SetSpeedY(randomposYSpeed);

        }
    }
    

    [SerializeField]
    private GameObject square;

    private float time = 0.0f;
}
