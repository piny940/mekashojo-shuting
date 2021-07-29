using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkanFire__Player_scr : MonoBehaviour
{
    [SerializeField, Header("弾丸の速度")] float _speed;
    [SerializeField, Header("消える時間")] float _disappearTime;
    GameObject _player;
    GameObject _getInput;
    GetInput_scr _getInput_scr;
    Rigidbody2D _rigidbody2D;
    CommonForBattleScenes_scr _commonForBattleScenes;
    Vector3 _savedVelocity;
    float _power;
    float _time;
    bool _isPausing;

    // Start is called before the first frame update
    void Start()
    {
        //ゲームオブジェクトの取得
        _player = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.Player__BattleScene.ToString());
        _getInput = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.GetInput__BattleScene.ToString());

        //コンポーネントを取得
        _commonForBattleScenes = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.CommonForBattleScenes__BattleScene.ToString()).GetComponent<CommonForBattleScenes_scr>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _getInput_scr = _getInput.GetComponent<GetInput_scr>();

        //速度を設定
        _rigidbody2D.velocity = (_getInput_scr.mousePosition - _player.transform.position) / Vector3.Magnitude(_getInput_scr.mousePosition - _player.transform.position) * _speed;

        //初期設定
        _time = 0;
        _power = EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentType.SubWeapon__Balkan][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Balkan]][EquipmentData_scr.equipmentParameter.Power];

        //nullの場合
        if (_player == null || _getInput == null || _commonForBattleScenes == null)
        {
            throw new System.Exception();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //一定時間経過したら消滅する
        _time += Time.deltaTime;
        if (_time > _disappearTime)
        {
            Destroy(this.gameObject);
        }

        //ポーズの処理
        _commonForBattleScenes.ProceedPausing(_rigidbody2D, ref _isPausing, ref _savedVelocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagManager_scr.Tags.Enemy__BattleScene.ToString())
        {
            EnemyDamageManager enemyDamageManager = collision.GetComponent<EnemyDamageManager>();

            //nullの場合
            if (enemyDamageManager == null)
            {
                throw new System.Exception();
            }

            //弾が消滅する場合
            if (enemyDamageManager.hp >= _power)
            {
                enemyDamageManager.GetDamage(_power);
                Destroy(this.gameObject);
            }

            //弾が消滅しない場合
            float comtemporaryPower = _power;
            _power -= enemyDamageManager.hp;
            enemyDamageManager.GetDamage(comtemporaryPower);
        }
    }
}
