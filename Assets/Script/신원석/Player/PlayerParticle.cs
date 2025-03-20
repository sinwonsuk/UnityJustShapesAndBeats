using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlayerParticle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        color = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (color.a <= 0.05)
        {
            Destroy(gameObject);
        }

        DownRenderAlpha();
    }

    void DownRenderAlpha()
    {
        color.a -= Time.deltaTime * speed;
        SpriteRenderer.color = color;
    }


    [SerializeField]
    float speed = 2.0f;

    Color color;
    SpriteRenderer SpriteRenderer;


}
