using UnityEngine;
using System.Collections;

public class BossStagePase3 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        entity.StartPattern(entity.Getpattern(BossEPattern.SpawnSlayerPattern1));
    }

    public override void Execute(BossStage entity)
    {
        time += Time.deltaTime;

        if (time > 33)
        {
            entity.ChangeState(BossStagePase.Pase2);
            return;
        }
    }

    public override void Exit(BossStage entity)
    {
        
    }

    private float time = 0;
}
