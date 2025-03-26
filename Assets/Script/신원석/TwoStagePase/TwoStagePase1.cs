using UnityEngine;
using System.Collections;

public class TwoStagePase1 : State<TwoStage>
{  
    public override void Enter(TwoStage entity)
    {
        entity.StartPattern(entity.Getpattern(TwoEPattern.SnowBall_first));
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_first), 22);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.triangle_spawner), 5.5f);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.triangle_spawner_2), 6);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.miniball), 5.5f);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.miniball_2), 6);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 15);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 27);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.ugly), 20);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.ugly), 21);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.ugly), 22);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.ugly), 23);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.UglyCircle), 23);
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
