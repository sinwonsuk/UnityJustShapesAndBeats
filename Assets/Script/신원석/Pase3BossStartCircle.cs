using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Pase3BossStartCircle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeColorProcess());
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    IEnumerator ChangeColorProcess()
    {
        while (true)
        {
            yield return ChangeColor(colors[0]);
            yield return ChangeColor(colors[1]);
            yield return ChangeColor(colors[2]);
            yield return ChangeColor(colors[3]);
        }
    }


    IEnumerator ChangeColor(Color _color)
    {
        spriteRenderer.color = _color;
        yield return new WaitForSeconds(0.1f);     
    }




    SpriteRenderer spriteRenderer;
    Color color;

    [SerializeField]
    List<Color> colors = new List<Color>();

}
