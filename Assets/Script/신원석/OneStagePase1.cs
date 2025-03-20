using System.Collections;
using UnityEngine;

public class OneStagePase1 : State<OneStage>
{
    public override void Enter(OneStage entity)
    {
        //entity.StartPattern(entity.Getpattern(EPattern.));
    }

    public override void Execute(OneStage entity)
    {
        GameObject pattern2 = entity.Getpattern(EPattern.Pattern2);
        pattern2.SetActive(true);
      
    }

    public override void Exit(OneStage entity)
    {
        GameObject pattern1 = entity.Getpattern(EPattern.Pattern1);
        pattern1.SetActive(false);

        //GameObject pattern2 = entity.Getpattern(EPattern.Pattern2);
        //pattern2.SetActive(false);
    }



}
