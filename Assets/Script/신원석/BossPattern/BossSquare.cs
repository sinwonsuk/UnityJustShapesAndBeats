using System.Collections;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class BossSquare : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        pinkColor = spriteRenderer.color;

        color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        
     
     
    }

    // Update is called once per frame
    void Update()
    {

        

        if (color.a <= 1)
        {
            color.a += Time.deltaTime * speed;
            spriteRenderer.color = color;


            Vector3 scale = transform.localScale;
            scale.x += Time.deltaTime * scaleSpeed;
            transform.localScale = scale;

        }
        else
        {
           if(coroutine == null)
           {
                coroutine = StartCoroutine(ProcessCorutine());
           }
        }
      
       



          
    }

    IEnumerator ProcessCorutine()
    {

        yield return ScaleUp();

        yield return ChangeWhiteColor();

        yield return ChangePinkColor();

        yield break;
    }

    IEnumerator ScaleUp()
    {
        Vector3 scale = transform.localScale;
        scale.x = 10.0f; 
        transform.localScale = scale;
        yield break;
    }

    IEnumerator ChangeWhiteColor()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(colorWaitTime);
    }

    IEnumerator ChangePinkColor()
    {
        spriteRenderer.color = pinkColor;
        yield break;
    }


    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private float scaleSpeed = 0;

    [SerializeField]
    private float colorWaitTime = 0.5f;


    Coroutine coroutine;

    SpriteRenderer spriteRenderer;

    Color color;
    Color pinkColor;

}
