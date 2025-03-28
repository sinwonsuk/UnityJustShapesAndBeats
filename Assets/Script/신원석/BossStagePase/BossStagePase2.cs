using UnityEngine;
using System.Collections;

public class BossStagePase2 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        time = 0;
		// 34 ~ 49
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 5);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossBbababam), 5.5f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 7f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 9f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.BigSnailSpawner), 14f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 17f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 18f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 20f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.SmileBossSpawner), 25f);

	}

    public override void Execute(BossStage entity)
    {
        time += Time.deltaTime;
        if(time > 28f)
        {
            entity.ChangeState(BossStagePase.Pase3);
        }
    }

    public override void Exit(BossStage entity)
    {
        entity.StopPattern(entity.Getpattern(BossEPattern.SmileBossSpawner));
    }

    private float time = 0;
}
