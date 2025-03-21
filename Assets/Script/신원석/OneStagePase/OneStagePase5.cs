using UnityEngine;

public class OneStagePase5 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        time = 0;

        entity.StartPattern(entity.Getpattern(EPattern.kickMove));
        entity.StopTimePattern(entity.Getpattern(EPattern.kickMove), 15f);

        entity.StartTimePattern(entity.Getpattern(EPattern.UppercutPattern), 7f);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern), 13.9f);

        entity.StartTimePattern(entity.Getpattern(EPattern.UppercutPattern), 15f);
        entity.StartTimePattern(entity.Getpattern(EPattern.HadoukenPattern), 19f);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern), 23f);

    }

    public override void Execute(OneStage entity)
    {
        //time += Time.deltaTime;

        //if (time >= 25)
        //{
        //    entity.ChangeState(StagePase.Pase2);
        //    return;
        //}
    }

    public override void Exit(OneStage entity)
    {
        time = 0;
    }

    float time = 0;
}
