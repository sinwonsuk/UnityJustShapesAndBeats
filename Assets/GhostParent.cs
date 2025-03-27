using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GhostParent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReduceScaleChild()
    {
        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].ReduceAlpha();
        }
    }

    [SerializeField]
    List<Ghost> ghosts = new List<Ghost>();
}
