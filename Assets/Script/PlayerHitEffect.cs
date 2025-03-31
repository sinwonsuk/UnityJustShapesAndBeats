using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitPlayerEffect(Vector2 posXRange, Vector2 posYRange)
    {
        for (int i = 0; i < 30; i++)
        {
            float posX = Random.Range(posXRange.x, posXRange.y);
            float posY = Random.Range(posYRange.x, posYRange.y);
            Instantiate(playerParticle, new Vector2(childPlayerTransform.position.x + posX, childPlayerTransform.position.y + posY), Quaternion.identity);
        }
    }


    [SerializeField]
    GameObject playerParticle;

    [SerializeField]
    Transform childPlayerTransform;

    float time = 0.0f;
}
