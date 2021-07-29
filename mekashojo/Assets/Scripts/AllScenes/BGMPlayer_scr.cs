using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer_scr : MonoBehaviour
{
    static public BGMPlayer_scr bgmPlayer;
    [HideInInspector] public AudioSource bgmAudioSource;

    private void Awake()
    {
        if (bgmPlayer == null)
        {
            bgmPlayer = this;
            DontDestroyOnLoad(this.gameObject);
            bgmAudioSource = GetComponent<AudioSource>();
            bgmAudioSource.Play();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
