using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_scr : MonoBehaviour
{
    [SerializeField, Header("移動速度")]float _speed;
    [SerializeField] GetInput_scr _getInput;
    Rigidbody2D _rigidbody2D;
    bool _mainSelected;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _mainSelected = true;   //初めはmain選択状態にしておく
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        SwitchWeapon();
    }


    /// <summary>
    /// プレイヤーを移動させる
    /// </summary>
    void MovePlayer()
    {
        //水平方向の移動
        if (_getInput.horizontalKey != 0)
        {
            _rigidbody2D.velocity = new Vector3(_speed * _getInput.horizontalKey, _rigidbody2D.velocity.y, 0);
        }
        else
        {
            _rigidbody2D.velocity = new Vector3(0, _rigidbody2D.velocity.y, 0);
        }

        //垂直方向の移動
        if (_getInput.verticalKey != 0)
        {
            _rigidbody2D.velocity = new Vector3(_rigidbody2D.velocity.x, _speed * _getInput.verticalKey, 0);
        }
        else
        {
            _rigidbody2D.velocity = new Vector3(_rigidbody2D.velocity.x, 0, 0);
        }


    }

    /// <summary>
    /// メイン武器とサブ武器と切り替える
    /// </summary>
    void SwitchWeapon()
    {
        //マウスホイールが奥に回された場合
        if (_getInput.mouseWheel > 0)
        {
            _mainSelected = true;
            //画面にメインが選択中だと表示する

            return;
        }

        //マウスホイールが手前に回された場合
        if (_getInput.mouseWheel > 0)
        {
            _mainSelected = false;
            //画面にサブが選択中だと表示する


        }


    }
}
