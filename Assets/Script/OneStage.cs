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
		// ��� Ŭ������ Setup �޼ҵ� ȣ�� (ID, �̸�, ���� ����)
		base.Setup(name);

		// �����Ǵ� ������Ʈ �̸� ����
		gameObject.name = $"{ID:D2}_Student_{name}";

		// ���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ�, ù ���¸� ����
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

