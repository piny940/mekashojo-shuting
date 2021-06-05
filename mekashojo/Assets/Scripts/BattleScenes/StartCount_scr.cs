using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCount_scr : MonoBehaviour
{
    
    [HideInInspector] public bool hasStarted;
    [HideInInspector] public bool isStarting;
    [SerializeField, Header("StartCount__1を入れる")] GameObject _startCount__1;
    [SerializeField, Header("StartCount__2を入れる")] GameObject _startCount__2;
    [SerializeField, Header("StartCount__3を入れる")] GameObject _startCount__3;
    float _countTime;
    bool _isStartCount1Active;
    bool _isStartCount2Active;
    bool _isStartCount3Active;


    // Start is called before the first frame update
    void Start()
    {
        _countTime = 0;
        _startCount__1.SetActive(false);
        _startCount__2.SetActive(false);
        _startCount__3.SetActive(false);
        _isStartCount1Active = false;
        _isStartCount2Active = false;
        _isStartCount3Active = false;
        hasStarted = false;
        isStarting = true;

    }

    // Update is called once per frame
    void Update()
    {
        StartCount();
    }

    /// <summary>
    /// ３、２、1とカウントする
    /// </summary>
    void StartCount()
    {
        if (!isStarting)
        {
            return;
        }

        _countTime += Time.deltaTime;

        if (_countTime <= 1 && !_isStartCount3Active)
        {
            //３番をアクティブにする
            _startCount__3.SetActive(true);
            _isStartCount3Active = true;

            return;
        }

        if (_countTime > 1 && _countTime <= 2 && !_isStartCount2Active)
        {
            //３番を非アクティブにする
            _startCount__3.SetActive(false);
            _isStartCount3Active = false;

            //２番をアクティブにする
            _startCount__2.SetActive(true);
            _isStartCount2Active = true;

            return;
        }

        if (_countTime > 2 && _countTime <= 3 && !_isStartCount1Active)
        {
            //2番を非アクティブにする
            _startCount__2.SetActive(false);
            _isStartCount2Active = false;

            //1番をアクティブにする
            _startCount__1.SetActive(true);
            _isStartCount1Active = true;

            return;
        }

        if (_countTime > 3)
        {
            _startCount__1.SetActive(false);
            _isStartCount1Active = false;

            _countTime = 0;
            isStarting = false;
            hasStarted = true;
        }

    }

}
