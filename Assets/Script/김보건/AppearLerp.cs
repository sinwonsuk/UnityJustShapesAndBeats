using System.Collections;
using UnityEngine;

public class AppearLerp : MonoBehaviour
{
    [SerializeField] private GameObject appearCircle;
    [SerializeField] private Color targetColor = Color.white; // 바뀔 색 (흰색)
    [SerializeField] private float speed = 1f; // 깜빡이는 속도

    private Color originalColor;
    private SpriteRenderer movingRenderer;

    private void Start()
    {
        movingRenderer = appearCircle.GetComponent<SpriteRenderer>();
        originalColor = movingRenderer.color; //원래 색상 저장
    }

    private void Update()
    {
        StartCoroutine(AppearCircleLerp());
    }
   
    private IEnumerator AppearCircleLerp()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);

        // 색상 보간: 원래 색 → targetColor (흰색)
        movingRenderer.color = Color.Lerp(originalColor, targetColor, t);

        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
    }
}
