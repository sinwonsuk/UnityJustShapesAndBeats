using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
	Stage1,
	Stage2,
	TestStage,
}




public class GameController : MonoBehaviour
{
	private void Start()
	{
		for ( int i = 0; i < stagePrefab.Length; ++ i )
		{
			// 에이전트 생성, 초기화 메소드 호출
			GameObject	clone	= Instantiate(stagePrefab[i]);

            BaseGameEntity entity = clone.GetComponent<BaseGameEntity>();
						
			if((Stage)i == stage)
			{
                clone.SetActive(true);
                entity.Setup();               
                prevstage = stage;
            }
            else
            {
                clone.SetActive(false);
            }

			stageEntities[(Stage)i] = entity;        
		}   
	
        SoundManager.GetInstance().PlayBgm(SoundManager.bgm.Stage1);
    }

    

    private void Update()
	{
		if ( IsGameStop == true ) return;

        ChangeStage();
        UpdateStage();
    }

    void ChangeStage()
    {
        if (prevstage != stage)
        {
            stageEntities[stage].gameObject.SetActive(true);
            stageEntities[stage].Setup();
            prevstage = stage;
        }
    }
    void UpdateStage()
    {
        foreach (var entity in stageEntities.Values)
        {
            entity.gameObject.SetActive(false);
        }

        if (stageEntities.ContainsKey(stage))
        {

            stageEntities[stage].Updated();
        }
    }
    public static void Stop(BaseGameEntity entity)
	{
		IsGameStop = true;		
	}
  
    [SerializeField]
    private GameObject[] stagePrefab; // Student 타입의 프리팹

    // Stage 모음 프리팹
    private Dictionary<Stage, BaseGameEntity> stageEntities = new Dictionary<Stage, BaseGameEntity>();

    [SerializeField]
    private Stage stage = Stage.Stage1;

    Stage prevstage = Stage.Stage1;

    public static bool IsGameStop { set; get; } = false;
}

