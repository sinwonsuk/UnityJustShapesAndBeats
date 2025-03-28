using System.Collections;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class SlayerBoss : MonoBehaviour
{
    [SerializeField] private Vector3 originalScale =new Vector3(0.6f, 0.6f, 0.6f);
    [SerializeField] private Vector3 firstScale = new Vector3(0.6f, 0.6f, 0.6f);
    [SerializeField] private Vector3 maxScale = new Vector3(0.7f, 0.7f, 0.7f); 
    [SerializeField] private float growTime = 0.06f;
    [SerializeField] private float shrinkTime = 0.5f;
    [SerializeField] private float deleteTime = 0.1f;

    [SerializeField] private float deleteDelayTime = 0.15f;

    private void OnEnable()
    {
        Color bossColor = new Color(255 / 255f, 32 / 255f, 112 / 255f, 1f);
        ChangeColor(bossColor);

        transform.localScale = Vector3.zero;
        StartCoroutine(AppearSlayer());
    }

    private void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = color;
        }
    }

    private IEnumerator AppearSlayer()
    {
        Color pinkColor = new Color(255 / 255f, 32 / 255f, 112 / 255f, 1f);
        Color whiteColor = Color.white;


        yield return new WaitForSeconds(1.4f);

        float current = 0f;
        ChangeColor(whiteColor);
        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(firstScale, maxScale, current / growTime);
            yield return null;
        }

        ChangeColor(pinkColor);
        current = 0f;
        while (current < shrinkTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, originalScale, current / shrinkTime);
            yield return null;
        }

        transform.localScale = originalScale;

        yield return new WaitForSeconds(deleteDelayTime);

        current = 0f;
        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, maxScale, current / growTime);
            yield return null;
        }

        current = 0f;
        while (current < shrinkTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, Vector3.zero, current / deleteTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
