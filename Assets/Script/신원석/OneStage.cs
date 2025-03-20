using System.Collections;
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

		// 기반 클래스의 Setup 메소드 호출 (ID, 이름, 색상 설정)
		base.Setup(name);

		// 생성되는 오브젝트 이름 설정
		gameObject.name = $"{ID:D2}_Student_{name}";

		// 상태를 관리하는 StateMachine에 메모리를 할당하고, 첫 상태를 설정
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

    // 몇초후에 시작하고 싶다
    public IEnumerator StartTimePattern(GameObject pattern, float timeUpdate)
    {
        yield return new WaitForSeconds(timeUpdate);
        pattern.SetActive(true);
        yield break;
    }

    // 몇초후에 종료시키고 싶다 
    public IEnumerator FinsihTimePattern(GameObject pattern, float timeUpdate)
    {
        yield return new WaitForSeconds(timeUpdate);
        pattern.SetActive(false);
        yield break;
    }

    // 바로 시작하고 싶다.
    public void StartPattern(GameObject pattern)
    {
        pattern.SetActive(true);
    }
    // 바로 멈추고 싶다. 
    public void StopPattern(GameObject pattern)
    {
        pattern.SetActive(false);
    }

    public State<OneStage>[] states;
    private StateMachine<OneStage> stateMachine;

	[SerializeField]
	private GameObject[] pattern;

}

