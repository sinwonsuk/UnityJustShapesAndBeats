using UnityEngine;

public class TwoStagePase2 : State<TwoStage>
{

    public override void Enter(TwoStage entity)
    {
        time = 0;
        //���� ������ 5��
        entity.GetPatternObject(TwoEPattern.BoxFadePatternManager, 0);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 5f);

        //���� ������ 6�ʺ��� ~ 15�ʱ���
        entity.GetPatternTimeObject(TwoEPattern.BoxFadePatternManager, 1, 6);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 6f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 14f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.BoxFadePatternManager), 15f);

        //����� ����
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.Circle), 16f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.Circle), 27f);

        //���μ��� �� �پ� 16�ʺ���  16�ʺ��� 28�ʱ���
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
