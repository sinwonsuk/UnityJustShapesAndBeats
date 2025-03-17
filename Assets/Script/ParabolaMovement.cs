using UnityEngine;

public enum Dir
{
    ParabolaMiddle,
    LeftDown,
    LeftUp,
}


public class ParabolaMovement : MonoBehaviour
{
    void Start()
    {
      
    }
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime* rotateSpeed);

        switch (dir)
        {
            case Dir.ParabolaMiddle:
                {
                    float posY = Mathf.Sin(angle) * Time.deltaTime * speedY;
                    angle += Time.deltaTime * angleSpeed;
                    float posX = Time.deltaTime * speedX;
                    transform.Translate(posX, posY, 0);
                }
                break;
            case Dir.LeftDown:
                {
                    transform.Translate(new Vector2(-1,-1)*speed * Time.deltaTime);
                }
                break;
            case Dir.LeftUp:
                {
                    transform.Translate(new Vector2(-1, 1) * speed * Time.deltaTime);
                }
                break;
            default:
                break;
        }   
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetSpeedY(float _speedY)
    {
        speedY = _speedY;
    }
    public void SetSpeedX(float _speedX)
    {
        speedX = _speedX;
    }
    public void SetAngleSpeed(float _angleSpeed)
    {
        angleSpeed = _angleSpeed;
    }

    public float speed { get; set; } = 4.5f;

    public Dir dir = Dir.ParabolaMiddle;

    [SerializeField]
    private float speedY = 1;
    [SerializeField]
    private float speedX = -5;
    [SerializeField]
    private float angleSpeed = 6.0f;

    private float angle = 1;

    private float rotateSpeed = 3;
}