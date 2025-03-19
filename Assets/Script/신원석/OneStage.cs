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
		
        for (int i = 0; i < pattern.Length; i++)
		{
            pattern[i] = Instantiate(pattern[i], transform.position, Quaternion.identity);
        }

		// ��� Ŭ������ Setup �޼ҵ� ȣ�� (ID, �̸�, ���� ����)
		base.Setup(name);

		// �����Ǵ� ������Ʈ �̸� ����
		gameObject.name = $"{ID:D2}_Student_{name}";

		// ���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ�, ù ���¸� ����
		stateMachine = new StateMachine<OneStage>();
		stateMachine.Setup(this, pattern[(int)StagePattern.Stage].GetComponent<State<OneStage>>());	
	}

	public override void Updated()
	{
		stateMachine.Execute();
	}

	public void ChangeState(StagePattern newState)
	{	
        stateMachine.ChangeState(pattern[(int)newState].GetComponent<State<OneStage>>());
	}

    public State<OneStage>[] states;
    private StateMachine<OneStage> stateMachine;

	[SerializeField]
	private GameObject[] pattern;

}

