using System.Collections;
using UnityEngine;

/// <summary>
/// 흔들릴 방향을 정의한 열거형
/// </summary>
public enum ShakeDirection
{
    Up,
    Down,
    Left,
    Right
}

public class DirectionalShake : MonoBehaviour
{
    // 흔들리는 빈도(주파수)
    public float frequency = 10f;

    /// <summary>
    /// 흔들림 시작 메서드
    /// </summary>
    /// <param name="direction">위, 아래, 왼쪽, 오른쪽 중 어느 방향으로 흔들지를 지정</param>
    /// <param name="duration">얼마나 오랫동안 흔들릴지 (초 단위)</param>
    /// <param name="amplitude">흔들리는 세기(진폭)</param>
    public void StartShake(ShakeDirection direction, float duration, float amplitude)
    {
        // 코루틴 중복 실행을 방지하고 싶다면
        // 이미 실행 중인 코루틴을 중단하는 등의 로직을 추가할 수 있음.
        StartCoroutine(ShakeCoroutine(direction, duration, amplitude));
    }

    /// <summary>
    /// 특정 방향으로 duration 동안 흔들고, 이후 원래 위치로 복귀하는 코루틴
    /// </summary>
    private IEnumerator ShakeCoroutine(ShakeDirection direction, float duration, float amplitude)
    {
        Vector3 originalPos = transform.localPosition; // 오브젝트의 초기 위치(로컬 기준)

        // 방향에 따른 벡터 정의
        Vector3 directionVector = Vector3.zero;
        switch (direction)
        {
            case ShakeDirection.Up:
                directionVector = Vector3.up;
                break;
            case ShakeDirection.Down:
                directionVector = Vector3.down;
                break;
            case ShakeDirection.Left:
                directionVector = Vector3.left;
                break;
            case ShakeDirection.Right:
                directionVector = Vector3.right;
                break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // 흔들림 정도를 PerlinNoise(혹은 Random)로 계산
            // PerlinNoise(x, y)는 0~1 사이 값이므로 -0.5~0.5 범위로 보정할 수도 있음.
            // 여기서는 간단하게 0~1 값을 진폭에 곱해 사용.
            float noise = Mathf.PerlinNoise(Time.time * frequency, 0f);
            float offset = (noise - 0.5f) * 2f * amplitude; // -amplitude ~ +amplitude 범위로 변환

            // 흔들릴 방향(directVector)에 offset을 곱해 위치를 보정
            transform.localPosition = originalPos + directionVector * offset;

            yield return null;
        }

        // 흔들림 종료 후 원래 위치로 복귀
        transform.localPosition = originalPos;
    }
}