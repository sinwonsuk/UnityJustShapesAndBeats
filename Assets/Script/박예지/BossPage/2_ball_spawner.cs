using UnityEngine;
using System.Collections;

public class ball_spawner2 : MonoBehaviour

{
    public float speed = 2f;             // Ÿ�� ������ ���� �ӵ�
    public float horizontalRadius = 3f;  // ���� ������
    public float verticalRadius = 5f;    // ���� ������
    public float bobbingSpeed = 1f;      // ���Ʒ��� �����̴� �ӵ�
    public float bobbingAmount = 0.5f;   // ���Ʒ��� �����̴� ����
    public Vector2 center;               // Ÿ���� �߽� (Inspector���� ���� ����)

    private float angle = 0f;            // ����
    public GameObject Bbullet;

    void Start()
    {
        StartCoroutine(StartDelay());
        if (center == Vector2.zero)
            center = transform.localPosition;

    }
    void Update()
    {
        //// Ÿ�� ��θ� ���� �����̱�
        angle += Time.deltaTime * speed;

        // Ÿ�� ��ο��� X, Y ���
        float x = center.x + Mathf.Cos(angle) * horizontalRadius;
        float y = center.y + Mathf.Sin(angle) * verticalRadius;

        // ���Ʒ��� �����̴� ȿ�� �߰�
        y += Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // ��ġ ������Ʈ
        transform.localPosition = new Vector2(x, y);
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f); // 2�� ���
        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(Bbullet, transform.position, Quaternion.Euler(0, 0, 30 * i));
        }

       
    }



}