/*      Handling settings script

*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    #region Variable Initialization

    Sounds[] sounds;
    QualityManager qualityManager;
    SceneControls sceneControls;

    [SerializeField] Image sfxOnOffImage;
    [SerializeField] Image musicOnOffImage;
    [SerializeField] Image screenShakeOnOffImage;
    [SerializeField] TextMeshProUGUI qualityText;
    [SerializeField] bool muteSfx;
    [SerializeField] bool muteMusic;
    [SerializeField] bool screenShake;

    bool sfxSettings;
    bool musicSettings;
    bool screenShakeSettings;
    bool qualitySettings;

    #endregion

    void Start()
    {
        sceneControls = FindObjectOfType<SceneControls>();
        qualityManager = QualityManager.instance;
        sounds = AudioManager.instance._Sounds;
        UpdateSettings();
        MuteSettings(Sounds.AudioType.SFX, muteSfx, "sfxToggle");
        MuteSettings(Sounds.AudioType.Music, muteMusic, "musicToggle");
    }

    void Update()
    {
        UpdateSettings();
    }

    void UpdateSettings()
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

        // QUALITY TEXT
        qualitySettings = PlayerPrefs.GetInt(qualityManager.POST_PROCESSING_REFERENCE) == 1 ? true : false;
        if (qualitySettings)
        {
            qualityText.text = "HIGH";
        }
        else
        {
            qualityText.text = "LOW";
        }

    }

    public void ScreenShakeControl()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        screenShake = !screenShake;
        screenShakeOnOffImage.enabled = screenShake;
        PlayerPrefs.SetInt("screenShake", screenShake ? 1: 0);
    }

    public void QualityControl()
    {
        // Used to toggle quality settings
        qualityManager.QualityControl();
        PlayerPrefs.SetInt(qualityManager.POST_PROCESSING_REFERENCE, QualityManager.instance.PostProcessVolume.enabled ? 1 : 0);
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
