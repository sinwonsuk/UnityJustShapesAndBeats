using UnityEngine;

public class TwoStagePase2 : State<TwoStage>
{

    public override void Enter(TwoStage entity)
    {
        time = 0;
        //가로 여러줄 5초
        entity.GetPatternObject(TwoEPattern.BoxFadePatternManager, 0);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 5f);

        //세로 여러줄 6초부터 ~ 15초까지
        entity.GetPatternTimeObject(TwoEPattern.BoxFadePatternManager, 1, 6);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 6f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 14f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 15f);

        //상수님 패턴
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.Circle), 16f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.Circle), 27f);

        //가로세로 한 줄씩 16초부터  16초부터 28초까지
        entity.GetPatternTimeObject(TwoEPattern.BoxFadePatternManager, 4, 16);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 28f);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.SnowBall_fast), 21f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_fast), 30f);


    }

    public override void Execute(TwoStage entity)
    {
        time += Time.deltaTime;

        if(time > 31f)
        {
            entity.ChangeState(TwoStagePase.Pase3);
        }
    }

    public override void Exit(TwoStage entity)
    {
        time = 0;

    }
    private float time = 0;
}
