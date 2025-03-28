using System.Collections.Generic;
using UnityEngine;


public enum SceneStage
{
    Lobby,
    Play,
    Ending,
}


public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < scences.Count; i++)
        {
            SceneDictinary[(SceneStage)i] = scences[i];
        }

        //prevSceneStage = sceneStage;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            ChangeScene(SceneStage.Lobby);
        }
        if (Input.GetKeyDown("2"))
        {
            ChangeScene(SceneStage.Play);
        }

        UpdateScene();
    }

    public void UpdateScene()
    {
        if(prevSceneStage == sceneStage)
        {
            return;
        }

        if (SceneDictinary.ContainsKey(sceneStage))
        {
            SceneDictinary[prevSceneStage].SetActive(false);
            SceneDictinary[sceneStage].SetActive(true);

            prevSceneStage = sceneStage;
        }
    }
    public static void ChangeScene(SceneStage _sceneStage)
    {      
        sceneStage = _sceneStage;
    }


    private static SceneStage sceneStage = SceneStage.Lobby;
    private SceneStage prevSceneStage = SceneStage.Ending;
    private Dictionary<SceneStage, GameObject> SceneDictinary = new Dictionary<SceneStage, GameObject>();

    static public bool IsLife { get; set; } = true;

    public void Off()
    {

    }

    [SerializeField]
    private List<GameObject> scences;
}
