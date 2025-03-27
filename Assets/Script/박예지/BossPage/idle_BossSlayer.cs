using System.Collections;
using UnityEngine;

public class idle_BossSlayer : MonoBehaviour
{
    [SerializeField] private Vector3 originalScale = Vector3.one;  // 보스 원래 크기
    [SerializeField] private Vector3 firstScale = new Vector3(1.1f, 1.1f, 1.1f);  // 첫번째 크기
    [SerializeField] private Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);  // 최대 크기
    [SerializeField] private float growTime = 0.1f;  // 크기 증가 시간
    [SerializeField] private float shrinkTime = 0.5f;  // 크기 감소 시간
    [SerializeField] private float deleteTime = 0.1f;  // 삭제 시간
    public GameObject Bbullet;  // 총알 프리팹
    [SerializeField] private float deleteDelayTime = 0.8f;  // 삭제 대기 시간
    [SerializeField] private float appearDelayTime = 19f;  // 보스가 나타나는 시간 (초)

    private void OnEnable()
    {
        // 보스를 처음에는 보이지 않게 설정
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        // 19초 대기 후 보스가 나타나는 코루틴 시작
        StartCoroutine(AppearSlayer());
    }

    private IEnumerator AppearSlayer()
    {
        // 설정한 시간만큼 대기 후 보스가 나타남
        yield return new WaitForSeconds(appearDelayTime);

        // 보스가 나타나면서 크기를 키우기 시작
        float current = 0f;
        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, firstScale, current / growTime);
            yield return null;
        }

        // 보스가 출현하자마자 총알을 9번 발사하기 시작
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 30 * j);  // 각도에 맞게 회전
                Instantiate(Bbullet, transform.position, rotation);  // 총알 발사
            }

            yield return new WaitForSeconds(2f);  // 2초 간격으로 다음 발사
        }

        // 보스 크기 키우기 애니메이션
        current = 0f;
        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(firstScale, maxScale, current / growTime);
            yield return null;
        }

        // 보스 크기 줄이기 애니메이션
        current = 0f;
        while (current < shrinkTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, originalScale, current / shrinkTime);
            yield return null;
        }

        transform.localScale = originalScale;  // 최종 크기 설정

        // 일정 시간 대기 후 삭제
        yield return new WaitForSeconds(deleteDelayTime);

        // 보스가 다시 커지면서 사라짐
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

        Destroy(gameObject);  // 보스 삭제
    }
}
