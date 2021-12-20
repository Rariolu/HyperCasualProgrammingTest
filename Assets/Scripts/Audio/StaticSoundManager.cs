using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public static class StaticSoundManager
{
    static AudioMixer mainMixer;
    static Dictionary<SOUND, Sound> sounds = new Dictionary<SOUND, Sound>();
    public static void AddSound(Sound sound)
    {
        if (sounds.ContainsKey(sound.name))
        {
            Debug.LogWarningFormat("Added sound with key \"{0}\" again.", sound.name);
            sounds[sound.name] = sound;
        }
        else
        {
            sounds.Add(sound.name, sound);
        }
    }
    public static void AddSounds(Sound[] sounds)
    {
        foreach(Sound sound in sounds)
        {
            AddSound(sound);
        }
    }
    public static void PlaySound(SOUND name)
    {
        Sound sound;
        if (SoundExists(name, out sound))
        {
            GameObject soundPlayer = new GameObject();
            soundPlayer.name = string.Format("{0}_SoundPlayer", name);
            soundPlayer.AddComponent<SoundPlayer>().PlaySound(sound, mainMixer);
        }
    }
    public static bool SoundExists(SOUND name, out Sound sound)
    {
        if (sounds.ContainsKey(name))
        {
            sound = sounds[name];
            return true;
        }
        sound = new Sound();
        return false;
    }

    public static void SetMixer(AudioMixer mixer)
    {
        mainMixer = mixer;
    }
}