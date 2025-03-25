using UnityEngine;
using System.Collections;

public class TwoStagePase1 : State<TwoStage>
{
    public override void Enter(TwoStage entity)
    {
        entity.StartPattern(entity.Getpattern(TwoEPattern.SnowBall_first));
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_first), 22);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.triangle_spawner), 8);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.triangle_spawner_2), 8);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.miniball), 8);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.miniball_2), 8);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 15);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 27);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.UglyCircle), 26);
        time = 0;
    }

    public override void Execute(TwoStage entity)
    {
       
    }

    public override void Exit(TwoStage entity)
    {
        
    }


    float time = 0;
}
