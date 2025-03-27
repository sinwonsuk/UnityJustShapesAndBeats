using UnityEngine;
using System.Collections;

public class BossStagePase3 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        entity.StartPattern(entity.Getpattern(BossEPattern.Pase3BossStartCircle));

        entity.StartTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle),3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.Boss),3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.GhostParent), 3.2f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossChainDown), 3.5f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossChainUp), 3.5f);

    }

    public override void Execute(BossStage entity)
    {
       
   
    }

    public override void Exit(BossStage entity)
    {
        
    }
}
