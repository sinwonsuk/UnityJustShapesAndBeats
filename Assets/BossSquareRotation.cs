using System.Collections.Generic;
using UnityEngine;

public class BossSquareRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed*Time.deltaTime);  
    }
    public void ReduceScaleChild()
    {
        for (int i = 0; i < bossSquares.Count; i++)
        {
            bossSquares[i].ReduceAlpha();
        }
    }

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    List<BossSquare> bossSquares = new List<BossSquare>();

}
