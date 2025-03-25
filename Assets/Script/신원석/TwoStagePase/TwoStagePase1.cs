using UnityEngine;

public class TwoStagePase1 : State<TwoStage>
{

    public override void Enter(TwoStage entity)
    {
        entity.GetPatternInstantiate(TwoEPattern.ugly, new Vector2(2, 2));
        entity.GetPatternTimeInstantiate(TwoEPattern.ugly, new Vector2(1,1), 2.0f);

    }

    public override void Execute(TwoStage entity)
    {
       
    }

    public override void Exit(TwoStage entity)
    {

    }
}
