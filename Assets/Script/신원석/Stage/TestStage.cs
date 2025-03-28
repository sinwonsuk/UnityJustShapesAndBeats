using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.VersionControl.Asset;

public enum TestStagePase
{
    // �׽�Ʈ ���������Դϴ�
    �ſ��� = 0,
    ������,
    �ڿ���,
    �躸��,
    �ڻ��,
}

public enum TestEPattern
{
    // �ְ� ������ �������� 
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
        // �����Ŀ� �����ϰ� �ʹ�
        StartCoroutine(StartTimePatternCor(pattern, timeUpdate));
    }

    public void StopTimePattern(GameObject pattern, float timeUpdate)
    {
        // �����Ŀ� �����Ű�� �ʹ� 
        StartCoroutine(StopTimePatternCor(pattern, timeUpdate));
    }

    public IEnumerator StopTimePatternCor(GameObject pattern, float timeUpdate)
    {
        yield return new WaitForSeconds(timeUpdate);
        pattern.SetActive(false);
        yield break;
    }

    // �ٷ� �����ϰ� �ʹ�.
    public void StartPattern(GameObject pattern)
    {
        pattern.SetActive(true);
    }
    // �ٷ� ���߰� �ʹ�. 
    public void StopPattern(GameObject pattern)
    {
        pattern.SetActive(false);
    }

    [SerializeField]
    TestStagePase testStage = TestStagePase.�ſ���;

    public State<TestStage>[] states;
    private StateMachine<TestStage> stateMachine;

    [SerializeField]
    private GameObject[] pattern;

}

