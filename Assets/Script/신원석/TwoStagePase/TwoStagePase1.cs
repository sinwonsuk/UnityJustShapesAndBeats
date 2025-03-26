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

        entity.GetPatternTimeObject(TwoEPattern.BoxFadePatternManager, 0, 16);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 24);

      
        //entity.StopTimePattern(entity.GetPatternObject(TwoEPattern.BoxFadePatternManager, 0), 26);


        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 20);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 21);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 22);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 23);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.UglyCircle), 23);
        time = 0; 

    }

    public override void Execute(TwoStage entity)
    {
        time += Time.deltaTime;

        if (time > 27)
        {
            entity.ChangeState(TwoStagePase.Pase2);
            return;
        }
   
    }

    public override void Exit(TwoStage entity)
    {
        
    }


    float time = 0;
}
