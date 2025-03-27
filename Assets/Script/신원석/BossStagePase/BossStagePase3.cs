using UnityEngine;
using System.Collections;

public class BossStagePase3 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        time = 0;

        entity.StartPattern(entity.Getpattern(BossEPattern.Pase3BossStartCircle));

        entity.StartTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle),3.0f);


        entity.StopTimePattern(entity.Getpattern(BossEPattern.Pase3BossStartCircle),3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.Boss),3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.GhostParent), 3.2f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle), 3.2f);

        entity.Getpattern(BossEPattern.BossChainDown).transform.position = new Vector2(-8.3f, -4.4f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossChainDown), 3.5f);

      
        entity.Getpattern(BossEPattern.BossChainUp).transform.position = new Vector2(8.3f, 4.4f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossChainUp), 3.5f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.SquareRotation), 3.7f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossBulletSpawn), 4.0f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.BossBulletSpawn), 11.0f);


        entity.StartTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle), 13.3f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.Boss), 13.4f);
    }

    public override void Execute(BossStage entity)
    {
        time += Time.deltaTime;

        if (time > 9.0f)
        {
            entity.Getpattern(BossEPattern.BossChainUp).GetComponent<BossChainCircleSpawn>().StartReduceCircle();
            entity.Getpattern(BossEPattern.BossChainDown).GetComponent<BossChainCircleSpawn>().StartReduceCircle();
        }

        if (time > 10.5f)
        {        
            entity.Getpattern(BossEPattern.SquareRotation).GetComponent<BossSquareRotation>().ReduceScaleChild();
        }

        if (time > 11.0f)
        {           
            entity.Getpattern(BossEPattern.GhostParent).GetComponent<GhostParent>().ReduceScaleChild();
        }
    }

    public override void Exit(BossStage entity)
    {
        
    }

    float time = 0;
}
