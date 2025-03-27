using System.Collections;
using UnityEngine;

public class idle_BossSlayer : MonoBehaviour
{
    [SerializeField] private Vector3 originalScale = Vector3.one;  // ���� ���� ũ��
    [SerializeField] private Vector3 firstScale = new Vector3(1.1f, 1.1f, 1.1f);  // ù��° ũ��
    [SerializeField] private Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);  // �ִ� ũ��
    [SerializeField] private float growTime = 0.1f;  // ũ�� ���� �ð�
    [SerializeField] private float shrinkTime = 0.5f;  // ũ�� ���� �ð�
    [SerializeField] private float deleteTime = 0.1f;  // ���� �ð�
    public GameObject Bbullet;  // �Ѿ� ������
    [SerializeField] private float deleteDelayTime = 0.8f;  // ���� ��� �ð�
    [SerializeField] private float appearDelayTime = 19f;  // ������ ��Ÿ���� �ð� (��)

    private void OnEnable()
    {
        // ������ ó������ ������ �ʰ� ����
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        // 19�� ��� �� ������ ��Ÿ���� �ڷ�ƾ ����
        StartCoroutine(AppearSlayer());
    }

    private IEnumerator AppearSlayer()
    {
        // ������ �ð���ŭ ��� �� ������ ��Ÿ��
        yield return new WaitForSeconds(appearDelayTime);

        // ������ ��Ÿ���鼭 ũ�⸦ Ű��� ����
        float current = 0f;
        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, firstScale, current / growTime);
            yield return null;
        }

        // ������ �������ڸ��� �Ѿ��� 9�� �߻��ϱ� ����
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 30 * j);  // ������ �°� ȸ��
                Instantiate(Bbullet, transform.position, rotation);  // �Ѿ� �߻�
            }

            yield return new WaitForSeconds(2f);  // 2�� �������� ���� �߻�
        }

        // ���� ũ�� Ű��� �ִϸ��̼�
        current = 0f;
        while (current < growTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(firstScale, maxScale, current / growTime);
            yield return null;
        }

        // ���� ũ�� ���̱� �ִϸ��̼�
        current = 0f;
        while (current < shrinkTime)
        {
            current += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, originalScale, current / shrinkTime);
            yield return null;
        }

        transform.localScale = originalScale;  // ���� ũ�� ����

        // ���� �ð� ��� �� ����
        yield return new WaitForSeconds(deleteDelayTime);

        // ������ �ٽ� Ŀ���鼭 �����
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

        Destroy(gameObject);  // ���� ����
    }
}
