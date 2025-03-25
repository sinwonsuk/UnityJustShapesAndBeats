using System.Collections;
using UnityEngine;

public class OneStagePase1 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        entity.StartPattern(entity.Getpattern(EPattern.BallMovementPattern));
        entity.StartTimePattern(entity.Getpattern(EPattern.map_first),3.5f);

        entity.StopTimePattern(entity.Getpattern(EPattern.map_first), 13.0f);
        entity.StartTimePattern(entity.Getpattern(EPattern.map_fastCreate), 13.2f);


        time = 0;
    }

    public override void Execute(OneStage entity)
    {
        time += Time.deltaTime;
   
        if(time>= 3.5f && time < 3.6f )
        {
            entity.StartPattern(entity.Getpattern(EPattern.BigBallMovementPattern));
        }
        else if (time >= 17.0f)
        {
            entity.ChangeState(StagePase.Pase2);
            return;
        }
        else if (time >= 15.0f)
        {
            entity.StartPattern(entity.Getpattern(EPattern.HadoukenPattern));
        }       
    }

    public override void Exit(OneStage entity)
    {
        entity.StopPattern(entity.Getpattern(EPattern.BallMovementPattern));
        entity.StopPattern(entity.Getpattern(EPattern.BigBallMovementPattern));
        time = 0;
    }

    
    float time = 0;

}
