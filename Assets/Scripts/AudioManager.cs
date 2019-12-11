using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<string, AudioSource> sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        sounds = new Dictionary<string, AudioSource>();
        foreach (AudioSource s in GetComponentsInChildren<AudioSource>())
        {
            sounds.Add(s.gameObject.name, s);
        }
        instance = this;
    }

    public void Play(string name)
    {
        CheckIfExists(name);
        sounds[name].Play();
    }

    public void PlayOneShot(string name)
    {
        CheckIfExists(name);
        sounds[name].PlayOneShot(sounds[name].clip);
    }

    public void Stop(string name)
    {
        CheckIfExists(name);
        sounds[name].Stop();
    }

    public void StopAll()
    {
        foreach (AudioSource s in sounds.Values)
        {
            s.Stop();
        }
    }

    private void CheckIfExists(string name)
    {
        if (!sounds.ContainsKey(name))
        {
            Debug.LogWarning(string.Format("Audio Manager '{0}' does not contain the audio source '{1}'!", gameObject.name, name));
            return;
        }
    }

}
