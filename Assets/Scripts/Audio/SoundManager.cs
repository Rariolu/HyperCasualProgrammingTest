using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;

    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    private void Awake()
    {
        StaticSoundManager.AddSounds(sounds);
        StaticSoundManager.SetMixer(mixer);
    }
}

[System.Serializable]
public struct Sound
{
    public SOUND name;
    public AudioClip clip;
    public bool loop;
    public SOUND_TYPE type;
    public float volume;
}