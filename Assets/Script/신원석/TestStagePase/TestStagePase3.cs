using UnityEngine;
using System.Collections;

public class TestStagePase3 : State<TestStage>
{
    public override void Enter(TestStage entity)
    {
        entity.StartPattern(entity.Getpattern(EPattern.kickMove));
        entity.StartPattern(entity.Getpattern(EPattern.UppercutPattern));


        entity.StopTimePattern(entity.Getpattern(EPattern.kickMove), 14);
        entity.StopTimePattern(entity.Getpattern(EPattern.UppercutPattern), 14);

        
       
    }


    public override void Execute(TestStage entity)
    {

     
    }

    public override void Exit(TestStage entity)
    {
     
    }
}
