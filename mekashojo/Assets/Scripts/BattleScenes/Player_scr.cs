using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_scr : MonoBehaviour
{
    [SerializeField, Header("移動速度")]float _speed;
    [SerializeField, Header("HPの最大値")] int _maxHP;
    [SerializeField, Header("メインエネルギーの最大値")] int _maxMainEnergy;
    [SerializeField, Header("サブエネルギーの最大値")] int _maxSubEnergy;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("MainTextを入れる")] GameObject _mainText;
    [SerializeField, Header("SubTextを入れる")] GameObject _subText;
    [SerializeField, Header("HPBarContentを入れる")] GameObject _hpBarContent;
    [SerializeField, Header("MainEnergyBarContentを入れる")] GameObject _mainEnergyBarContent;
    [SerializeField, Header("SubEnergyBarContentを入れる")] GameObject _subEnergyBarContent;
    int _hpAmount;
    int _mainEnergyAmount;
    int _subEnergyAmount;
    Image _mainTextImage;
    Image _subTextImage;
    Image _hpBarContentImage;
    Image _mainEnergyBarContentImage;
    Image _subEnergyBarContentImage;
    Rigidbody2D _rigidbody2D;
    bool _isMainSelected;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _mainTextImage = _mainText.GetComponent<Image>();
        _subTextImage = _subText.GetComponent<Image>();
        _hpBarContentImage = _hpBarContent.GetComponent<Image>();
        _mainEnergyBarContentImage = _mainEnergyBarContent.GetComponent<Image>();
        _subEnergyBarContentImage = _subEnergyBarContent.GetComponent<Image>();


        //初期化
        //初めはmain選択状態にしておく
        _isMainSelected = true;
        _mainTextImage.color = new Color(1, 1, 1, 1);
        _subTextImage.color = new Color(1, 1, 1, 0.2f);

        //HPとエネルギー値はmaxにしておく
        _hpAmount = _maxHP;
        _hpBarContentImage.fillAmount = 1;

        _mainEnergyAmount = _maxMainEnergy;
        _mainEnergyBarContentImage.fillAmount = 1;

        _subEnergyAmount = _maxSubEnergy;
        _subEnergyBarContentImage.fillAmount = 1;
        
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
            _isMainSelected = true;

            //画面にメインが選択中だと表示する
            _mainTextImage.color = new Color(1, 1, 1, 1);
            _subTextImage.color = new Color(1, 1, 1, 0.2f);

            return;
        }

        //マウスホイールが手前に回された場合
        if (_getInput.mouseWheel < 0)
        {
            _isMainSelected = false;

            //画面にサブが選択中だと表示する
            _mainTextImage.color = new Color(1, 1, 1, 0.2f);
            _subTextImage.color = new Color(1, 1, 1, 1);

        }


    }

    void Attack()
    {

    }


}
