using UnityEngine;
public class BallMovementPattern : MonoBehaviour
{
    private void OnEnable()
    { 
        // ���Ⱑ �⺻ Enter ��� ���� �� 
        // setActive(true)�Ҷ����� ȣ��� 
        Debug.Log(gameObject.name);
        Invoke("CreateObject", 2);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        // ���Ⱑ ���� Exit ��� ���� �� 
        // setActive(false)�Ҷ����� ȣ��� 
    }

    public void CreateObject()
    {
        for (int i = 0; i < 20; i++)
        {
            float randomPosY = Random.Range(5.0f, 13.0f);

            float randomPosX = Random.Range(10.0f, 18.0f);

            GameObject go = Instantiate(square, new Vector3(randomPosX, randomPosY), Quaternion.identity);

            ParabolaMovement parabolaMovement = go.GetComponent<ParabolaMovement>();

            parabolaMovement.dir = Dir.LeftDown;
        }

        for (int i = 0; i < 20; i++)
        {
            float randomPosY = Random.Range(-5.0f, -13.0f);

            float randomPosX = Random.Range(10.0f, 18.0f);

            GameObject go = Instantiate(square, new Vector3(randomPosX, randomPosY), Quaternion.identity);

            ParabolaMovement parabolaMovement = go.GetComponent<ParabolaMovement>();

            parabolaMovement.dir = Dir.LeftUp;
        }

        for (int i = 0; i < 15; i++)
        {
            float randomPosY = Random.Range(-5.0f, 5.0f);

            float randomPosX = Random.Range(10.0f, 22.0f);

            GameObject go = Instantiate(square, new Vector3(randomPosX, randomPosY), Quaternion.identity);

            ParabolaMovement parabolaMovement = go.GetComponent<ParabolaMovement>();

            float randomAngle = Random.Range(0, 9.0f);

            parabolaMovement.SetAngleSpeed(randomAngle);

            float randomposXSpeed = Random.Range(-8.0f, -5.0f);

            parabolaMovement.SetSpeedX(randomposXSpeed);

            float randomposYSpeed = Random.Range(5.0f, 10.0f);

            parabolaMovement.SetSpeedY(randomposYSpeed);

        }
    }
    
    [SerializeField]
    private GameObject square;


}
