using UnityEngine;
using System.Collections;

public class MyScript3 : State<OneStage>
{


    public override void Enter(OneStage entity)
    {
        time = 0;

        entity.StartPattern(entity.Getpattern(EPattern.kickMove));
        entity.StartPattern(entity.Getpattern(EPattern.UppercutPattern));

        entity.StopTimePattern(entity.Getpattern(EPattern.kickMove), 14);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern),14);

        entity.StartTimePattern(entity.Getpattern(EPattern.BulletLuncher), 16);
        entity.StopTimePattern(entity.Getpattern(EPattern.BulletLuncher), 32);
    }


    public override void Execute(OneStage entity)
    {
        time += Time.deltaTime;

        if (time > 32.0f)
        {
            entity.ChangeState(StagePase.Pase4);
            return;
        }    
    }

    public override void Exit(OneStage entity)
    {

    }

    float time = 0;
}