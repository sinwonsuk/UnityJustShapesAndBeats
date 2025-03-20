using UnityEngine;
using System.Collections;

public class MyScript3 : State<OneStage>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Enter(OneStage entity)
    {
        entity.StartPattern(entity.Getpattern(EPattern.kickmove));
        entity.StartPattern(entity.Getpattern(EPattern.UppercutPattern));
     

        entity.StopTimePattern(entity.Getpattern(EPattern.kickmove),14);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern),14);
     
    }

    public override void Execute(OneStage entity)
    {

        
    }

    public override void Exit(OneStage entity)
    {

    }

}