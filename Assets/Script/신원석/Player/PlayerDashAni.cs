using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlayerDashAni : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        color.a -= Time.deltaTime * reduceAlphaSpeed;

        spriteRenderer.color = color;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetAlpha(float Alpha)
    {
        color.a = Alpha/255.0f;
        spriteRenderer.color = color;
    }

    private float reduceAlphaSpeed = 5;

}
