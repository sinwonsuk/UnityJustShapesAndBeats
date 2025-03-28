using UnityEngine;
using System.Collections;

public class BossStagePase5 : State<BossStage>
{
	public override void Enter(BossStage entity)
	{
		time = 0;
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 0);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.BossBbababam), 0.5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 2f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.BigSnailSpawner), 5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 7f);
		entity.StopTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 6.9f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 7f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 8f);
		entity.StopTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 12f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.DotShootSpawner), 10f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.MiniBossSpawner), 12.5f);
		entity.StartTimePattern(entity.Getpattern(BossEPattern.SmileBossSpawner), 17f);

	}

	public override void Execute(BossStage entity)
	{
		time += Time.deltaTime;
		if (time > 20f)
		{
			entity.ChangeState(BossStagePase.Pase5);
		}
	}

	public override void Exit(BossStage entity)
	{
		entity.StopPattern(entity.Getpattern(BossEPattern.DotShootSpawner));
		entity.StopPattern(entity.Getpattern(BossEPattern.MiniBossSpawner));
		entity.StopPattern(entity.Getpattern(BossEPattern.BigSnailSpawner));
		entity.StopPattern(entity.Getpattern(BossEPattern.SmileBossSpawner));
	}

	private float time = 0;
}
