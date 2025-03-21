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
        entity.StopTimePattern(entity.Getpattern(EPattern.map_fastCreate_shake), 14);

        entity.StartTimePattern(entity.Getpattern(EPattern.map_fastCreate), 13.5f);
        entity.StartTimePattern(entity.Getpattern(EPattern.BulletLuncher), 16);

        entity.StopTimePattern(entity.Getpattern(EPattern.map_fastCreate),19);
        entity.StartTimePattern(entity.Getpattern(EPattern.map_fastCreate), 20f);
        entity.StartTimePattern(entity.Getpattern(EPattern.HadoukenPattern), 28.5f);

        entity.StopTimePattern(entity.Getpattern(EPattern.BulletLuncher), 27);
    }


    public override void Execute(OneStage entity)
    {
        time += Time.deltaTime;

        if (time > 31f)
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