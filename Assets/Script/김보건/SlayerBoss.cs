using System.Collections;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class SlayerBoss : MonoBehaviour
{
    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private Vector3 firstScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f); 
    [SerializeField] private float growTime = 0.1f;
    [SerializeField] private float shrinkTime = 0.5f;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(AppearSlayer());

    }

 
    private IEnumerator AppearSlayer()
    {

        yield return new WaitForSeconds(1.5f);

        float current = 0f;

        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(firstScale, maxScale, current / growTime);
            yield return null;
        }

        current = 0f;
        while (current < shrinkTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, originalScale, current / shrinkTime);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
