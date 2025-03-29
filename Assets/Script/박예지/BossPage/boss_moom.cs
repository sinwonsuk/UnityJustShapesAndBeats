using UnityEngine;
using System.Collections;

public class ShrinkAndDisappear : MonoBehaviour
{
    public Vector3 initialScale = new Vector3(1, 1, 1); // 처음 크기
    public float shrinkSpeed = 1f; // 줄어드는 속도
    public Color startColor = Color.white; // 처음 색상
    public Color endColor = Color.red; // 끝 색상 (스포이드로 고른 색)
    public float pauseTime = 1f; // 색상 변화 중 멈추는 시간 (초)

    private Renderer objectRenderer;
    private float startTime;
    private float duration;
    private bool isPaused = false;

    private void Start()
    {
        // 처음 크기 설정
        transform.localScale = initialScale;

        // 렌더러 컴포넌트 가져오기
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = startColor;

        // 색상 변화가 끝날 때까지의 시간 설정 (줄어드는 시간)
        duration = initialScale.x / shrinkSpeed; // 크기가 0이 될 때까지 걸리는 시간
        startTime = Time.time; // 시작 시간

        // 색상 변화 중 잠시 멈추기
        StartCoroutine(PauseForColorChange());
    }

    private void Update()
    {
        // 크기를 줄여가며 사라지게 하기
        if (transform.localScale.x > 0)
        {
            if (!isPaused)
            {
                transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
                // 시간에 따라 색상 보간 (Lerp)
                float t = (Time.time - startTime) / duration;
                objectRenderer.material.color = Color.Lerp(startColor, endColor, t);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // 색상 변화 중 잠시 멈추는 코루틴
    private IEnumerator PauseForColorChange()
    {
        yield return new WaitForSeconds(duration / 2); // 색상 변화 중반에 잠시 멈추기
        isPaused = true; // 멈추기 시작
        yield return new WaitForSeconds(pauseTime); // 원하는 시간만큼 멈춤
        isPaused = false; // 다시 움직이기 시작
    }
}
