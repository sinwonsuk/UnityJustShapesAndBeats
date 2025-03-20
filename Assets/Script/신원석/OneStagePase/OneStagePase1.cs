using System.Collections;
using UnityEngine;

public class OneStagePase1 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        int a = 0;

    }

    public override void Execute(OneStage entity)
    {
        time += Time.deltaTime;

        if (time > 4)
        {
            entity.ChangeState(StagePase.Pase2);
            return;
        }      
    }

    public override void Exit(OneStage entity)
    {
        time = 0;
    }

    float time = 0;

}
