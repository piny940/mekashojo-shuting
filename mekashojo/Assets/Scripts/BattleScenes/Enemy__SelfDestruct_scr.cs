using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SelfDestruct_scr : EnemyBaseImp
{
    [SerializeField, Header("移動速度")] float _speed;
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        _rigidbody2D = GetComponent<Rigidbody2D>();

        //移動速度の設定（移動速度は一定）
        SetVelocity(_rigidbody2D, _speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


}
