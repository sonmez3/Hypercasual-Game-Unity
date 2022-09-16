using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.isEffect) s.source.volume = s.volume * PlayerPrefs.GetFloat("SoundEffectVolumeLevel"); //PlayerPrefsten cagir
            //else s.source.volume = s.volume * PlayerPrefs.GetFloat("MusicVolumeLevel");
            s.source.pitch = s.pitch;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
    }

    public AudioSource FindAudioSource(string name)
    {
        return Array.Find(sounds, sound => sound.name == name).source.GetComponent<AudioSource>();
    }

    public void PlayInLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        if (s != null)
            s.source.Play();
    }

    public void StopPlayingInLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = false;
        s.source.Stop();
    }

    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void ChangeEffectVolume(float effectVolume) // volume degisince kullan
    {
        foreach (Sound s in sounds)
        {
            if (s.isEffect) s.source.volume = s.volume * effectVolume;
        }
    }

    public void ChangeMusicVolume(float musicVolume)
    {
        foreach (Sound s in sounds)
        {
            if (!s.isEffect)
            {
                s.source.volume = s.volume * musicVolume;
            }
        }
    }

    public void ButtonClick()
    {
        Play("Button");
    }
}
