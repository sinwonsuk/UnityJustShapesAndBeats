using System.Collections;
using UnityEngine;

public class UglyCircleRotate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        pinkColor = spriteRenderer.color;
    }


    void Update()
    {
       
        if (transform.position.x <= 0.8f)
        {
            rotateZ += angleSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotateZ);
            transform.localScale += Vector3.one * Time.deltaTime * scaleSpeed;

            if (coroutine == null)
            {
                coroutine = StartCoroutine(ChangeColor());
            }
        }
        if (transform.localScale.x > 16.0f)
        {
            gameObject.SetActive(false);
            spawnPinkBullet.SetActive(true);
            bulletRotate.SetActive(true);

            

        }
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
          
            ColorChangeTime *= 0.65f;

            spriteRenderer.color = whiteColor;

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = pinkColor;
       
            yield return new WaitForSeconds(ColorChangeTime);
        }


    }


    Coroutine coroutine;


    [SerializeField]
    GameObject spawnPinkBullet;    
    [SerializeField]
    GameObject bulletRotate;
    [SerializeField]
    float scaleSpeed = 10;
    [SerializeField]
    float angleSpeed = 10;
    [SerializeField]
    float rotateZ = 0;

    [SerializeField]
    float ColorChangeTime = 5;

    Color whiteColor = Color.white;

    Color pinkColor = Color.white;
    SpriteRenderer spriteRenderer;


}
