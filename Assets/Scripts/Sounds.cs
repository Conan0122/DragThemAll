using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public enum AudioType
    {
        Music, SFX
    }
    public enum AudioName
    {
        HomeBGM, LevelBGM, DefenderDeath, DefenderSpawn, DestroyerAbsorb, AttackerDeath, AttackerDrag, 
        EmptyDefenderSlot, BigButtonClicks, NormalButtonClicks, LevelComplete
    }

    [SerializeField] AudioType clipType;
    [SerializeField] AudioName clipName;
    [SerializeField] AudioClip clip;
    [Range(0, 1)] [SerializeField] float volume = 1;
    [SerializeField] bool loop;
    [SerializeField] bool playOnAwake;
    // [SerializeField] bool audioToggle = true;

    AudioSource source;

    #region Getters and setters

    public AudioType ClipType { get { return clipType; } set { clipType = value; } }
    public AudioName ClipName { get { return clipName; } set { clipName = value; } }
    public AudioClip Clip { get { return clip; } set { clip = value; } }
    public float Volume { get { return volume; } set { volume = value; } }
    public AudioSource Source { get { return source; } set { source = value; } }
    public bool Loop { get { return loop; } set{ loop = value; } }
    public bool PlayOnAwake { get { return playOnAwake; } set{ playOnAwake = value; } }
    // public bool AudioToggle { get { return audioToggle; } set{ audioToggle = value; } }

    #endregion


}
