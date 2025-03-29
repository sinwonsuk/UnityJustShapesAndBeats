using UnityEngine;
using System.Collections;

public class BossStagePase3 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        entity.StartPattern(entity.Getpattern(BossEPattern.SpawnSlayerPattern1));

        Debug.Log(time);
    }

    public override void Execute(BossStage entity)
    {
        Debug.Log(time);

        time += Time.deltaTime;

        if (time > 33)
        {
            entity.ChangeState(BossStagePase.Pase4);
            return;
        }
    }

    public override void Exit(BossStage entity)
    {
        
    }

    private float time = 0;
}
