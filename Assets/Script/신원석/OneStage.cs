using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public enum StagePase 
{ 
	Pase1 = 0,
    Pase2,
    Pase3,
    Pase4,
    Pase5,
    Pase6,
}

public enum EPattern
{
    Pattern1 = 0,
    Pattern2,
    Pattern3,
    Pattern4,
    Pattern5,
    Pattern6,
}




public class OneStage : BaseGameEntity
{
	public override void Setup(string name)
	{
		
        for (int i = 0; i < pattern.Length; i++)
		{
            pattern[i] = Instantiate(pattern[i], transform.position, Quaternion.identity);

			pattern[i].SetActive(false);
        }

		// ��� Ŭ������ Setup �޼ҵ� ȣ�� (ID, �̸�, ���� ����)
		base.Setup(name);

		// �����Ǵ� ������Ʈ �̸� ����
		gameObject.name = $"{ID:D2}_Student_{name}";

		// ���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ�, ù ���¸� ����
		stateMachine = new StateMachine<OneStage>();
		stateMachine.Setup(this, states[(int)StagePase.Pase1]);	
	}

	public override void Updated()
	{
		stateMachine.Execute();
	}

	public void ChangeState(StagePase newState)
	{
		stateMachine.ChangeState(states[(int)newState]);
	}
    public GameObject Getpattern(EPattern _pattern)
    {
		return pattern[(int)_pattern];
    }



    public State<OneStage>[] states;
    private StateMachine<OneStage> stateMachine;

	[SerializeField]
	private GameObject[] pattern;

}

