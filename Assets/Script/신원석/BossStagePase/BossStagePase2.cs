using UnityEngine;
using System.Collections;

public class BossStagePase2 : State<BossStage>
{  
    public override void Enter(BossStage entity)
    {
        time = 0;
		// 34 ~ 49
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 0);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossBbababam), 0.5f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 2f);
        entity.StartTimePattern(entity.Getpattern(BossEPattern.BigSnailSpawner), 5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 7f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 7f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 8f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.SmileBossSpawner), 12f);
		

	}

    public override void Execute(BossStage entity)
    {
        time += Time.deltaTime;
        if(time > 16f)
        {
			entity.ChangeState(BossStagePase.Pase3);
        }
    }

    public override void Exit(BossStage entity)
    {
	}

    private float time = 0;
}
