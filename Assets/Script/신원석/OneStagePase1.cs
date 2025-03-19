using UnityEngine;

public class OneStagePase1 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        GameObject pattern1 = entity.Getpattern(EPattern.Pattern1);
        pattern1.SetActive(true);

        GameObject pattern2 = entity.Getpattern(EPattern.Pattern2);
        pattern2.SetActive(true);

    }

    public override void Execute(OneStage entity)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            entity.ChangeState(StagePase.Pase2);
        }
    }

    public override void Exit(OneStage entity)
    {
        GameObject pattern1 = entity.Getpattern(EPattern.Pattern1);
        pattern1.SetActive(false);

        GameObject pattern2 = entity.Getpattern(EPattern.Pattern2);
        pattern2.SetActive(false);
    }
}
