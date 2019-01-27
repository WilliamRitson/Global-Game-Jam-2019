using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

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
            instance.source.UnPause();
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
        source.UnPause();
        
    }


    public void StopMusic()
    {
        source.Pause();
    }
}
