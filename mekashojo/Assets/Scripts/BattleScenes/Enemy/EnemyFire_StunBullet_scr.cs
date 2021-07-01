using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire_StunBullet_scr : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    CommonForBattleScenes_scr _commonForBattleScenes;
    Vector3 _savedVelocity;
    bool _isPausing = false;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        _commonForBattleScenes = GameObject.FindGameObjectWithTag(Common_scr.Tags.CommonForBattleScenes__BattleScene.ToString()).GetComponent<CommonForBattleScenes_scr>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _commonForBattleScenes.Pause(_rigidbody2D, ref _isPausing, ref _savedVelocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player__BattleScene.ToString())
        {
            Player_scr player = collision.GetComponent<Player_scr>();

            if (player == null)
            {
                throw new System.Exception();
            }

            //ダメージを与える
            player.GetDamage(NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.DamageAmount]);

            player.isStunning = true;
        }
    }
}
