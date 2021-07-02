using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBaseImp : MonoBehaviour
{
    [SerializeField, Header("雑魚敵の種類を選ぶ")] NormalEnemyData_scr.normalEnemyType normalEnemyType;
    protected bool isPausing = false;
    protected Rigidbody2D rigidbody2D;
    protected CommonForBattleScenes_scr commonForBattleScenes;
    protected Vector3 savedVelocity;
    float _time;
    float _power;

    const float DISAPPEAR_TIME = 5;


    /// <summary>
    /// Startメソッドで呼ぶ<br></br>
    /// </summary>
    protected void Initialize()
    {
        //攻撃力の取得
        _power = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.DamageAmount];

        //コンポーネントの取得
        commonForBattleScenes = GameObject.FindGameObjectWithTag(Common_scr.Tags.CommonForBattleScenes__BattleScene.ToString()).GetComponent<CommonForBattleScenes_scr>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        _time = 0;
    }


    protected void DestroyLater()
    {
        _time += Time.deltaTime;

        if (_time > DISAPPEAR_TIME)
        {
            Destroy(gameObject);
        }
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player__BattleScene.ToString())
        {
            //ダメージを与える
            collision.GetComponent<Player_scr>().GetDamage(_power);

            switch (normalEnemyType)
            {
                case NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone:
                    Player_scr player = collision.GetComponent<Player_scr>();

                    if (player == null)
                    {
                        throw new System.Exception();
                    }

                    //ダメージを与える
                    player.GetDamage(NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.DamageAmount]);

                    player.isStunning = true;
                    break;

                case NormalEnemyData_scr.normalEnemyType.SelfDestruct__MiddleDrone:
                    throw new System.Exception();

                case NormalEnemyData_scr.normalEnemyType.WideBeam__MiddleDrone:
                    break;

                default:    //それ以外ならプレイヤーに当たったら消滅する
                    Destroy(this.gameObject);
                    break;

            }

        }
    }
}
