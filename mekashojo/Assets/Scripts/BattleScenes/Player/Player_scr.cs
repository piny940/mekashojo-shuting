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
    [SerializeField, Header("HPの最大値")] int _maxHP;
    [SerializeField, Header("メインエネルギーの最大値")] float _maxMainEnergy;
    [SerializeField, Header("サブエネルギーの最大値")] float _maxSubEnergy;
    [SerializeField, Header("敵と接触したときに受けるダメージ量")] int _contactDamageAmount;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;
    [SerializeField, Header("MainTextを入れる")] GameObject _mainText;
    [SerializeField, Header("SubTextを入れる")] GameObject _subText;
    [SerializeField, Header("HPBarContentを入れる")] GameObject _hpBarContent;
    [SerializeField, Header("MainEnergyBarContentを入れる")] GameObject _mainEnergyBarContent;
    [SerializeField, Header("SubEnergyBarContentを入れる")] GameObject _subEnergyBarContent;
    [SerializeField, Header("武器を入れる(順番注意)")] List<GameObject> _weapons;
    [HideInInspector] public float mainEnergyAmount;
    [HideInInspector] public float subEnergyAmount;
    [HideInInspector] public EquipmentData_scr.equipmentType mainWeaponName;
    [HideInInspector] public EquipmentData_scr.equipmentType subWeaponName;
    Cannon__Player_scr _cannon__Player;
    Laser__Player_scr _laser__Player;
    BeamMachineGun__Player_scr _beamMachineGun__Player;
    Balkan__Player_scr _balkan__Player;
    Missile__Player_scr _missile__Player;
    Bomb__Player_scr _bomb__Player;
    HeavyShield__Player_scr _heavyShield__Player;
    LightShield__Player_scr _lightShield__Player;
    int _hpAmount;
    int _balkanCount;
    Image _mainTextImage;
    Image _subTextImage;
    Image _hpBarContentImage;
    Image _mainEnergyBarContentImage;
    Image _subEnergyBarContentImage;
    Rigidbody2D _rigidbody2D;
    Action MainAttack;
    Action SubAttack;
    

    bool _hasAttacked;
    bool _isMainSelected;
    bool _isPausing;
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
        //初めはmain選択状態にしておく
        _isMainSelected = true;
        _mainTextImage.color = new Color(1, 1, 1, 1);
        _subTextImage.color = new Color(1, 1, 1, 0.2f);

        //HPとエネルギー値はmaxにしておく
        _hpAmount = _maxHP;
        _hpBarContentImage.fillAmount = 1;

        mainEnergyAmount = _maxMainEnergy;
        _mainEnergyBarContentImage.fillAmount = 1;

        subEnergyAmount = _maxSubEnergy;
        _subEnergyBarContentImage.fillAmount = 1;

        //その他
        _hasAttacked = false;
        _balkanCount = 0;

        //武器を設定
        SetWeapon();
        
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

    
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="power"></param>
    void GetDamage(int power)
    {
        //死ぬ場合
        if (_hpAmount <= power)
        {
            _hpAmount = 0;
            _hpBarContentImage.fillAmount = 0;

            return;
        }

        //生きてる場合
        _hpAmount -= power;
        _hpBarContentImage.fillAmount -= (float)power / (float)_maxHP;
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

        //選択中の武器をアクティブにする
        _weapons[(int)EquipmentData_scr.equipmentData.selectedMainWeaponName].SetActive(true);
        _weapons[(int)EquipmentData_scr.equipmentData.selectedSubWeaponName].SetActive(true);
        _weapons[(int)EquipmentData_scr.equipmentType.Bomb].SetActive(true);
        _weapons[(int)EquipmentData_scr.equipmentData.selectedShieldName].SetActive(true);


        //メイン武器の設定
        mainWeaponName = EquipmentData_scr.equipmentData.selectedMainWeaponName;

        switch ((int)EquipmentData_scr.equipmentData.selectedMainWeaponName)
        {
            case 0:
                MainAttack = _cannon__Player.Attack;
                break;
            case 1:
                MainAttack = _laser__Player.Attack;
                break;
            case 2:
                MainAttack = _beamMachineGun__Player.Attack;
                break;
            default:
                Debug.Log("メイン武器に対応していない武器が設定されています");
                break;

        }

        //サブ武器の設定
        subWeaponName = EquipmentData_scr.equipmentData.selectedSubWeaponName;

        switch ((int)EquipmentData_scr.equipmentData.selectedSubWeaponName)
        {
            case 3:
                SubAttack = _balkan__Player.Attack;
                break;
            case 4:
                SubAttack = _missile__Player.Attack;
                break;
            default:
                Debug.Log("サブ武器に対応していない武器が設定されています");
                break;
        }
    }


    void Attack()
    {
        if (_isMainSelected)
        {
            MainAttack();
            _mainEnergyBarContentImage.fillAmount = mainEnergyAmount / _maxMainEnergy;
            return;
        }


        //サブが選択されていた時の処理
        if (subEnergyAmount <= 0)
        {
            return;
        }

        if (subWeaponName == EquipmentData_scr.equipmentType.SubWeapon__Missile)
        {

            //ミサイルは1クリック分の処理しかしない

            //左クリックを離した瞬間の処理
            if (_hasAttacked && !_getInput.isMouseLeft)
            {
                _hasAttacked = false;
                return;
            }

            //左クリックを押した状態でも2フレーム目以降は何もしない
            if (_hasAttacked)
            {
                return;
            }

            if (_getInput.isMouseLeft)
            {
                _hasAttacked = true;

                SubAttack();
                subEnergyAmount -= EquipmentData_scr.equipmentData.equipmentStatus[subWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[subWeaponName]][EquipmentData_scr.equipmentParameter.Cost];
                _subEnergyBarContentImage.fillAmount = subEnergyAmount / _maxSubEnergy;
                return;
            }

        }

        SubAttack();
        _subEnergyBarContentImage.fillAmount = subEnergyAmount / _maxSubEnergy;


    }
}
