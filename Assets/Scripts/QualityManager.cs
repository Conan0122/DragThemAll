/*      Handling Post-Processing
        Setting up Quality settings

*/
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class QualityManager : MonoBehaviour
{
    public static QualityManager instance;

    #region Variable Initialization

    [SerializeField] PostProcessVolume postProcessVolume;

    bool qualitySettings;

    public const string POST_PROCESSING_TOGGLE = "postProcessingToggle";

    #endregion

    #region Getters and Setters
    public PostProcessVolume PostProcessVolume
    {
        get { return postProcessVolume; }
        set { postProcessVolume = value; }
    }
    public string POST_PROCESSING_REFERENCE
    {
        get { return POST_PROCESSING_TOGGLE; }
    }
    #endregion

    void Awake()
    {
        // Singleton: To keep maintaining post-processing effects run smoothly on scenes.
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // QUALITY
        qualitySettings = PlayerPrefs.GetInt(POST_PROCESSING_TOGGLE) == 1 ? true : false;
        PostProcessVolume.enabled = qualitySettings;
    }

    public void QualityControl()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        postProcessVolume.enabled = !postProcessVolume.enabled;
    }

}
