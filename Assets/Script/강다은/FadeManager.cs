using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{
	private void Awake()
	{
		if (fadeManager == null)
		{
			fadeManager = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject); 
		}
	}


    public void FadeOutAndChangeScene(SceneStage _stage,System.Action onBeforeSceneChange = null, System.Action onAfterSceneChange = null, float duration = 2f)
	{
		if (fadeImage != null)
		{
			Destroy(fadeImage); 
		}

		fadeImage = Instantiate(fadeImagePrefab, Vector3.zero, Quaternion.identity);
		spriteRenderer = fadeImage.GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(0f, 0f, 0f, 0f); 
		spriteRenderer.sortingOrder = 10; 
		StartCoroutine(FadeRoutine(_stage, onBeforeSceneChange, onAfterSceneChange, duration));
	}

	private IEnumerator FadeRoutine(SceneStage _stage, System.Action onBeforeSceneChange, System.Action onAfterSceneChange, float duration)
	{
		float elapsed = 0f;
		SoundManager soundManager = SoundManager.GetInstance();

		// 🔽 Fade Out
		while (elapsed < duration)
		{
			elapsed += Time.unscaledDeltaTime;
			float t = Mathf.Clamp01(elapsed / duration);
			spriteRenderer.color = new Color(0f, 0f, 0f, t);

			soundManager.SetSoundBgm(Mathf.Lerp(initialBgmVolume, 0f, t));
			yield return null;
		}

		soundManager.Bgm_Stop();



		if(onBeforeSceneChange != null)
            onBeforeSceneChange?.Invoke();
		yield return null;

        SceneManager.ChangeScene(_stage);

        // ⏫ Fade In
        elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.unscaledDeltaTime;
			float t = Mathf.Clamp01(elapsed / duration);
			spriteRenderer.color = new Color(0f, 0f, 0f, 1f - t);
			yield return null;
		}
        if (onBeforeSceneChange != null)
            onAfterSceneChange?.Invoke();

        Destroy(fadeImage);
	}

	public static FadeManager fadeManager;

	[SerializeField] private GameObject fadeImagePrefab;
	[SerializeField] private float initialBgmVolume = 0.1f;

	private GameObject fadeImage;
	private SpriteRenderer spriteRenderer;
}
