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
    [SerializeField, Header("敵と接触したときに受けるダメージ量")] int _contactDamageAmount;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;
    [SerializeField, Header("MainTextを入れる")] GameObject _mainText;
    [SerializeField, Header("SubTextを入れる")] GameObject _subText;
    [SerializeField, Header("HPBarContentを入れる")] GameObject _hpBarContent;
    [SerializeField, Header("MainEnergyBarContentを入れる")] GameObject _mainEnergyBarContent;
    [SerializeField, Header("SubEnergyBarContentを入れる")] GameObject _subEnergyBarContent;
    [SerializeField, Header("ダメージを受けたときになる音")] AudioClip _damageSE;
    int _hpAmount;
    int _mainEnergyAmount;
    int _subEnergyAmount;
    Image _mainTextImage;
    Image _subTextImage;
    Image _hpBarContentImage;
    Image _mainEnergyBarContentImage;
    Image _subEnergyBarContentImage;
    Rigidbody2D _rigidbody2D;
    AudioSource _audioSource;
    bool _isMainSelected;
    bool _isPausing;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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
        //始まってなかったら抜ける
        if (!_startCount.hasStarted)
        {
            if (!_isPausing)
            {
                _rigidbody2D.velocity = new Vector3(0, 0, 0);
                _isPausing = true;
            }
            return;
        }

        if (_startCount.hasStarted && _isPausing)
        {
            _isPausing = false;
        }



        MovePlayer();
        SwitchWeapon();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触ダメージ
        if (collision.tag == Common_scr.Tags.Enemy_BattleScene.ToString())
        {
            GetDamage(_contactDamageAmount);
        }
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

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="_power"></param>
    void GetDamage(int _power)
    {
        //死ぬ場合
        if (_hpAmount <= _power)
        {
            _hpAmount = 0;
            _hpBarContentImage.fillAmount = 0;

            return;
        }

        //生きてる場合
        _hpAmount -= _power;
        _hpBarContentImage.fillAmount -= (float)_power / (float)_maxHP;
        Common_scr.common.PlaySE(_damageSE);
    }


}
