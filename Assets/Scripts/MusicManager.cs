using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public bool changeSound = false;
    public AudioClip endSfx;
    private AudioSource source;

    private static MusicManager instance = null;

    public static MusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            if (changeSound)
            {
                instance.GetComponent<AudioSource>().Stop();
                instance.source.clip = this.source.clip;
                instance.GetComponent<AudioSource>().Play();
            }
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayEndSfx()
    {
        source.PlayOneShot(endSfx);
    }

    public void StopMusic()
    {
        source.Stop();
        print("Stop music");
    }
}
