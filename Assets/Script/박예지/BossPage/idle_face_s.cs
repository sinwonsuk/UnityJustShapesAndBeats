using UnityEngine;

public class idle_face_s : MonoBehaviour
{
    public GameObject eyePrefab;   // �� ������
    public GameObject mouthPrefab; // �� ������
    public Vector2 eyeOffset = new Vector2(0, 0.5f);  // �� ��ġ ����
    public Vector2 mouthOffset = new Vector2(0, -0.5f); // �� ��ġ ����

    private GameObject spawnedEye;   // ������ �� ������Ʈ ����
    private GameObject spawnedMouth; // ������ �� ������Ʈ ����

    void Start()
    {
        SpawnFace();
    }

    void SpawnFace()
    {
        if (eyePrefab != null)
        {
            // �� ����
            Vector2 eyePosition = (Vector2)transform.position + eyeOffset;
            spawnedEye = Instantiate(eyePrefab, eyePosition, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Eye Prefab�� �������� �ʾҽ��ϴ�!");
        }

        if (mouthPrefab != null)
        {
            // �� ����
            Vector2 mouthPosition = (Vector2)transform.position + mouthOffset;
            spawnedMouth = Instantiate(mouthPrefab, mouthPosition, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Mouth Prefab�� �������� �ʾҽ��ϴ�!");
        }
    }
}
