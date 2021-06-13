using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseImp : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float _speed;
    protected bool isPausing = false;
    protected new Rigidbody2D rigidbody2D;
    protected CommonForBattleScenes_scr commonForBattleScenes;
    protected Vector3 savedVelocity;
    bool _hasVelocitySet = false;
    bool _hasAnimationStarted = false;
    StartCount_scr _startCount;
    EnemyController_scr _enemyController;


    /// <summary>
    /// Startメソッドで呼ぶ
    /// </summary>
    protected void Initialize()
    {
        //コンポーネントの取得

        rigidbody2D = GetComponent<Rigidbody2D>();

        _startCount = GameObject.FindGameObjectWithTag(Common_scr.Tags.StartCount_BattleScene.ToString()).GetComponent<StartCount_scr>();

        _enemyController = GameObject.FindGameObjectWithTag(Common_scr.Tags.EnemyController_BattleScene.ToString()).GetComponent<EnemyController_scr>();

        commonForBattleScenes = GameObject.FindGameObjectWithTag(Common_scr.Tags.CommonForBattleScenes_BattleScene.ToString()).GetComponent<CommonForBattleScenes_scr>();
    }

    /// <summary>
    /// 移動速度の設定（移動速度は一定)
    /// </summary>
    protected void SetVelocity()
    {
        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        if (!_hasVelocitySet)
        {
            _hasVelocitySet = true;
            rigidbody2D.velocity = new Vector3(-_speed, 0, 0);

        }
    }
    

    /// <summary>
    /// アニメーションをスタートする
    /// </summary>
    protected void StartAnimation(Animator animator)
    {
        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        if (!_hasAnimationStarted)
        {
            animator.SetBool("hasStarted", true);
            _hasAnimationStarted = true;
        }
    }


    /// <summary>
    /// 消滅するときに呼ぶ
    /// </summary>
    protected void DestroyMyself()
    {
        _enemyController.EnemyAmount--;
        Destroy(this.gameObject);
    }
}

