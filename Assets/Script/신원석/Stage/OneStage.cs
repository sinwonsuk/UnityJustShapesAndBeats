using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.VersionControl.Asset;

public enum StagePase 
{ 
	Pase1 = 0,
    Pase2,
    Pase3,
    Pase4,
    Pase5,
    Pase6,
    Pase7,
    Pase8,
    Pase9
}

public enum EPattern
{
    BallMovementPattern = 0,
    BigBallMovementPattern,
    kickMove,
    HadoukenPattern,
    UppercutPattern,
    BulletLuncher,
    map_first,
    map_fastCreate,
    map_fastCreate_shake,
    map_shake
}
public class OneStage : BaseGameEntity
{


    void Start()
    {
      
    }


	public override void Setup()
	{
        if (stateMachine == null)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                pattern[i].gameObject.SetActive(false);

                pattern[i] = Instantiate(pattern[i], transform.position, Quaternion.identity);

                pattern[i].SetActive(false);
            }
        }
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

    public IEnumerator StartTimePatternCor(GameObject pattern, float timeUpdate)
    {
        yield return new WaitForSeconds(timeUpdate);
        pattern.SetActive(true);
        yield break;
    }

    public void StartTimePattern(GameObject pattern, float timeUpdate)
    {
        // 몇초후에 시작하고 싶다
        StartCoroutine(StartTimePatternCor(pattern, timeUpdate));
    }

    public void StopTimePattern(GameObject pattern, float timeUpdate)
    {
        // 몇초후에 종료시키고 싶다 
        StartCoroutine(StopTimePatternCor(pattern,timeUpdate));
    }

    public IEnumerator StopTimePatternCor(GameObject pattern, float timeUpdate)
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

