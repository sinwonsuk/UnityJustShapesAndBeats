using TMPro;
using UnityEngine;

public class TwoStagePase3 : State<TwoStage>
{

    public override void Enter(TwoStage entity)
    {
        entity.GetPatternInstantiate(TwoEPattern.ugly, new Vector2(0, 0));

        GameObject boxPattern = entity.Getpattern(TwoEPattern.BoxFadePatternManager);
        BoxFadePatternSpawnManager manager = boxPattern.GetComponent<BoxFadePatternSpawnManager>();
        manager.SetPattern((int)T.Three);

        entity.StartTimePattern((boxPattern), 9f);
        entity.StartTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 15.5f);
        entity.StopTimePattern(entity.Getpattern(TwoEPattern.SnowBall_slow), 30f);
    }

    public override void Execute(TwoStage entity)
    {
       
    }

    public override void Exit(TwoStage entity)
    {

    }

}
