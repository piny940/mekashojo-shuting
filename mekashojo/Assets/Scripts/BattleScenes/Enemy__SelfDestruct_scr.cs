using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SelfDestruct_scr : EnemyBaseImp
{
    [SerializeField, Header("移動速度")] float _speed;
    StartCount_scr _startCount;
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startCount = GameObject.FindGameObjectWithTag(Common_scr.Tags.StartCount_BattleScene.ToString()).GetComponent<StartCount_scr>();




    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        Pause(_startCount, _speed, ref _rigidbody2D);

        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        //移動速度の設定
        SetVelocity(_rigidbody2D, _speed);

        
    }





}
