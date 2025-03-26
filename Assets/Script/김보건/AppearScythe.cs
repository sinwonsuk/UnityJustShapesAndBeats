using System.Collections;
using UnityEngine;

public class AppearScythe : MonoBehaviour
{
    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private Vector3 firstScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float growTime = 0.1f;
    [SerializeField] private float shrinkTime = 0.5f;

    [Header("이동 + 회전")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 360f;

    private Vector2 moveDir;
    private bool canMove = false;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        moveDir = Random.insideUnitCircle.normalized;

        StartCoroutine(AppearSlayer());

    }
    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (canMove)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }


    private IEnumerator AppearSlayer()
    {

        yield return new WaitForSeconds(1.7f);

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

        canMove = true;
    }
}
