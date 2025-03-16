using UnityEngine;

public class sleep : State<Student>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter(Student entity)
    {
        // 장소를 집으로 설정하고, 집에오면 스트레스가 0이 된다.
        entity.CurrentLocation = Locations.SweetHome;
        entity.Stress = 0;

        entity.PrintText("잠 시작.");
    }

    public override void Execute(Student entity)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.ChangeState(StudentStates.Stage);
        }

        entity.PrintText("잠 업데이트 중 ");

        // 피로가 0이 아니면
        if (entity.Fatigue > 0)
        {
            // 피로 10씩 감소
            entity.Fatigue -= 10;
        }
        // 피로가 0이면
        else
        {

        }
    }

    public override void Exit(Student entity)
    {
        entity.PrintText("잠 끝나다.");
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
