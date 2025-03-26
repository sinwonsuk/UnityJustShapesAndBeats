using UnityEngine;

public class WagSlayerTail : MonoBehaviour
{
    void Start()
    {
        tailRenderer = GetComponent<SpriteRenderer>();
        currentRadius = minRadius;
    }

    void Update()
    {
        // �θ��� scale.x ���� ���� ��� (���� ũ�� 1�� �� ���)
        float scaleFactor = transform.parent.localScale.x;

        // 1. ���콺 ���� (�����̾� ���� �����Ͽ� ����)
        float scaledMinRadius = minRadius * scaleFactor;
        float scaledMaxRadius = maxRadius * scaleFactor;

        // 2. ���� ���
        currentAngle = Mathf.Sin(Time.time * speed) * angleRange + 90f;
        rad = currentAngle * Mathf.Deg2Rad;

        if (currentAngle < prevAngle)
            moveLeft = true;
        else
            moveLeft = false;

        if (!moveLeft)
        {
            lerpTime += Time.deltaTime * radiusChangeSpeed;
            currentRadius = Mathf.Lerp(scaledMinRadius, scaledMaxRadius, Mathf.Sin(lerpTime * Mathf.PI));
        }
        else
        {
            lerpTime = 0f;
            currentRadius = scaledMinRadius;
        }

        Vector3 offSet = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * currentRadius;
        Vector3 circleTailPos = centerPoint.position + offSet;
        transform.position = circleTailPos;

        Vector3 dir = (circleTailPos - centerPoint.position).normalized;
        float lookAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle - 90f);

        if (tailRenderer != null)
            tailRenderer.flipX = !moveLeft;

        prevAngle = currentAngle;
    }

    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minRadius = 1.8f;
    [SerializeField] private float maxRadius = 2.3f;
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
