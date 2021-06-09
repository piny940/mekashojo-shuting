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
    float _time;
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Common_scr.Tags.Player_BattleScene.ToString());
        _getInput = GameObject.FindGameObjectWithTag(Common_scr.Tags.GetInput_BattleScene.ToString());
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _getInput_scr = _getInput.GetComponent<GetInput_scr>();
        _rigidbody2D.velocity = (_getInput_scr.mousePosition - _player.transform.position) / Vector3.Magnitude(_getInput_scr.mousePosition - _player.transform.position) * _speed;
        _time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(_time> _disappearTime)
        {
            gameObject.SetActive(false);
        }
    }
}
