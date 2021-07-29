using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer_scr : MonoBehaviour
{
    public static SEPlayer_scr sePlayer = null;
    [HideInInspector] public AudioSource audioSource;

    private void Awake()
    {
        if (sePlayer == null)
        {
            sePlayer = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
