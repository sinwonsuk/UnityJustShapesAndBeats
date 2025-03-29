using UnityEngine;
using System.Collections;

public class BossStagePase4 : State<BossStage>
{
    public override void Enter(BossStage entity)
    {
        time = 0;

        entity.StartPattern(entity.Getpattern(BossEPattern.Pase3BossStartCircle));

        entity.StartTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle), 3.0f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.Pase3BossStartCircle), 3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BeatCamera), 3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.Pase4_idle_BossSlayer), 3.0f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.GhostParent), 3.2f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle), 3.2f);

        entity.Getpattern(BossEPattern.BossChainDown).transform.position = new Vector2(-8.3f, -4.4f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossChainDown), 3.5f);

        entity.Getpattern(BossEPattern.BossChainUp).transform.position = new Vector2(8.3f, 4.4f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossChainUp), 3.5f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.SquareRotation), 3.7f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.BossBulletSpawn), 4.0f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.BossBulletSpawn), 9.5f);

        entity.StartTimePattern(entity.Getpattern(BossEPattern.FadeToPinkCircle), 11.6f);

        entity.StopTimePattern(entity.Getpattern(BossEPattern.Pase4_idle_BossSlayer), 11.7f);

    }

    public override void Execute(BossStage entity)
    {
        time += Time.deltaTime;

        if (time > 7.3f)
        {
            entity.Getpattern(BossEPattern.BossChainUp).GetComponent<BossChainCircleSpawn>().StartReduceCircle();
            entity.Getpattern(BossEPattern.BossChainDown).GetComponent<BossChainCircleSpawn>().StartReduceCircle();
        }

        if (time > 8.7f)
        {
            entity.Getpattern(BossEPattern.SquareRotation).GetComponent<BossSquareRotation>().ReduceScaleChild();
        }

        if (time > 9.5f)
        {
            entity.Getpattern(BossEPattern.GhostParent).GetComponent<GhostParent>().ReduceScaleChild();
        }

        if (time > 11.8f)
        {
            entity.ChangeState(BossStagePase.Pase5);
            return;
        }
    }

    public override void Exit(BossStage entity)
    {
        GameObject[] gameObjects = new GameObject[]
        {
           entity.Getpattern(BossEPattern.BossChainDown),
           entity.Getpattern(BossEPattern.BossChainUp)
        };

        entity.DisableUnlisted(gameObjects);
    }

    float time = 0;
}
