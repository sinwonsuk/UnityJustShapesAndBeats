using UnityEngine;

public class BossChainCircle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one * Time.deltaTime * scaleSpeed;
    }

    [SerializeField]
    private float scaleSpeed = 5.0f;
}
