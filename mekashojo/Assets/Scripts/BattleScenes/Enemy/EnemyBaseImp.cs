using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseImp : MonoBehaviour
{
    bool _isPausing = false;
    bool _hasVelocitySet = false;
    bool _hasAnimationStarted = false;
    [SerializeField, Header("移動速度")] float _speed;
    StartCount_scr _startCount;
    Rigidbody2D _rigidbody2D;
    protected EnemyController_scr _enemyController;

    /// <summary>
    /// Startメソッドで呼ぶ
    /// </summary>
    protected void Initialize()
    {
        //コンポーネントの取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startCount = GameObject.FindGameObjectWithTag(Common_scr.Tags.StartCount_BattleScene.ToString()).GetComponent<StartCount_scr>();
        _enemyController = GameObject.FindGameObjectWithTag(Common_scr.Tags.EnemyController_BattleScene.ToString()).GetComponent<EnemyController_scr>();

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
            _rigidbody2D.velocity = new Vector3(-_speed, 0, 0);

        }
    }

    

    /// <summary>
    /// ポーズ時の処理をする (アニメーションなしの時)
    /// </summary>
    protected void Pause()
    {
        //ポーズし始めた時
        if (!_startCount.hasStarted && !_isPausing)
        {
            _rigidbody2D.velocity = new Vector3(0, 0, 0);
            _isPausing = true;
            return;
        }

        //ポーズし終わった時
        if (_startCount.hasStarted && _isPausing)
        {
            _rigidbody2D.velocity = new Vector3(-_speed, 0, 0);
            _isPausing = false;
        }


    }


    /// <summary>
    /// ポーズ時の処理をする (アニメーションありの時)
    /// </summary>
    protected void Pause(Animator animator)
    {
        //ポーズし始めた時
        if (!_startCount.hasStarted && !_isPausing)
        {
            _rigidbody2D.velocity = new Vector3(0, 0, 0);
            _isPausing = true;
            animator.SetBool("hasStarted", false);
            return;
        }

        //ポーズし終わった時
        if (_startCount.hasStarted && _isPausing)
        {
            _rigidbody2D.velocity = new Vector3(-_speed, 0, 0);
            _isPausing = false;
            animator.SetBool("hasStarted", true);
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

