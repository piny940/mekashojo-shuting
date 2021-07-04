using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBaseImp : MonoBehaviour
{
    protected NormalEnemyData_scr.normalEnemyType normalEnemyType;
    protected bool isPausing = false;
    protected new Rigidbody2D rigidbody2D;
    protected CommonForBattleScenes_scr commonForBattleScenes;
    protected Vector3 savedVelocity;
    const float SCREEN_FRAME = 1;

    /// <summary>
    /// Startメソッドで呼ぶ<br></br>
    /// これより前にnormalEnemyTypeを設定する
    /// </summary>
    protected void Initialize()
    {
        //コンポーネントの取得
        commonForBattleScenes = GameObject.FindGameObjectWithTag(Common_scr.Tags.CommonForBattleScenes__BattleScene.ToString()).GetComponent<CommonForBattleScenes_scr>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 画面の外に出たら消滅する
    /// </summary>
    protected void DestroyLater()
    {
        //画面左下と右上の座標の取得
        Vector3　cornerPosition__LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 cornerPosition__RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (transform.position.x < cornerPosition__LeftBottom.x - SCREEN_FRAME || transform.position.x > cornerPosition__RightTop.x + SCREEN_FRAME || transform.position.y > cornerPosition__RightTop.y + SCREEN_FRAME || transform.position.y < cornerPosition__LeftBottom.y - SCREEN_FRAME)
        {
            Destroy(gameObject);
        }
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player__BattleScene.ToString())
        {
            //ダメージを与える
            collision.GetComponent<Player_scr>().GetDamage(NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[normalEnemyType][NormalEnemyData_scr.normalEnemyParameter.DamageAmount]);

            switch (normalEnemyType)
            {
                //スタン型の場合は
                case NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone:
                    //Player_scrを取得
                    Player_scr player = collision.GetComponent<Player_scr>();

                    if (player == null)
                    {
                        throw new System.Exception();
                    }

                    //ダメージを与える
                    player.GetDamage(NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.StunBullet__SmallDrone][NormalEnemyData_scr.normalEnemyParameter.DamageAmount]);

                    //スタンさせる
                    player.isStunning = true;
                    break;

                //自爆型には対応していない
                case NormalEnemyData_scr.normalEnemyType.SelfDestruct__MiddleDrone:
                    throw new System.Exception();

                //全方位ビームの場合は何もしない
                case NormalEnemyData_scr.normalEnemyType.WideBeam__MiddleDrone:
                    break;

                //それ以外ならプレイヤーに当たったら消滅する
                default:
                    Destroy(this.gameObject);
                    break;

            }

        }
    }
}
