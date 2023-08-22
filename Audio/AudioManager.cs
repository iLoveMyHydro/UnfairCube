using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Der komplette Sound wurde mit folgenden Quellen erstellt (Code Basis und Unity Editor): 
    //https://youtu.be/6OT43pvUyfY
    //https://youtu.be/MjH5rsmYmQY

    #region Parameters

    #region Const

    private const string ThemeAudioText = "Theme";

    #endregion

    #region Sound

    public Sound[] sounds;

    public static AudioManager instance;

    #endregion

    #endregion


    #region Awake

    /// <summary>
    /// Gets the Audio Source Component and the correct values from the array
    /// </summary>
    void Awake()
    {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;

            s.source.loop = s.Loop;
        }
    }

    #endregion

    #region Start

    /// <summary>
    /// Plays the Theme
    /// </summary>
    private void Start()
    {
        Play(ThemeAudioText);
    }

    #endregion

    #region Play

    /// <summary>
    /// Plays the music if the name is correctly in the Editor
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    #endregion
}
