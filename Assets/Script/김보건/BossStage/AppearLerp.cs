using System.Collections;
using UnityEngine;

public class AppearLerp : MonoBehaviour
{
    [SerializeField] private GameObject appearCircle;
    [SerializeField] private Color targetColor = Color.white;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float DeleteTime = 1.4f;

    private Color originalColor;
    private SpriteRenderer movingRenderer;

    private void Start()
    {
        movingRenderer = appearCircle.GetComponent<SpriteRenderer>();
        originalColor = movingRenderer.color;
    }

    private void Update()
    {
        StartCoroutine(AppearCircleLerp());
    }
   
    private IEnumerator AppearCircleLerp()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);

     
        movingRenderer.color = Color.Lerp(originalColor, targetColor, t);

        yield return new WaitForSeconds(DeleteTime);
        gameObject.SetActive(false);
    }
}
