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

    float _power;


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
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player__BattleScene.ToString())
        {
            //ダメージを与える
            collision.GetComponent<Player_scr>().GetDamage(_power);

            switch ((int)normalEnemyType)
            {
                case 2: //スタン型
                    throw new System.Exception();

                case 10: //自爆型
                    throw new System.Exception();

                case 7: //全方位ビーム
                    break;

                default:    //それ以外ならプレイヤーに当たったら消滅する
                    Destroy(this.gameObject);
                    break;

            }

        }
    }
}
