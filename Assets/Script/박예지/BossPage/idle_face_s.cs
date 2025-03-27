using UnityEngine;

public class idle_face_s : MonoBehaviour
{
    public GameObject eyePrefab;   // 눈 프리팹
    public GameObject mouthPrefab; // 입 프리팹
    public Vector2 eyeOffset = new Vector2(0, 0.5f);  // 눈 위치 조정
    public Vector2 mouthOffset = new Vector2(0, -0.5f); // 입 위치 조정

    private GameObject spawnedEye;   // 생성된 눈 오브젝트 저장
    private GameObject spawnedMouth; // 생성된 입 오브젝트 저장

    void Start()
    {
        SpawnFace();
    }

    void SpawnFace()
    {
        if (eyePrefab != null)
        {
            // 눈 생성
            Vector2 eyePosition = (Vector2)transform.position + eyeOffset;
            spawnedEye = Instantiate(eyePrefab, eyePosition, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Eye Prefab이 설정되지 않았습니다!");
        }

        if (mouthPrefab != null)
        {
            // 입 생성
            Vector2 mouthPosition = (Vector2)transform.position + mouthOffset;
            spawnedMouth = Instantiate(mouthPrefab, mouthPosition, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Mouth Prefab이 설정되지 않았습니다!");
        }
    }
}
