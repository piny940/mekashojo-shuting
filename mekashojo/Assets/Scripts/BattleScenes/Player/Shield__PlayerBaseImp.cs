using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield__PlayerBaseImp : MonoBehaviour
{
    [SerializeField, Header("シールドが小さくなっていく速さ")] float _shieldSmallerSpeed;
    [SerializeField, Header("シールドの大きさが回復していく速さ")] float _shieldRestoreSpeed;
    [Header("シールド使用時の速度の割合")] public float speedReduceRate;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    [SerializeField, Header("ShieldModelを入れる")] GameObject _shieldModel;
    
    float _defaultShieldSize;


    // Start is called before the first frame update
    protected void Start()
    {
        _defaultShieldSize = _shieldModel.transform.localScale.x;
        _shieldModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseShield()
    {
        if (_getInput.isMouseRight && _shieldModel.transform.localScale.x > 0)
        {
            if (!_player.isShieldUsing)
            {
                //シールドを使い始める時の処理
                _shieldModel.SetActive(true);
                _player.isShieldUsing = true;
            }

            _shieldModel.transform.localScale -= new Vector3(_shieldSmallerSpeed, _shieldSmallerSpeed, 0) * Time.deltaTime;
        }
        else
        {
            if (_player.isShieldUsing)
            {
                //シールドを使い終わる時の処理
                _shieldModel.SetActive(false);
                _player.isShieldUsing = false;
            }

            //右クリックを離したらシールドが回復する
            if (!_getInput.isMouseRight && _shieldModel.transform.localScale.x < _defaultShieldSize)
            {
                _shieldModel.transform.localScale += new Vector3(_shieldRestoreSpeed, _shieldRestoreSpeed, 0) * Time.deltaTime;
            }
            
        }
    }
}
