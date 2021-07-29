using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb__Player_scr : MonoBehaviour
{
    [SerializeField, Header("衝撃波が大きくなっていくスピード")] float _bombExpandSpeed;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    [SerializeField, Header("BombFireを入れる")] GameObject _bombFire;
    bool _isBombActive
    {
        get { return _bombFire.activeSelf; }
        set { _bombFire.SetActive(value); }
    }
    const float MAX_BOBM_SIZE = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        _bombFire.transform.localScale = new Vector3(0, 0, 1);
        _isBombActive = false;

        //_bombExpandSpeedが0だと無限ループに陥るので回避用
        if (_bombExpandSpeed == 0)
        {
            throw new System.Exception();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        //BombFireをアクティブにする
        if (!_isBombActive)
        {
            _isBombActive = true;
        }

        if (_bombFire.transform.localScale.x < MAX_BOBM_SIZE)
        {
            //ボムを大きくしていく
            _bombFire.transform.localScale += new Vector3(_bombExpandSpeed, _bombExpandSpeed, 0) * Time.deltaTime;
        }
        else
        {
            _bombFire.transform.localScale = new Vector3(0, 0, 1);
            _player.isBombUsing = false;
            _isBombActive = false;
        }
    }
}
