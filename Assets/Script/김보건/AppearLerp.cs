using System.Collections;
using UnityEngine;

public class AppearLerp : MonoBehaviour
{
    [SerializeField] private GameObject appearCircle;
    [SerializeField] private Color targetColor = Color.white; // �ٲ� �� (���)
    [SerializeField] private float speed = 1f; // �����̴� �ӵ�

    private Color originalColor;
    private SpriteRenderer movingRenderer;

    private void Start()
    {
        movingRenderer = appearCircle.GetComponent<SpriteRenderer>();
        originalColor = movingRenderer.color; //���� ���� ����
    }

    private void Update()
    {
        StartCoroutine(AppearCircleLerp());
    }
   
    private IEnumerator AppearCircleLerp()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // ���� ����: ���� �� �� targetColor (���)
        movingRenderer.color = Color.Lerp(originalColor, targetColor, t);

        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
    }
}
