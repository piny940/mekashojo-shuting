using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy__WideBeam_scr : EnemyBaseImp
{
    float _firingInterval;
    float _time;
    bool _isBeamsActive;
    bool _isColliderEnabled;
    [SerializeField, Header("Beamを発射しておく時間")] float _attackTime;
    [SerializeField, Header("Beamを入れる")] GameObject _beam;
    SpriteRenderer _spriteRenderer;
    PolygonCollider2D _polygonCollier2D;
    

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        _firingInterval = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.WideBeam__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.FiringInterval];

        _time = Random.value * _firingInterval;

        _isBeamsActive = false;

        //コンポーネントの取得
        _spriteRenderer = _beam.GetComponent<SpriteRenderer>();
        _polygonCollier2D = _beam.GetComponent<PolygonCollider2D>();

        //ビームを非アクティブにする
        _beam.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの処理
        commonForBattleScenes.ProceedPausing(rigidbody2D, ref isPausing, ref savedVelocity);

        //移動速度の設定
        SetVelocity();

        //攻撃
        Attack();

        //画面の外に出たら消滅する
        DestroyLater();
    }
    
    void Attack()
    {
        if (!_startCount.hasStarted)
        {
            return;
        }

        _time += Time.deltaTime;

        //平常時
        if (_time <= _firingInterval - 1)
        {
            return;
        }

        //攻撃予告
        if (_time < _firingInterval)
        {
            if (!_isBeamsActive)
            {
                //ビームをアクティブにする
                _beam.SetActive(true);
                _isBeamsActive = true;

                //当たり判定はなくしておく
                _polygonCollier2D.enabled = false;
                _isColliderEnabled = false;

                //薄く表示させる
                _spriteRenderer.color = new Color(1, 1, 1, 0.1f);
                
            }

            return;
        }

        //攻撃時
        if (_time < _firingInterval + _attackTime)
        {
            if (!_isColliderEnabled)
            {
                //当たり判定をOnにする
                _polygonCollier2D.enabled = true;

                //ちゃんと表示する
                _spriteRenderer.color = new Color(1, 1, 1, 1);

            }
        }

        //攻撃終了時
        if (_time >= _firingInterval + _attackTime)
        {
            //Beamを非アクティブにする
            _beam.SetActive(false);
            _isBeamsActive = false;

            _time = 0;
        }
    }
}
