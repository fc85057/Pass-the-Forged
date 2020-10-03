using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource[] soundSources;
    [SerializeField]
    private Dictionary<string, AudioSource> sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        soundSources = GetComponents<AudioSource>();
        sounds = new Dictionary<string, AudioSource>();

        foreach (AudioSource source in soundSources)
        {
            sounds.Add(source.clip.name, source);
        }

    }

    public void PlaySound(string soundName)
    {
        if (sounds[soundName] != null)
        {
            sounds[soundName].pitch = Random.Range(0.9f, 1.1f);
            sounds[soundName].Play();
        }
        
    }

}
