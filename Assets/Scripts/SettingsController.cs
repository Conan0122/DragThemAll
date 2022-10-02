using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    #region Variable Initialization

    Sounds[] sounds;
    SceneControls sceneControls;

    [SerializeField] Image sfxOnOffImage;
    [SerializeField] Image musicOnOffImage;
    [SerializeField] Image screenShakeOnOffImage;
    [SerializeField] bool muteSfx;
    [SerializeField] bool muteMusic;
    [SerializeField] bool screenShake;

    bool sfxSettings;
    bool musicSettings;
    bool screenShakeSettings;

    #endregion

    void Start()
    {
        sceneControls = FindObjectOfType<SceneControls>();
        sounds = AudioManager.instance._Sounds;
        UpdateSettings();
        MuteSettings(Sounds.AudioType.SFX, muteSfx, "sfxToggle");
        MuteSettings(Sounds.AudioType.Music, muteMusic, "musicToggle");
    }

    void Update()
    {
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        //  SFX
        sfxSettings = PlayerPrefs.GetInt("sfxToggle") == 1 ? true : false;
        sfxOnOffImage.enabled = sfxSettings;
        muteSfx = sfxSettings;

        //MUSIC
        musicSettings = PlayerPrefs.GetInt("musicToggle") == 1 ? true : false;
        musicOnOffImage.enabled = musicSettings;
        muteMusic = musicSettings;

        // Screen Shake
        screenShakeSettings = PlayerPrefs.GetInt("screenShake", 1) == 1 ? true : false;
        screenShakeOnOffImage.enabled = screenShakeSettings;
        screenShake = screenShakeSettings;
    }

    public void ScreenShakeControl()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        screenShake = !screenShake;
        screenShakeOnOffImage.enabled = screenShake;
        PlayerPrefs.SetInt("screenShake", screenShake ? 1: 0);
    }

    public void SFXControl()
    {
        muteSfx = !muteSfx;
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        MuteSettings(Sounds.AudioType.SFX, muteSfx, "sfxToggle");
    }

    public void MusicControl()
    {
        muteMusic = !muteMusic;
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        MuteSettings(Sounds.AudioType.Music, muteMusic, "musicToggle");
    }

    public void ShowGameProgressPopUp()
    {
        sceneControls.GameProgresssPopUp();
    }

    void MuteSettings(Sounds.AudioType audioType, bool toMute, string saveToggle)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipType == audioType)
            {
                if (toMute)
                {
                    sounds[i].Source.mute = true;
                }
                else if (!toMute)
                {
                    sounds[i].Source.mute = false;
                }
                PlayerPrefs.SetInt(saveToggle, sounds[i].Source.mute ? 1 : 0);
            }
        }
    }

}
