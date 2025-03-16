using UnityEngine;

public class ttttt : State<Student>
{
    public override void Enter(Student entity)
    {
        // 장소를 집으로 설정하고, 집에오면 스트레스가 0이 된다.
        entity.CurrentLocation = Locations.SweetHome;
        entity.Stress = 0;

        entity.PrintText("tt 시작하다.");
    }

    public override void Execute(Student entity)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.ChangeState(StudentStates.Stage1);
        }

        entity.PrintText("tt 업데이트중");

        // 피로가 0이 아니면
        if (entity.Fatigue > 0)
        {
            // 피로 10씩 감소
            entity.Fatigue -= 10;
        }
        // 피로가 0이면
      
    }

    public override void Exit(Student entity)
    {
        entity.PrintText("tt 끝나다.");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
