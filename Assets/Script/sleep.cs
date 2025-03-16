using UnityEngine;

public class sleep : State<Student>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter(Student entity)
    {
        // ��Ҹ� ������ �����ϰ�, �������� ��Ʈ������ 0�� �ȴ�.
        entity.CurrentLocation = Locations.SweetHome;
        entity.Stress = 0;

        entity.PrintText("�� ����.");
    }

    public override void Execute(Student entity)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.ChangeState(StudentStates.Stage);
        }

        entity.PrintText("�� ������Ʈ �� ");

        // �Ƿΰ� 0�� �ƴϸ�
        if (entity.Fatigue > 0)
        {
            // �Ƿ� 10�� ����
            entity.Fatigue -= 10;
        }
        // �Ƿΰ� 0�̸�
        else
        {

        }
    }

    public override void Exit(Student entity)
    {
        entity.PrintText("�� ������.");
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
