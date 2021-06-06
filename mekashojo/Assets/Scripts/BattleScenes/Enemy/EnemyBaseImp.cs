using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseImp : MonoBehaviour
{
    bool _isPausing = false;
    bool _hasVelocitySet = false;
    bool _hasAnimationStarted = false;

    //移動速度の設定（移動速度は一定）
    public void SetVelocity(Rigidbody2D _rigidbody2D, float _speed)
    {
        if (!_hasVelocitySet)
        {
            _hasVelocitySet = true;
            _rigidbody2D.velocity = new Vector3(-_speed, 0, 0);

        }
    }


    /// <summary>
    /// ポーズ時の処理をする (アニメーションなしの時)
    /// </summary>
    public void Pause(StartCount_scr _startCount, float _speed, ref Rigidbody2D _rigidbody2D)
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
    public void Pause(StartCount_scr _startCount, float _speed, ref Rigidbody2D _rigidbody2D, Animator _animator)
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

    public void StartAnimation(Animator _animator)
    {
        if (!_hasAnimationStarted)
        {
            _animator.SetBool("hasStarted", true);
            _hasAnimationStarted = true;
        }
    }
    

}

