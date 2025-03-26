using UnityEngine;

public class AppearLerp : MonoBehaviour
{
    [SerializeField] private GameObject appearCircle;
    [SerializeField] private Color targetColor = Color.white; // 원래 색 (흰색과 왔다갔다)
    [SerializeField] private float speed = 1f; // 색 전환 속도

    private Color originalColor;
    private SpriteRenderer movingRenderer;


    private void Start()
    {
        movingRenderer = appearCircle.GetComponent<SpriteRenderer>();
        originalColor = movingRenderer.color;
    }

    private void Update()
    {
        if (movingRenderer != null)
        {
            AppearCircleLerp();
        }
    }

    void AppearCircleLerp()
    {
        // PingPong으로 0~1 반복
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // 색상 보간
        movingRenderer.color = Color.Lerp(originalColor, targetColor, t);
    }
}
