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
    Animator _animator;

    // Start is called before the first frame update
    protected void Start()
    {
        //コンポーネントの取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();   //nullが返ってくる場合があるが参照しなければ問題ない(はず)
        _startCount = GameObject.FindGameObjectWithTag(Common_scr.Tags.StartCount_BattleScene.ToString()).GetComponent<StartCount_scr>();

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
    protected void PauseWithoutAnimation()
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
    protected void PauseWithAnimation()
    {
        //ポーズし始めた時
        if (!_startCount.hasStarted && !_isPausing)
        {
            _rigidbody2D.velocity = new Vector3(0, 0, 0);
            _isPausing = true;
            _animator.SetBool("hasStarted", false);
            return;
        }

        //ポーズし終わった時
        if (_startCount.hasStarted && _isPausing)
        {
            _rigidbody2D.velocity = new Vector3(-_speed, 0, 0);
            _isPausing = false;
            _animator.SetBool("hasStarted", true);
        }


    }

    /// <summary>
    /// アニメーションをスタートする
    /// </summary>
    protected void StartAnimation()
    {
        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        if (!_hasAnimationStarted)
        {
            _animator.SetBool("hasStarted", true);
            _hasAnimationStarted = true;
        }
    }

}

