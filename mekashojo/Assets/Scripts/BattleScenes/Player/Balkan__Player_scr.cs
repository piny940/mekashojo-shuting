using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balkan__Player_scr : MonoBehaviour
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    [SerializeField, Header("1秒あたりに発射する球の数")] int _firePerSecound;
    int _count;

    // Start is called before the first frame update
    void Start()
    {
        _count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (_player.CanAttack())
        {
            //一定フレームごとに呼び出す
            if (_count < 60 / _firePerSecound)
            {
                _count++;
                return;
            }

            _count = 0;
            Instantiate((GameObject)Resources.Load("BattleScenes/BalkanFire__Player"), transform.position, Quaternion.identity);

            //エネルギーを減らす
            _player.subEnergyAmount -= EquipmentData_scr.equipmentData.equipmentStatus[_player.subWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[_player.subWeaponName]][EquipmentData_scr.equipmentParameter.Cost];
        }
    }
}
