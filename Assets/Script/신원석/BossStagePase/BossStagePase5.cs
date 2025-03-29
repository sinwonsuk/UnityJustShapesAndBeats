using UnityEngine;
using System.Collections;

public class BossStagePase5 : State<BossStage>
{
	public override void Enter(BossStage entity)
	{
		time = 0;

        entity.StartPattern(entity.Getpattern(BossEPattern.BeatCamera));
        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossBbababam), 0.5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 0);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 2f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.BigSnailSpawner), 5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 7f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 7f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 8f);;
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 11f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 12.5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.SmileBossSpawner), 17f);

	}

	public override void Execute(BossStage entity)
	{
		time += Time.deltaTime;
		if (time > 20f)
		{
			entity.ChangeState(BossStagePase.Pase6);
		}
	}

	public override void Exit(BossStage entity)
	{
        entity.AllStop();
    }

	private float time = 0;
}
