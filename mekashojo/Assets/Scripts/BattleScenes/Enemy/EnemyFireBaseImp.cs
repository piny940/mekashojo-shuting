using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBaseImp : MonoBehaviour
{
    [SerializeField, Header("敵の種類を選ぶ")] NormalEnemyData_scr.normalEnemyType _normalEnemyType;

    float _power;

    protected void Initialize()
    {
        //攻撃力の取得
        _power = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[_normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.DamageAmount];
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player_BattleScene.ToString())
        {
            //ダメージを与える
            collision.GetComponent<Player_scr>().GetDamage(_power);

            switch ((int)_normalEnemyType)
            {
                case 2: //スタン型
                    Debug.Log("このclassはスタン型には対応していません");
                    break;
                case 10: //自爆型
                    Debug.Log("このclassは自爆型には対応していません");
                    break;
                case 7: //全方位ビーム
                    break;
                default:    //それ以外ならプレイヤーに当たったら消滅する
                    Destroy(this.gameObject);
                    break;

            }
            
        }
    }
}
