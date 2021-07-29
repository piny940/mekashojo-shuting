using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseImp : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float _speed;
    protected bool isPausing = false;
    protected new Rigidbody2D rigidbody2D;
    protected CommonForBattleScenes_scr commonForBattleScenes;
    protected Vector3 savedVelocity;
    protected StartCount_scr _startCount;
    bool _hasVelocitySet = false;
    bool _hasAnimationStarted = false;
    EnemyController_scr _enemyController;
    const float SCREEN_FRAME = 1;


    /// <summary>
    /// Startメソッドで呼ぶ
    /// </summary>
    protected void Initialize()
    {
        //コンポーネントの取得

        rigidbody2D = GetComponent<Rigidbody2D>();

        _startCount = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.StartCount__BattleScene.ToString()).GetComponent<StartCount_scr>();

        _enemyController = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.EnemyController__BattleScene.ToString()).GetComponent<EnemyController_scr>();

        commonForBattleScenes = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.CommonForBattleScenes__BattleScene.ToString()).GetComponent<CommonForBattleScenes_scr>();

        //nullの場合
        if (_startCount == null || _enemyController == null || commonForBattleScenes == null)
        {
            throw new System.Exception();
        }
    }

    /// <summary>
    /// 移動速度の設定（移動速度は一定)
    /// </summary>
    protected void SetVelocity()
    {
        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        if (!_hasVelocitySet)
        {
            _hasVelocitySet = true;
            rigidbody2D.velocity = new Vector3(-_speed, 0, 0);

        }
    }
    

    /// <summary>
    /// アニメーションをスタートする
    /// </summary>
    protected void StartAnimation(Animator animator)
    {
        //まだ始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            return;
        }

        if (!_hasAnimationStarted)
        {
            animator.SetBool("hasStarted", true);
            _hasAnimationStarted = true;
        }
    }


    /// <summary>
    /// 画面の外に出たら消滅する
    /// </summary>
    protected void DestroyLater()
    {
        //画面左下と右上の座標の取得
        Vector3 cornerPosition__LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 cornerPosition__RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //画面の外に出たら消滅する
        //画面外という判定にはSCREEN_FRAMEの分だけ余裕を持たせておく
        if (transform.position.x < cornerPosition__LeftBottom.x - SCREEN_FRAME || transform.position.x > cornerPosition__RightTop.x + SCREEN_FRAME
            || transform.position.y > cornerPosition__RightTop.y + SCREEN_FRAME || transform.position.y < cornerPosition__LeftBottom.y - SCREEN_FRAME)
        {
            DestroyMyself();
        }
    }


    /// <summary>
    /// 消滅するときに呼ぶ
    /// </summary>
    protected void DestroyMyself()
    {
        _enemyController.EnemyAmount--;
        Destroy(this.gameObject);
    }
}

