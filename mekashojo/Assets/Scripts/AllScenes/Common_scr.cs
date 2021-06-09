using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_scr : MonoBehaviour
{
    public static Common_scr common = null;
    AudioSource _audioSource;

    public enum Tags
    {
        Enemy_BattleScene,
        Enemy__Fire_BattleScene,
        Player__Fire_BattleScene,
        PauseController_BattleScene,
        StartCount_BattleScene,
        GetInput_BattleScene,
        Player_BattleScene
    }

    //シングルトン
    private void Awake()
    {
        if (common == null)
        {
            common = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

}
