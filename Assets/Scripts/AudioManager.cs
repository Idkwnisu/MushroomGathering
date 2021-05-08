using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    public AudioSource soundSource;
    public AudioSource musicSource;

    public AudioClip[] sounds;
    public string[] soundNames;
    public AudioClip[] musics;
    public string[] musicNames;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSoundIndex(string sound)
    {
        int index = 0;
        for (int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == sound)
            {
                index = i;
                i = soundNames.Length;
            }
        }
        return index;
    }

    public int getMusicIndex(string music)
    {
        int index = 0;
        for (int i = 0; i < musicNames.Length; i++)
        {
            if (musicNames[i] == music)
            {
                index = i;
                i = musicNames.Length;
            }
        }
        return index;
    }

    public void PlaySound(string name)
    {
        int index = getSoundIndex(name);
        soundSource.PlayOneShot(sounds[index]);
    }

    public void PlayMusic(string name)
    {
        int index = getMusicIndex(name);
        musicSource.clip = musics[index];
        musicSource.Play();
    }
}
