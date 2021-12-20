using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    AudioSource audioSource;
    AudioSource AudioSource
    {
        get
        {
            if (audioSource == null)
            {
                return audioSource = GetComponent<AudioSource>();
            }
            return audioSource;
        }
    }

    public void PlaySound(Sound sound, AudioMixer mixer)
    {
        StartCoroutine(PlaySoundAsync(sound, mixer));
    }

    IEnumerator PlaySoundAsync(Sound sound, AudioMixer mixer)
    {
        AudioSource.clip = sound.clip;
        AudioSource.loop = sound.loop;
        AudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups(sound.type.ToString())[0];
        AudioSource.volume = sound.volume;
        AudioSource.Play();
        yield return new WaitForSeconds(sound.clip.length);
        if (!sound.loop)
        {
            Destroy(gameObject);
        }
    }
}