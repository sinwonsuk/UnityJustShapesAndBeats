using System.Collections;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class FadeWhiteCircle : MonoBehaviour
{
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        targetColor = new Color(1f, 0.125f, 0.439f);

        StartCoroutine(MoveUpDown());
    }


    void Update()
    {

        if (transform.localScale.x > scaleLimit)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * reduceScaleSpeed;          
        }

        if (Vector4.Distance(spriteRenderer.color, targetColor) < 0.01f)
        {          
            spriteRenderer.color = targetColor;
        }
        else
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, lerpSpeed * Time.deltaTime);
        }

    }

    IEnumerator MoveUpDown()
    {
        while (true)
        {
            yield return MoveUp();
            yield return MoveDown();
        }     
    }

    IEnumerator MoveUp()
    {

        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y + offsetY);

        while (true)
        {

            float posY = Mathf.Cos(angle) * Time.deltaTime * speedY;
            angle += Time.deltaTime * angleSpeed;

            transform.Translate(0, posY, 0);

            yield return null;
        }    
    }
    IEnumerator MoveDown()
    {

        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y - offsetY);

        while (true)
        {           
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * upDownSpeed);

            yield return null;
        }
    }


    [SerializeField]
    private float reduceScaleSpeed;

    [SerializeField]
    private float lerpSpeed = 1;

    [SerializeField]
    private float scaleLimit = 0.4f;

    [SerializeField]
    private float upDownSpeed = 1;

    [SerializeField]
    private float offsetY = 0.5f;


    private SpriteRenderer spriteRenderer;

    private Color targetColor;


    
    float angle = 0;

    [SerializeField]
    float speedY = 0;

    [SerializeField]
    float angleSpeed = 1;

    [SerializeField]
    float speedX = 0;



}
