using UnityEngine;

public class UpDown : MonoBehaviour
{
    public Vector2 initialPosition = new Vector2(0, 0); // 초기 위치 (인스펙터에서 설정)

    void Start()
    {
        // 초기 위치 설정
        transform.position = new Vector3(initialPosition.x, initialPosition.y, transform.position.z);
    }

    void Update()
    {
        // 아래로 이동
        transform.Translate(Vector2.up * Time.deltaTime * 3);
    }
}
