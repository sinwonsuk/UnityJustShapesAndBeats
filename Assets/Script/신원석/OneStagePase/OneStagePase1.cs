using System.Collections;
using UnityEngine;

public class OneStagePase1 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        int a = 0;

    }

    public override void Execute(OneStage entity)
    {
        time += Time.deltaTime;


        entity.StartPattern(entity.Getpattern(EPattern.BallMovementPattern));
        if(time>= 3.5f && time < 3.6f )
        {

            entity.StartPattern(entity.Getpattern(EPattern.BigBallMovementPattern));
            entity.StopPattern(entity.Getpattern(EPattern.BallMovementPattern));
        }
        else if (time >= 8.5f)
        {
            entity.ChangeState(StagePase.Pase2);
        }

    }

    public override void Exit(OneStage entity)
    {
        time = 0;
    }

    float time = 0;

}
