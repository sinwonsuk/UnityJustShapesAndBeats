using UnityEngine;
using System.Collections;

public class BossStagePase1 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        entity.StartPattern(entity.Getpattern(BossEPattern.fire));
        entity.StopTimePattern(entity.Getpattern(BossEPattern.fire), 18);
        entity.StartPattern(entity.Getpattern(BossEPattern.white_one_0));
        entity.StartPattern(entity.Getpattern(BossEPattern.big_white_one_0));
        entity.StartPattern(entity.Getpattern(BossEPattern.idle_BossSlayer));

    }

    public override void Execute(BossStage entity)
    {
       
   
    }

    public override void Exit(BossStage entity)
    {
        
    }
}
