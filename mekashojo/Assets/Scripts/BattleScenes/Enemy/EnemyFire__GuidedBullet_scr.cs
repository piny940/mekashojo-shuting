using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire__GuidedBullet_scr : EnemyFireBaseImp
{
    [SerializeField, Header("プレイヤーにどれくらい近づいたら追跡しなくなるか")] float _stopChasingDistance;
    GameObject _player;
    GameObject _startCountObject;
    StartCount_scr _startCount;
    bool _hasApproached;
    bool _isFirst;
    float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        normalEnemyType = NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone;

        //コンポーネントの取得
        _player = GameObject.FindGameObjectWithTag(Common_scr.Tags.Player__BattleScene.ToString());
        _startCountObject = GameObject.FindGameObjectWithTag(Common_scr.Tags.StartCount__BattleScene.ToString());

        //弾の速さの取得
        _speed = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[NormalEnemyData_scr.normalEnemyType.GuidedBullet__MiddleDrone][NormalEnemyData_scr.normalEnemyParameter.BulletSpeed];

        //nullの時の処理
        if (_player == null || _startCountObject == null)
        {
            throw new System.Exception();
        }

        _startCount = _startCountObject.GetComponent<StartCount_scr>();

        if (_startCount == null)
        {
            throw new System.Exception();
        }

        _isFirst = false;

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        commonForBattleScenes.ProceedPausing(GetComponent<Rigidbody2D>(), ref isPausing, ref savedVelocity);

        Move();

        DestroyLater();
    }

    void Move()
    {
        if (!_startCount.hasStarted)
        {
            return;
        }

        Vector3 adjustedPlayerPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, 10);

        float distance = Vector3.Magnitude(adjustedPlayerPosition - transform.position);


        if (distance < _stopChasingDistance)
        {
            _hasApproached = true;
        }

        if (!_hasApproached || _isFirst)
        {
            GetComponent<Rigidbody2D>().velocity = (adjustedPlayerPosition - transform.position) * _speed / distance;
            _isFirst = false;
        }
    }
}
