using UnityEngine;
using System.Collections;

public class MyScript3 : State<OneStage>
{


    public override void Enter(OneStage entity)
    {
        entity.StartPattern(entity.Getpattern(EPattern.kickmove));
        entity.StartPattern(entity.Getpattern(EPattern.UppercutPattern));
     

        entity.StopTimePattern(entity.Getpattern(EPattern.kickmove),14);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern),14);
     


    public override void Enter(OneStage entity)
    {


    }

    public override void Execute(OneStage entity)
    {


    }

    public override void Exit(OneStage entity)
    {

    }

}