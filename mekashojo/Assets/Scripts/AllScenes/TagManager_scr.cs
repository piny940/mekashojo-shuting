using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager_scr : MonoBehaviour
{
    public static TagManager_scr tagManager = null;

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
        if (tagManager == null)
        {
            tagManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
