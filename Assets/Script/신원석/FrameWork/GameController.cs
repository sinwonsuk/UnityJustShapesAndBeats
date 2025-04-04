using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
	Stage1,
	Stage2,
	BossStage,
}

public interface PatternChoiceInterface
{
    void SetPattern(int value);
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
            stageEntities[prevstage].gameObject.SetActive(false);
            stageEntities[stage].gameObject.SetActive(true);
            stageEntities[stage].Setup();
            prevstage = stage;
        }
    }
    void UpdateStage()
    {      
        if (stageEntities.ContainsKey(stage))
        {
            stageEntities[stage].Updated();
        }
    }
    public static void Stop(BaseGameEntity entity)
	{
		IsGameStop = true;		
	}

    static public void ChangeStage(Stage _stage)
    {
        stage = _stage;
    }
    static public void PluseStage()
    {
        stage++;
    }

    [SerializeField]
    private GameObject[] stagePrefab; // Student 타입의 프리팹

    // Stage 모음 프리팹
    private Dictionary<Stage, BaseGameEntity> stageEntities = new Dictionary<Stage, BaseGameEntity>();

    [SerializeField]
    static private Stage stage = Stage.Stage1;

    Stage prevstage = Stage.Stage1;

    public static bool IsGameStop { set; get; } = false;
}

