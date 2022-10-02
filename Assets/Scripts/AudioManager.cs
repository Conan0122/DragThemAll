using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region Variable Initialization

    [SerializeField] Sounds[] sounds;

    public static AudioManager instance;

    bool sfxSettings;
    bool musicSettings;

    #endregion

    #region Getters and Setters
    public Sounds[] _Sounds
    {
        get { return sounds; }
        set { sounds = value; }
    }
    #endregion

    void Awake()
    {
        // Singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Source = gameObject.AddComponent<AudioSource>();
            sounds[i].Source.clip = sounds[i].Clip;
            sounds[i].Source.volume = sounds[i].Volume;
            sounds[i].Source.loop = sounds[i].Loop;
            sounds[i].Source.playOnAwake = sounds[i].PlayOnAwake;
        }
    }

    void Start()
    {
        PlayAudio(Sounds.AudioName.HomeBGM, false);

        /*
            We need to check audio at start because 
            we need to assure that the saved audio settings is applied
            in the begining of game.
        */

        // Check if audio is muted or not
        sfxSettings = PlayerPrefs.GetInt("sfxToggle") == 1 ? true : false;
        musicSettings = PlayerPrefs.GetInt("musicToggle") == 1 ? true : false;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipType == Sounds.AudioType.SFX)
            {
                sounds[i].Source.mute = sfxSettings;
            }

            if (sounds[i].ClipType == Sounds.AudioType.Music)
            {
                sounds[i].Source.mute = musicSettings;
            }
        }
    }

    public void PlayAudio(Sounds.AudioName soundName, bool playOneShot)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipName == soundName)
            {
                if (!playOneShot)
                {
                    if (true)
                    {
                        sounds[i].Source.Play();
                    }

                }
                else
                    sounds[i].Source.PlayOneShot(sounds[i].Clip);
            }
        }
    }

}