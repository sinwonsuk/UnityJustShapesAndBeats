using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public enum sfx
    {
       none,
    }
    public enum bgm
    {
        Stage1,
        Stage2,
        Stage3,
    }
    private void Awake()
    {
        instance = this;
        Init();
    }

    public void SfxPlay(sfx sfx, bool _loopcheck, float volume = 0.5f)
    {
        for (int i = 0; i < sfxSound.Length; i++)
        {
            if (sfxSound[i].isPlaying)
            {
                continue;
            }

            sfxSound[i].clip = sfxClips[(int)sfx];
            sfxSound[i].Play();
            sfxSound[i].volume = volume;

            if (_loopcheck == true)
            {
                sfxSound[i].loop = true;
            }
            else
            {
                sfxSound[i].loop = false;
            }
            break;
        }

    }
    public void Sfx_Stop(sfx _sfx)
    {
        for (int i = 0; i < sfxSound.Length; i++)
        {
            if (sfxSound[i].clip == sfxClips[(int)_sfx])
            {
                sfxSound[i].Stop();
            }
        }
    }
    public void PlayBgm(bgm _bgm)
    {
        bgmPlayer.clip = bgmClips[(int)_bgm];
        bgmPlayer.Play();
    }
    public void Bgm_Stop()
    {
        bgmPlayer.Stop();
    }
    public void All_Sfx_Stop()
    {
        for (int i = 0; i < sfxSound.Length; i++)
        {
            sfxSound[i].Stop();
        }
    }
    public static SoundManager GetInstance()
    {
        return instance;
    }
    private void Init()
    {
        GameObject bgmObject = new GameObject("BGM");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        GameObject sfxObject = new GameObject("SFX");
        sfxObject.transform.parent = transform;
        sfxSound = new AudioSource[channels];

        for (int i = 0; i < sfxSound.Length; i++)
        {
            sfxSound[i] = sfxObject.AddComponent<AudioSource>();
            sfxSound[i].playOnAwake = false;
            sfxSound[i].volume = sfxVolume;
        }
    }
    public void SetSoundBgm(float volume)
    {
        bgmPlayer.volume = volume;
    }

    public void ReduceSoundBgm()
    {
        if(bgmPlayer.volume <= 0)
        {
            bgmPlayer.volume -= Time.deltaTime * reduceSoundSpeed;
        }     
    }

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;
    public float reduceSoundSpeed;


    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxSound;



    static SoundManager instance;


}
