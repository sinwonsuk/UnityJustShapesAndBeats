using UnityEngine;

public class OneStagePase5 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        time = 0;

        entity.StartPattern(entity.Getpattern(EPattern.kickMove));
        entity.StopTimePattern(entity.Getpattern(EPattern.kickMove), 15f);

        entity.StopTimePattern(entity.Getpattern(EPattern.map_fastCreate), 6f);

        entity.StartTimePattern(entity.Getpattern(EPattern.map_fastCreate_shake), 6f);


        entity.StartTimePattern(entity.Getpattern(EPattern.UppercutPattern), 4f);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern), 13.9f);

        entity.StopTimePattern(entity.Getpattern(EPattern.map_fastCreate_shake), 15.9f);

        entity.StartTimePattern(entity.Getpattern(EPattern.map_fastCreate), 15.8f);
        entity.StopTimePattern(entity.Getpattern(EPattern.map_fastCreate), 17f);

        entity.StartTimePattern(entity.Getpattern(EPattern.map_fastCreate_shake), 17f);


        entity.StartTimePattern(entity.Getpattern(EPattern.UppercutPattern), 15f);
        entity.StartTimePattern(entity.Getpattern(EPattern.HadoukenPattern), 19f);
        entity.StopTimePattern(entity.Getpattern(EPattern.map_fastCreate_shake), 23f);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern), 23f);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern), 23f);

        entity.StartTimePattern(entity.Getpattern(EPattern.NextSceneAnimation), 27.0f);
        entity.Getpattern(EPattern.NextSceneAnimation).GetComponent<NextSceneTriangleSpawner>().entity = entity;
    }

    public override void Execute(OneStage entity)
    {

    }

    public override void Exit(OneStage entity)
    {
        entity.AllStop();

        time = 0;
    }

    float time = 0;
}
