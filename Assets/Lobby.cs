using UnityEngine;

public class Lobby : MonoBehaviour
{
    private void Start()
    {
        SoundManager.GetInstance().PlayBgm(SoundManager.bgm.Lobby);
        SoundManager.GetInstance().SetSoundBgm(0.1f);
    }
    private void OnEnable()
    {
        if (SoundManager.GetInstance() != null)
        {
            SoundManager.GetInstance().PlayBgm(SoundManager.bgm.Lobby);
            SoundManager.GetInstance().SetSoundBgm(0.1f);
        }
    }
    void Update()
    {
        
    }
}
