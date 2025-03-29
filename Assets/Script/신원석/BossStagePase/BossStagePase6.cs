using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;

public class BossStagePase6 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        time = 0;

        entity.StartPattern(entity.Getpattern(BossEPattern.SpawnSlayerPattern2));
        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossSlayer_finish),20f);

    }

    public override void Execute(BossStage entity)
    {
        time += Time.deltaTime;

        if (time > 31f)
        {           
           FadeManager.fadeManager.FadeOutAndChangeScene(SceneStage.Ending);
           return;
        }

    }

    public override void Exit(BossStage entity)
    {
        time = 0;
    }

    private float time = 0;
}
