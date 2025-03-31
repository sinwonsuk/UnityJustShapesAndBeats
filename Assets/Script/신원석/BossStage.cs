using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStagePase
{
    Pase1 = 0,
    Pase2,
    Pase3,
    Pase4,
    Pase5,
    Pase6,
}

public enum BossEPattern
{
    Pase3BossStartCircle,
    FadeToPinkCircle,
    Boss,
    GhostParent,
    SquareRotation,
    BossChainDown,
    BossChainUp,
    BossBulletSpawn,
    idle_BossSlayer,
    big_white_one_0,
    Boss_BossBulletSpawn,
    fire,
    one_0,
    white_one_0,
    SpawnSlayerPattern1,
    SpawnSlayerPattern2,
    BossSlayer_finish,
    BossBbababam,
    BigSnailSpawner,
    DotShootSpawner,
    MiniBossSpawner,
    SmileBossSpawner,
    boss_moom_circle,
    Pase4_idle_BossSlayer,
    BeatCamera,
    ShakeCamera

}
public class BossStage : BaseGameEntity
{
    void Start()
    {
        SoundManager.GetInstance().PlayBgm(SoundManager.bgm.Stage3);
        SoundManager.GetInstance().SetSoundBgm(0.1f);
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
            stateMachine = new StateMachine<BossStage>();
        }
        stateMachine.Setup(this, states[(int)BossStagePase.Pase1]);
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

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(BossStagePase newState)
    {
        stateMachine.ChangeState(states[(int)newState]);
    }
    public GameObject Getpattern(BossEPattern _pattern)
    {
        return pattern[(int)_pattern];
    }

    public GameObject GetPatternInstantiate(BossEPattern _pattern, Vector2 position)
    {
        GameObject go = Instantiate(pattern[(int)_pattern], position, Quaternion.identity);
        go.SetActive(true);
        return pattern[(int)_pattern];
    }


    public GameObject GetPatternObject(BossEPattern _pattern, int choice)
    {


        PatternChoiceInterface go = pattern[(int)_pattern].GetComponent<PatternChoiceInterface>();


        if (go != null)
        {
            go.SetPattern(choice);
            pattern[(int)_pattern].SetActive(true);
        }
        return pattern[(int)_pattern];
    }
    public void GetPatternTimeObject(BossEPattern _pattern, int choice, int time)
    {
        StartCoroutine(GetPatternTimeObjectCor(_pattern, choice, time));
    }


    public IEnumerator GetPatternTimeObjectCor(BossEPattern _pattern, int choice, int time)
    {
        yield return new WaitForSeconds(time);



        PatternChoiceInterface go = pattern[(int)_pattern].GetComponent<PatternChoiceInterface>();



        if (go != null)
        {
            go.SetPattern(choice);
            pattern[(int)_pattern].SetActive(true);
        }
        yield break;
    }

    public void GetPatternTimeInstantiate(BossEPattern _pattern, Vector2 position, float timeUpdate)
    {
        StartCoroutine(GetPatternTimeInstantiateCor(_pattern, position, timeUpdate));
    }
    public IEnumerator GetPatternTimeInstantiateCor(BossEPattern _pattern, Vector2 position, float timeUpdate)
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

    public void AllStop()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i].SetActive(false);
        }
    }
    public void DisableUnlisted(GameObject[] pattern)
    {
        var patternSet = new HashSet<GameObject>(pattern);

        foreach (var obj in this.pattern)
        {
            if (!patternSet.Contains(obj))
            {
                obj.SetActive(false);
            }
        }
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

    public State<BossStage>[] states;
    private StateMachine<BossStage> stateMachine;


    [SerializeField]
    private GameObject[] pattern;
}

