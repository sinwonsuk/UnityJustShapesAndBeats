using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class FadeToPinkCircle : MonoBehaviour
{

    private void OnDisable()
    {
        spriteRenderer.color = firstColor;
        transform.localScale = Vector3.one;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        firstColor = spriteRenderer.color;

        targetColor = new Color(1f, 0.125f, 0.439f);
    }


    void Update()
    {

        if (transform.localScale.x > scaleLimit)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * reduceScaleSpeed;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, lerpSpeed * Time.deltaTime);
        }

        if (transform.localScale.x < 0.01f)
        {
            spriteRenderer.color = targetColor;
            gameObject.SetActive(false);
        }
    }

    [SerializeField]
    private float reduceScaleSpeed;

    [SerializeField]
    private float lerpSpeed = 1;

    [SerializeField]
    private float scaleLimit = 0.4f;


    private SpriteRenderer spriteRenderer;

    private Color targetColor;

    private Color firstColor;
}
