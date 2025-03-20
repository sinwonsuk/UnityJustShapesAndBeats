using NUnit.Framework.Interfaces;
using UnityEngine;

public class OneStagePase2 : State<OneStage>
{
	public override void Enter(OneStage entity)
	{

	}

	public override void Execute(OneStage entity)
	{
		time += Time.deltaTime;
		entity.StartPattern(entity.Getpattern(EPattern.kickmove));
		if (time >= 7f && time < 7.1f)
		{
			entity.StartPattern(entity.Getpattern(EPattern.UppercutPattern));
			entity.StopPattern(entity.Getpattern(EPattern.kickmove));
		}
		else if (time >= 19f && time < 19.1f)
		{
			entity.StartPattern(entity.Getpattern(EPattern.HadoukenPattern));
		}
		else if (time >= 20f && time < 20.1f)
		{
			entity.StopPattern(entity.Getpattern(EPattern.UppercutPattern));
		}
		else if (time >= 20.3f)
		{
			entity.ChangeState(StagePase.Pase3);
		}
	}

	public override void Exit(OneStage entity)
	{
		time = 0f;
	}

	private float time = 0f;
}