using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //SINGLETON
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        CheckInstance();

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();


            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.isLooping;

        }
    }

    public void PlaySound(string soundName, float volume = 1, string mode = "global", int num = 0)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == soundName);

        switch (num)
        {
            case 0:
                sound.audioSource.clip = sound.clips[UnityEngine.Random.Range(0, sound.clips.Length)];
                break;
            default:
                sound.audioSource.clip = sound.clips[num];
                break;
        }

        switch (mode)
        {
            case "global":
                sound.audioSource.Play();
                break;
            case "child":
                sound.audioSource.Play();

                break;
        }

    }

    private void CheckInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip[] clips;
    public bool isLooping;

    [Range(0f, 5f)]
    public float volume;
    [Range(.1f, 3f)]

    public float pitch;
    //[HideInInspector]
    public AudioSource audioSource;
}