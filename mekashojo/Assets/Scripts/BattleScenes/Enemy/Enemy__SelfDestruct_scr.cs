using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__SelfDestruct_scr : EnemyBaseImp
{

    Animator _animator;
    float _power;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        _animator = GetComponent<Animator>();

        _power = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.SelfDestruct__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.DamageAmount];

        Initialize();

        
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity, _animator);
        
        //移動速度の設定
        SetVelocity();

        //アニメーションの開始
        StartAnimation(_animator);

        //画面の外に出たら消滅する
        DestroyLater();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagManager_scr.Tags.Player__BattleScene.ToString())
        {
            collision.GetComponent<Player_scr>().GetDamage(_power);

            Instantiate((GameObject)Resources.Load("BattleScenes/ExplodeEffect__SelfDestruct__Enemy"), transform.position, Quaternion.identity);

            DestroyMyself();
        }
    }
}
