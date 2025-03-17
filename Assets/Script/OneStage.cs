using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public enum StagePattern 
{ 
	Stage = 0,
    Stage1,
    Stage2,
    PlayAGame, 
	HitTheBottle 
}

public class OneStage : BaseGameEntity
{
	public override void Setup(string name)
	{
		// 기반 클래스의 Setup 메소드 호출 (ID, 이름, 색상 설정)
		base.Setup(name);

		// 생성되는 오브젝트 이름 설정
		gameObject.name = $"{ID:D2}_Student_{name}";

		// 상태를 관리하는 StateMachine에 메모리를 할당하고, 첫 상태를 설정
		stateMachine = new StateMachine<OneStage>();
		stateMachine.Setup(this, states[(int)StagePattern.Stage]);	
	}

	public override void Updated()
	{
		stateMachine.Execute();
	}

	public void ChangeState(StagePattern newState)
	{
		states[(int)newState].gameObject.SetActive(true);
        stateMachine.ChangeState(states[(int)newState]);
	}

    public State<OneStage>[] states;
    private StateMachine<OneStage> stateMachine;
}

