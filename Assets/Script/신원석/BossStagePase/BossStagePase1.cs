using UnityEngine;
using System.Collections;

public class BossStagePase1 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        time = 0;
        entity.StartPattern(entity.Getpattern(BossEPattern.fire));
        entity.StopTimePattern(entity.Getpattern(BossEPattern.fire),17);
        entity.StartPattern(entity.Getpattern(BossEPattern.white_one_0));                       
        entity.StartPattern(entity.Getpattern(BossEPattern.big_white_one_0));
        entity.StartPattern(entity.Getpattern(BossEPattern.idle_BossSlayer));
        entity.StopTimePattern(entity.Getpattern(BossEPattern.idle_BossSlayer), 33);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.Boss_BossBulletSpawn), 18);
        entity.StopTimePattern(entity.Getpattern(BossEPattern.Boss_BossBulletSpawn), 32f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.boss_moom_circle), 33);

        
    }
    float time = 0;
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
}
