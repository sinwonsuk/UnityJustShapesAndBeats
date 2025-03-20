using System.Collections.Generic;
using UnityEngine;

public enum Locations { SweetHome = 0, Library, LectureRoom, PCRoom, Pub };

public class GameController : MonoBehaviour
{
	private void Awake()
	{
		entitys = new List<BaseGameEntity>();

		for ( int i = 0; i < arrayStage.Length; ++ i )
		{
			// ������Ʈ ����, �ʱ�ȭ �޼ҵ� ȣ��
			GameObject	clone	= Instantiate(stagePrefab);
			OneStage    entity	= clone.GetComponent<OneStage>();
			entity.Setup(arrayStage[i]);
			// ������Ʈ���� ��� ��� ���� ����Ʈ�� ����
			entitys.Add(entity);
		}    
    }

    private void Start()
    {
		SoundManager.GetInstance().PlayBgm(SoundManager.bgm.Stage1);

    }

    private void Update()
	{
		if ( IsGameStop == true ) return;

		// ��� ������Ʈ�� Updated()�� ȣ���� ������Ʈ ����
		for ( int i = 0; i < entitys.Count; ++ i )
		{
			entitys[i].Updated();
		}
	}

	public static void Stop(BaseGameEntity entity)
	{
		IsGameStop = true;		
	}

    [SerializeField]
    private string[] arrayStage;    // Student���� �̸� �迭
    [SerializeField]
    private GameObject stagePrefab; // Student Ÿ���� ������

    [SerializeField]
    private string[] arrayUnemployeds;  // Unemployed���� �̸� �迭
    [SerializeField]
    private GameObject unemployedPrefab;    // Unemployed Ÿ���� ������

    // ��� ��� ���� ��� ������Ʈ ����Ʈ
    private List<BaseGameEntity> entitys;

    public static bool IsGameStop { set; get; } = false;
}

