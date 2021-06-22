using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player_scr : MonoBehaviour
{
    #region
    //変数を宣言
    [SerializeField, Header("移動速度")]float _speed;
    [SerializeField, Header("HPの最大値")] float _maxHP;
    [SerializeField, Header("メインエネルギーの1秒あたりの回復量")] float _mainEnergyChargePerSecond;
    [SerializeField, Header("サブエネルギーの1秒あたりの回復量")] float _subEnergyChargePerSecond;
    [SerializeField, Header("敵と接触したときに受けるダメージ量")] float _contactDamageAmount;
    [Header("メインエネルギーの最大値")] public float maxMainEnergyAmount;
    [Header("サブエネルギーの最大値")] public float maxSubEnergyAmount;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;
    [SerializeField, Header("MainTextを入れる")] GameObject _mainText;
    [SerializeField, Header("SubTextを入れる")] GameObject _subText;
    [SerializeField, Header("HPBarContentを入れる")] GameObject _hpBarContent;
    [SerializeField, Header("MainEnergyBarContentを入れる")] GameObject _mainEnergyBarContent;
    [SerializeField, Header("SubEnergyBarContentを入れる")] GameObject _subEnergyBarContent;
    [SerializeField, Header("武器を入れる(順番注意)")] List<GameObject> _weapons;
    [SerializeField, Header("PlayerModelを入れる(順番注意)")] List<GameObject> _playerModels;
    [SerializeField, Header("HavingBombを入れる(1,2,3の順)")] List<GameObject> _havingBombs;
    [HideInInspector] public float mainEnergyAmount { get; set; }
    [HideInInspector] public float subEnergyAmount { get; set; }
    [HideInInspector] public bool isMainSelected { get; private set; }
    [HideInInspector] public EquipmentData_scr.equipmentType mainWeaponName { get { return EquipmentData_scr.equipmentData.selectedMainWeaponName; } }
    [HideInInspector] public EquipmentData_scr.equipmentType subWeaponName { get { return EquipmentData_scr.equipmentData.selectedSubWeaponName; } }
    Cannon__Player_scr _cannon__Player;
    Laser__Player_scr _laser__Player;
    BeamMachineGun__Player_scr _beamMachineGun__Player;
    Balkan__Player_scr _balkan__Player;
    Missile__Player_scr _missile__Player;
    Bomb__Player_scr _bomb__Player;
    HeavyShield__Player_scr _heavyShield__Player;
    LightShield__Player_scr _lightShield__Player;
    GameObject _playerModel__Main;
    GameObject _playerModel__Sub;
    Image _mainTextImage;
    Image _subTextImage;
    Image _hpBarContentImage;
    Image _mainEnergyBarContentImage;
    Image _subEnergyBarContentImage;
    Rigidbody2D _rigidbody2D;
    Action MainAttack;
    Action SubAttack;
    float _hpAmount;
    bool _isPausing;
    bool _isSwitchingWeapon;
    int _havingBombAmount;
    const int MAX_BOMB_AMOUNT = 3;
    #endregion

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
        _cannon__Player = _weapons[0].GetComponent<Cannon__Player_scr>();
        _laser__Player = _weapons[1].GetComponent<Laser__Player_scr>();
        _beamMachineGun__Player = _weapons[2].GetComponent<BeamMachineGun__Player_scr>();
        _balkan__Player = _weapons[3].GetComponent<Balkan__Player_scr>();
        _missile__Player = _weapons[4].GetComponent<Missile__Player_scr>();
        _bomb__Player = _weapons[5].GetComponent<Bomb__Player_scr>();
        _heavyShield__Player = _weapons[6].GetComponent<HeavyShield__Player_scr>();
        _lightShield__Player = _weapons[7].GetComponent<LightShield__Player_scr>();


        //初期化
        //武器を設定
        SetWeapon();

        //初めはmain選択状態にしておく
        isMainSelected = true;
        _mainTextImage.color = new Color(1, 1, 1, 1);
        _subTextImage.color = new Color(1, 1, 1, 0.2f);
        _playerModel__Main.SetActive(true);     //注意！_playerModel__Mainの設定はSetWeaponでやっているためこれをSetWeaponより先に走らせるとバグる

        //HPとエネルギー値はmaxにしておく
        _hpAmount = _maxHP;
        _hpBarContentImage.fillAmount = 1;

        mainEnergyAmount = maxMainEnergyAmount;
        _mainEnergyBarContentImage.fillAmount = 1;

        subEnergyAmount = maxSubEnergyAmount;
        _subEnergyBarContentImage.fillAmount = 1;

        //ボムの所持数は0にしておく
        _havingBombAmount = 0;
        for(int i = 0; i < MAX_BOMB_AMOUNT; i++)
        {
            _havingBombs[i].SetActive(false);
        }

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

        Attack();

        AutoEnergyCharge();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触ダメージ
        if (collision.tag == Common_scr.Tags.Enemy__BattleScene.ToString())
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
        if (_getInput.mouseWheel > 0 && !isMainSelected)
        {
            isMainSelected = true;
            _playerModel__Main.SetActive(true);
            _playerModel__Sub.SetActive(false);

            //画面にメインが選択中だと表示する
            _mainTextImage.color = new Color(1, 1, 1, 1);
            _subTextImage.color = new Color(1, 1, 1, 0.2f);

            //武器切り替え中フラグを立てる
            _isSwitchingWeapon = true;

            
            return;
        }

        //マウスホイールが手前に回された場合
        if (_getInput.mouseWheel < 0 && isMainSelected)
        {
            isMainSelected = false;
            _playerModel__Main.SetActive(false);
            _playerModel__Sub.SetActive(true);

            //画面にサブが選択中だと表示する
            _mainTextImage.color = new Color(1, 1, 1, 0.2f);
            _subTextImage.color = new Color(1, 1, 1, 1);

            //メイン武器の使用をやめる
            if (mainWeaponName == EquipmentData_scr.equipmentType.MainWeapon__Cannon)
            {
                _cannon__Player.StopUsing();
            }
            else if (mainWeaponName == EquipmentData_scr.equipmentType.MainWeapon__Laser)
            {
                _laser__Player.StopUsing();
            }

            //武器切り替え中フラグを立てる
            _isSwitchingWeapon = true;
        }

        //キャノンやレーザーを使用したまま武器を切り替えると、切り替えた瞬間に弾が飛び出すため、「サブ武器にミサイルがセットされている場合は」左クリックを離すまでは「切り替え中」状態にし、サブ武器を使えないようにする
        //使用できないようにするコードはAttackメソッドに書いた
        if (_isSwitchingWeapon && !_getInput.isMouseLeft)
        {
            _isSwitchingWeapon = false;
        }

    }

    
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="power"></param>
    public void GetDamage(float power)
    {
        //死ぬ場合
        if (_hpAmount <= power)
        {
            _hpAmount = 0;
            _hpBarContentImage.fillAmount = 0;
            //死んだときの処理
            return;
        }

        //生きてる場合
        _hpAmount -= power;
        _hpBarContentImage.fillAmount -= power / _maxHP;
    }


    /// <summary>
    /// 武器を設定する
    /// </summary>
    void SetWeapon()
    {
        //一旦全て非アクティブ
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].SetActive(false);
        }

        for(int i = 0; i < _playerModels.Count; i++)
        {
            _playerModels[i].SetActive(false);
        }

        //選択中の武器をアクティブにする
        _weapons[(int)EquipmentData_scr.equipmentData.selectedMainWeaponName].SetActive(true);
        _weapons[(int)EquipmentData_scr.equipmentData.selectedSubWeaponName].SetActive(true);
        _weapons[(int)EquipmentData_scr.equipmentType.Bomb].SetActive(true);
        _weapons[(int)EquipmentData_scr.equipmentData.selectedShieldName].SetActive(true);


        //メイン武器の設定
        switch ((int)EquipmentData_scr.equipmentData.selectedMainWeaponName)
        {
            case (int)EquipmentData_scr.equipmentType.MainWeapon__Cannon:
                MainAttack = _cannon__Player.Attack;
                _playerModel__Main = _playerModels[0];
                break;
            case (int)EquipmentData_scr.equipmentType.MainWeapon__Laser:
                MainAttack = _laser__Player.Attack;
                _playerModel__Main = _playerModels[1];
                break;
            case (int)EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                MainAttack = _beamMachineGun__Player.Attack;
                _playerModel__Main = _playerModels[2];
                break;
            default:
                throw new System.Exception();

        }

        //サブ武器の設定
        switch ((int)EquipmentData_scr.equipmentData.selectedSubWeaponName)
        {
            case (int)EquipmentData_scr.equipmentType.SubWeapon__Balkan:
                SubAttack = _balkan__Player.Attack;
                _playerModel__Sub = _playerModels[3];
                break;
            case (int)EquipmentData_scr.equipmentType.SubWeapon__Missile:
                SubAttack = _missile__Player.Attack;
                _playerModel__Sub = _playerModels[4];
                break;
            default:
                throw new System.Exception();
        }
    }


    /// <summary>
    /// 左クリック攻撃をする
    /// </summary>
    void Attack()
    {
        //メインが選択されていた時の処理
        if (isMainSelected)
        {
            MainAttack();
            return;
        }

        //武器切り替え中はミサイルは使えないようにする
        if (_isSwitchingWeapon && EquipmentData_scr.equipmentData.selectedSubWeaponName == EquipmentData_scr.equipmentType.SubWeapon__Missile)
        {
            return;
        }

        //サブが選択されていた時の処理
        SubAttack();
    }


    /// <summary>
    /// エネルギーの自動回復
    /// </summary>
    void AutoEnergyCharge()
    {
        //メインエネルギーの回復
        if (mainEnergyAmount < maxMainEnergyAmount)
        {
            mainEnergyAmount += _mainEnergyChargePerSecond * Time.deltaTime;
        }

        //サブエネルギーの回復
        if (subEnergyAmount < maxSubEnergyAmount)
        {
            subEnergyAmount += _subEnergyChargePerSecond * Time.deltaTime;
        }

        //エネルギー表示の更新
        _mainEnergyBarContentImage.fillAmount = mainEnergyAmount / maxMainEnergyAmount;
        _subEnergyBarContentImage.fillAmount = subEnergyAmount / maxSubEnergyAmount;
    }

    /// <summary>
    /// ボムを１つ加える
    /// </summary>
    public void AddBomb()
    {
        if (_havingBombAmount < 3)
        {
            _havingBombAmount++;
            _havingBombs[_havingBombAmount - 1].SetActive(true);
        }
    }
}
