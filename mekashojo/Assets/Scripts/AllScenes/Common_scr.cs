using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_scr : MonoBehaviour
{
    public static Common_scr common = null;
    [HideInInspector] public AudioSource audioSource;

    public enum Tags
    {
        Enemy__BattleScene,
        EnemyFire__BattleScene,
        PlayerFire__BattleScene,
        PauseController__BattleScene,
        StartCount__BattleScene,
        GetInput__BattleScene,
        Player__BattleScene,
        EnemyController__BattleScene,
        CommonForBattleScenes__BattleScene
    }

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
        audioSource = GetComponent<AudioSource>();
    }
}
