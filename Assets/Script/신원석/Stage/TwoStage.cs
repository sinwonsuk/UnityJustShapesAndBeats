using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEditor.VersionControl.Asset;

public enum TwoStagePase
{
    Pase1 = 0,
    Pase2,
    Pase3,
    Pase4,
    Pase5,
    Pase6,
}

public enum TwoEPattern
{
    UglyCircle,
    BoxFadePatternManager,
    SnowBall_fast,
    SnowBall_first,
    SnowBall_slow,
    Circle,
    ThreeCircle,
    miniball,
    miniball_2,
    triangle_spawner,
    triangle_spawner_2,
    ugly,
}
public class TwoStage : BaseGameEntity
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
            stateMachine = new StateMachine<TwoStage>();
        }
        stateMachine.Setup(this, states[(int)TwoStagePase.Pase1]);
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(TwoStagePase newState)
    {
        stateMachine.ChangeState(states[(int)newState]);
    }
    public GameObject Getpattern(TwoEPattern _pattern)
    {
        return pattern[(int)_pattern];
    }

    public GameObject GetPatternInstantiate(TwoEPattern _pattern, Vector2 position)
    {     
        GameObject go = Instantiate(pattern[(int)_pattern], position, Quaternion.identity);
        go.SetActive(true);
        return pattern[(int)_pattern];
    }

    public GameObject GetPatternObject(TwoEPattern _pattern, int choice)
    {
        pattern[(int)_pattern].SetActive(true);

        PatternChoiceInterface go = pattern[(int)_pattern].GetComponent<PatternChoiceInterface>();

        if (go != null)
        {
            go.SetPattern(choice);
        }
        return pattern[(int)_pattern];
    }
    public void GetPatternTimeObject(TwoEPattern _pattern, int choice,int time)
    {
        StartCoroutine(GetPatternTimeObjectCor(_pattern, choice, time));
    }

    public IEnumerator GetPatternTimeObjectCor(TwoEPattern _pattern, int choice, int time)
    {
        yield return new WaitForSeconds(time);

        pattern[(int)_pattern].SetActive(true);

        PatternChoiceInterface go = pattern[(int)_pattern].GetComponent<PatternChoiceInterface>();

        if (go != null)
        {
            go.SetPattern(choice);
        }
        yield break;
    }

    public void GetPatternTimeInstantiate(TwoEPattern _pattern, Vector2 position, float timeUpdate)
    {
        StartCoroutine(GetPatternTimeInstantiateCor(_pattern,position, timeUpdate));
    }
    public IEnumerator GetPatternTimeInstantiateCor(TwoEPattern _pattern, Vector2 position, float timeUpdate)
    {
        yield return new WaitForSeconds(timeUpdate);
        GameObject go = Instantiate(pattern[(int)_pattern], position, Quaternion.identity);

        go.SetActive(true);

        yield break;
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

    public State<TwoStage>[] states;
    private StateMachine<TwoStage> stateMachine;

    [SerializeField]
    private GameObject[] pattern;
}

