using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FadeBar : MonoBehaviour
{
    [SerializeField] private GameObject mapObj;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Vector3 startPos; // 시작 위치

    void Start()
    {
        if (mapObj != null)
        {
            mapObj.transform.position = startPos;
        }
        StartCoroutine(FadeInMap(mapObj, 0f, fadeDuration, 0.2f));
    }

    void Update()
    {
        
    }

    private IEnumerator FadeInMap(GameObject obj, float startTime, float fadeDuration, float targetAlpha)
    {
        SpriteRenderer fadeObj = obj.GetComponent<SpriteRenderer>();
        if (fadeObj == null)
        {
            Debug.LogError($"{obj.name}에 SpriteRenderer가 없습니다!");
            yield break;
        }

        Color color = fadeObj.color;
        color.a = 0f;
        fadeObj.color = color;

        yield return new WaitForSeconds(startTime);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsed / fadeDuration);
            fadeObj.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeObj.color = new Color(color.r, color.g, color.b, targetAlpha);

        obj.SetActive(false);
    }
}
