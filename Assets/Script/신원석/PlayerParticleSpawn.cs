using System.Collections;
using UnityEngine;

public class PlayerParticleSpawn : MonoBehaviour
{

    void Start()
    {
       
    }

    void Update()
    {
        CreatePlayerParticle();
    }

    void CreatePlayerParticle()
    {
        time += Time.deltaTime;

        if (time > 0.02f)
        {
            float posX = Random.Range(-0.1f, 0.1f);
            float posY = Random.Range(-0.2f, 0.2f);
            Instantiate(playerParticle, new Vector2(childPlayerTransform.position.x + posX, childPlayerTransform.position.y + posY), Quaternion.identity);
            time = 0;
        }
    }

    [SerializeField]
    GameObject playerParticle;

    [SerializeField]
    Transform childPlayerTransform;

    float time = 0.0f;
}
