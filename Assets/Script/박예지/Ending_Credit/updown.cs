using UnityEngine;
using UnityEngine.Scripting;

public class UpDown : MonoBehaviour
{
    public float speed = 50f;
    void Start()
    {
        
        aaa = GetComponent<RectTransform>();
        
    }

    void Update()
    {
        // 위로 이동
        aaa.Translate(Vector3.up * Time.deltaTime * speed);
        
    }
    RectTransform aaa;
    
}
