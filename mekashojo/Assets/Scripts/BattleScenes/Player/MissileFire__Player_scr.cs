using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire__Player_scr : MonoBehaviour
{
    [SerializeField, Header("弾丸の速度")] float _speed;
    [SerializeField, Header("消える時間")] float _disappearTime;
    float _power;
    GameObject _player;
    GameObject _getInput;
    GetInput_scr _getInput_scr;
    float _time;
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントを取得
        _player = GameObject.FindGameObjectWithTag(Common_scr.Tags.Player_BattleScene.ToString());
        _getInput = GameObject.FindGameObjectWithTag(Common_scr.Tags.GetInput_BattleScene.ToString());
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _getInput_scr = _getInput.GetComponent<GetInput_scr>();

        //速度を設定
        _rigidbody2D.velocity = (_getInput_scr.mousePosition - _player.transform.position) / Vector3.Magnitude(_getInput_scr.mousePosition - _player.transform.position) * _speed;

        //初期設定
        _time = 0;
        _power = EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentType.SubWeapon__Missile][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Missile]][EquipmentData_scr.equipmentParameter.Power];
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Enemy_BattleScene.ToString())
        {
            EnemyGetDamage_scr enemyGetDamage = collision.GetComponent<EnemyGetDamage_scr>();

            enemyGetDamage.GetDamage(_power);
            Destroy(this.gameObject);
        }
    }
}
