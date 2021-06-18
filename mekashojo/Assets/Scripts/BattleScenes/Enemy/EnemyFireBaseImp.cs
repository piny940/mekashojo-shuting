using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBaseImp : MonoBehaviour
{
    protected NormalEnemyData_scr.normalEnemyType normalEnemyType;

    float _power;

    /// <summary>
    /// Startメソッドで呼ぶ<br></br>
    /// これを呼ぶ前にnormalEnemyTypeを設定する必要がある
    /// </summary>
    protected void Initialize()
    {
        //攻撃力の取得
        _power = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.DamageAmount];
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player__BattleScene.ToString())
        {
            //ダメージを与える
            collision.GetComponent<Player_scr>().GetDamage(_power);

            switch ((int)normalEnemyType)
            {
                case (int)NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone: //スタン型
                case (int)NormalEnemyData_scr.normalEnemyType.SelfDestruct__MiddleDrone: //自爆型
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
