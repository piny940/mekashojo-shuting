using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseImp : MonoBehaviour
{
    /// <summary>
    /// 速度を設定する
    /// </summary>
    /// <param name="_rigidbody2D"></param>
    /// <param name="_speed"></param>
    public void SetVelocity(Rigidbody2D _rigidbody2D,float _speed)
    {
        _rigidbody2D.velocity = new Vector3(-_speed, 0, 0);
    }

    
}

