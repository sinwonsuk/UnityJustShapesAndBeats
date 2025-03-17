using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PinkFadingColor : MonoBehaviour
{

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        targetColor = new Color(1f, 0.125f, 0.439f);
    }


    void Update()
    {
        
        if (transform.localScale.x > scaleLimit && transform.position.x < 8.5f)
        {          
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * reduceScaleSpeed;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, lerpSpeed * Time.deltaTime);
        }
      


        if (Vector4.Distance(spriteRenderer.color, targetColor) < 0.01f)
        {
            spriteRenderer.color = targetColor;
        }


    }

    [SerializeField]
    private float reduceScaleSpeed;

    [SerializeField]
    private float lerpSpeed = 1;

    [SerializeField]
    private float scaleLimit = 0.4f;


    private SpriteRenderer spriteRenderer;

    private  Color targetColor;

}
