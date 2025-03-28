using TMPro;
using UnityEngine;

public class TwoStagePase3 : State<TwoStage>
{

    public override void Enter(TwoStage entity)
    {
        entity.GetPatternInstantiate(TwoEPattern.ugly, new Vector2(0, 0));
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 1);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 2);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 3);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 4);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 5);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 6);

        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 13);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 13);

        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 13.2f);
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 13.2f);

        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(0, 0), 13.3f);

        GameObject boxPattern = entity.Getpattern(TwoEPattern.BoxFadePatternManager);
        BoxFadePatternSpawnManager manager = boxPattern.GetComponent<BoxFadePatternSpawnManager>();
        manager.SetPattern((int)T.Three);

        entity.StartTimePattern((boxPattern), 9f);

        entity.StopTimePattern((boxPattern), 35f);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 15.5f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 30f);

        entity.StartTimePattern(entity.Getpattern(TwoEPattern.TwoNextSceneAnimation),38);

        entity.Getpattern(TwoEPattern.TwoNextSceneAnimation).GetComponent<NextSceneTriangleSpawner>().entity = entity;
    }

    public override void Execute(TwoStage entity)
    {
      

    }

    public override void Exit(TwoStage entity)
    {
        time = 0;
    }

    private float time = 0;
}
