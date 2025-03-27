using System.Collections;
using UnityEngine;

public class WagTail : MonoBehaviour
{
    void Start()
    {
        tailRenderer = GetComponent<SpriteRenderer>();
        currentRadius = minRadius;
    }

    void Update()
    {
        //�󸶳� ��鸱�� ���� ���
        currentAngle = Mathf.Sin(Time.time * speed) * angleRange + 90f;
        rad = currentAngle * Mathf.Deg2Rad;

        //�����̴� ����
        if (currentAngle < prevAngle)
        {
            moveLeft = true;  // �������� �̵�
        }
        else
        {
            moveLeft = false; // �����������̵�
        }

        // ���������� �������Ÿ� ���� 
        if (!moveLeft)
        {
            // �������� �̵�: �����ߴٰ� ����
            lerpTime += Time.deltaTime * radiusChangeSpeed;
            currentRadius = Mathf.Lerp(minRadius, maxRadius, Mathf.Sin(lerpTime * Mathf.PI));
        }
        else
        {
            // �����������̵� 
            lerpTime = 0f;
            currentRadius = minRadius;
        }

        // ��ġ ���
        Vector3 offSet = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * currentRadius;
        Vector3 circleTailPos = centerPoint.position + offSet;
        transform.position = circleTailPos;

        // ȸ��
        Vector3 dir = (circleTailPos - centerPoint.position).normalized;
        float lookAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle - 90f);

        //�¿����
        if (tailRenderer != null)
            tailRenderer.flipX = !moveLeft;

        prevAngle = currentAngle;
    }

    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minRadius = 1.2f;
    [SerializeField] private float maxRadius = 1.6f;
    [SerializeField] private float radiusChangeSpeed = 1.5f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float angleRange = 20f;

    private float currentAngle;
    private float prevAngle;
    private float currentRadius;
    private float rad;

    private SpriteRenderer tailRenderer;
    private bool moveLeft = false;
    private float lerpTime = 0f;
}
