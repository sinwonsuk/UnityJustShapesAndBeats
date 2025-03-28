using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.VersionControl.Asset;

public enum TestStagePase
{
    // 테스트 스테이지입니다
    신원석 = 0,
    강다은,
    박예지,
    김보건,
    박상수,
}

public enum TestEPattern
{
    // 넣고 싶은거 넣으세요 
    BallMovementPattern = 0,
    BigBallMovementPattern,
    kickmove,
    HadoukenPattern,
    UppercutPattern,
    Pattern6,
}
public class TestStage : BaseGameEntity
{
    void Start()
    {
       
    }

    public override void OffActive()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i].gameObject == null)
                continue;
            pattern[i].SetActive(false);
        }

        gameObject.SetActive(false);
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
            stateMachine = new StateMachine<TestStage>();
        }
        stateMachine.Setup(this, states[(int)testStage]);
    }
    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(TestStagePase newState)
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
        StartCoroutine(StopTimePatternCor(pattern, timeUpdate));
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

    [SerializeField]
    TestStagePase testStage = TestStagePase.신원석;

    public State<TestStage>[] states;
    private StateMachine<TestStage> stateMachine;

    [SerializeField]
    private GameObject[] pattern;

}

