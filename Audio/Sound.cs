using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    [SerializeField] public float Volume = 1f;

    [Range(.1f, 3f)]
    [SerializeField] public float Pitch = 1f;

    [field: SerializeField] public bool Loop { get; set; }

    [HideInInspector]
    public AudioSource source;
}
