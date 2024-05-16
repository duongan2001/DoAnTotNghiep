using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    public Sound[] sounds;

    private void Awake()
    {
        if (soundManager && soundManager != this)
            Debug.LogError("Loi nhieu hon 1 Sound Manager");
        else
            soundManager = this;

        foreach (var item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();

            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
        }
    }

    private void Start()
    {
        //GetVolumn();
        //GetOnOffMusicState();
        //GetOnOffSFXState();
        if (PlayerPrefs.GetInt("musicState")==1)
        {
            PlayBGM(0);
        }
        SetOnOffSFXState();
        GetOnOffSFXState();
    }

    public void PlayBGM(int s)
    {
        sounds[s].source.Play();
    }    

    public void PlaySFX(int s)
    {
        sounds[s].source.PlayOneShot(sounds[s].source.clip);
    }    
    
    /*public void SetVolumnMusic(float musicVolume)
    {
        sounds[0].source.volume = musicVolume/100;
        PlayerPrefs.SetFloat("musicVolumn", sounds[0].source.volume);
    }*/
    
    /*
    public void SetVolumnSFX(float sfxVolume)
    {
        for (int i = 1; i < sounds.Length; i++)
        {
            sounds[i].source.volume = sfxVolume/100;
        }
        PlayerPrefs.SetFloat("sfxVolumn", sounds[1].source.volume);
    }*/

    /*public void GetVolumn()
    {
        if (PlayerPrefs.HasKey("musicVolumn"))
        {
            sounds[0].source.volume = PlayerPrefs.GetFloat("musicVolumn");
        }
        else
        {
            sounds[0].source.volume = 1;
        }
        Debug.Log(sounds[0].source.volume);

        if (PlayerPrefs.HasKey("musicVolumn"))
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                sounds[i].source.volume = PlayerPrefs.GetFloat("sfXVolumn");
            }
        }
        else
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                sounds[i].source.volume = 1;
            }
        }
    }*/  
    
    public void GetOnOffMusicState()
    {
        if (PlayerPrefs.GetInt("musicState") == 1)//On
        {
            sounds[0].source.volume = sounds[0].volume;
        }
        else
        {
            sounds[0].source.volume = 0;
        }    
    }

    public void GetOnOffSFXState()
    {
        if (PlayerPrefs.GetInt("sfxState") == 1)//On
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                sounds[i].source.volume = sounds[i].volume;
            }
        }
        else
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                sounds[i].source.volume = 0;
            }
        }
    }

    public void SetOnOffMusicState()
    {
        if(PlayerPrefs.HasKey("musicState"))
        {
            if(PlayerPrefs.GetInt("musicState") == 1)
            {
                //sounds[0].source = gameObject.AddComponent<AudioSource>();

                sounds[0].source.clip = sounds[0].clip;
                sounds[0].source.volume = sounds[0].volume;
                sounds[0].source.pitch = sounds[0].pitch;
                sounds[0].source.loop = sounds[0].loop;
            }
            else
            {
                //sounds[0].source = gameObject.AddComponent<AudioSource>();

                sounds[0].source.clip = sounds[0].clip;
                sounds[0].source.volume = 0;
                sounds[0].source.pitch = sounds[0].pitch;
                sounds[0].source.loop = sounds[0].loop;
            }
        }
        else
        {
            //sounds[0].source = gameObject.AddComponent<AudioSource>();

            sounds[0].source.clip = sounds[0].clip;
            sounds[0].source.volume = sounds[0].volume;
            sounds[0].source.pitch = sounds[0].pitch;
            sounds[0].source.loop = sounds[0].loop;
        }
    }

    public void SetOnOffSFXState()
    {
        if (PlayerPrefs.HasKey("sfxState"))
        {
            if (PlayerPrefs.GetInt("sfxState") == 1)
            {
                for (int i = 1; i < sounds.Length; i++)
                {
                    //sounds[i].source = gameObject.AddComponent<AudioSource>();

                    sounds[i].source.clip = sounds[i].clip;
                    sounds[i].source.volume = sounds[i].volume;
                    sounds[i].source.pitch = sounds[i].pitch;
                    sounds[i].source.loop = sounds[i].loop;
                }              
            }
            else
            {
                for (int i = 1; i < sounds.Length; i++)
                {
                    //sounds[i].source = gameObject.AddComponent<AudioSource>();

                    sounds[i].source.clip = sounds[i].clip;
                    sounds[i].source.volume = 0;
                    sounds[i].source.pitch = sounds[i].pitch;
                    sounds[i].source.loop = sounds[i].loop;
                }
            }
        }
        else
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                //sounds[i].source = gameObject.AddComponent<AudioSource>();

                sounds[i].source.clip = sounds[i].clip;
                sounds[i].source.volume = sounds[i].volume;
                sounds[i].source.pitch = sounds[i].pitch;
                sounds[i].source.loop = sounds[i].loop;
            }
        }
    }
}
