using System.Collections;
using UnityEngine;

/// <summary>
/// ��鸱 ������ ������ ������
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
    // ��鸮�� ��(���ļ�)
    public float frequency = 10f;

    /// <summary>
    /// ��鸲 ���� �޼���
    /// </summary>
    /// <param name="direction">��, �Ʒ�, ����, ������ �� ��� �������� ������� ����</param>
    /// <param name="duration">�󸶳� �������� ��鸱�� (�� ����)</param>
    /// <param name="amplitude">��鸮�� ����(����)</param>
    public void StartShake(ShakeDirection direction, float duration, float amplitude)
    {
        // �ڷ�ƾ �ߺ� ������ �����ϰ� �ʹٸ�
        // �̹� ���� ���� �ڷ�ƾ�� �ߴ��ϴ� ���� ������ �߰��� �� ����.
        StartCoroutine(ShakeCoroutine(direction, duration, amplitude));
    }

    /// <summary>
    /// Ư�� �������� duration ���� ����, ���� ���� ��ġ�� �����ϴ� �ڷ�ƾ
    /// </summary>
    private IEnumerator ShakeCoroutine(ShakeDirection direction, float duration, float amplitude)
    {
        Vector3 originalPos = transform.localPosition; // ������Ʈ�� �ʱ� ��ġ(���� ����)

        // ���⿡ ���� ���� ����
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

            // ��鸲 ������ PerlinNoise(Ȥ�� Random)�� ���
            // PerlinNoise(x, y)�� 0~1 ���� ���̹Ƿ� -0.5~0.5 ������ ������ ���� ����.
            // ���⼭�� �����ϰ� 0~1 ���� ������ ���� ���.
            float noise = Mathf.PerlinNoise(Time.time * frequency, 0f);
            float offset = (noise - 0.5f) * 2f * amplitude; // -amplitude ~ +amplitude ������ ��ȯ

            // ��鸱 ����(directVector)�� offset�� ���� ��ġ�� ����
            transform.localPosition = originalPos + directionVector * offset;

            yield return null;
        }

        // ��鸲 ���� �� ���� ��ġ�� ����
        transform.localPosition = originalPos;
    }
}