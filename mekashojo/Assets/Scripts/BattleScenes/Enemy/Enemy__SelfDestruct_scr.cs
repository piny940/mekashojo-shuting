using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SelfDestruct_scr : EnemyBaseImp
{

    Animator _animator;

    // Start is called before the first frame update
    protected void Start()
    {
        //コンポーネントの取得
        _animator = GetComponent<Animator>();   //nullが返ってくる場合があるが参照しなければ問題ない(はず)

        Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        Pause(_animator);
        
        //移動速度の設定
        SetVelocity();
        StartAnimation(_animator);
        
    }
}
