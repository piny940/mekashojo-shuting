using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCount_scr : MonoBehaviour
{

    [HideInInspector] public bool hasStarted { get; set; }
    [HideInInspector] public bool isStarting { get; set; }
    [SerializeField, Header("StartCount__1を入れる")] GameObject _startCount__1;
    [SerializeField, Header("StartCount__2を入れる")] GameObject _startCount__2;
    [SerializeField, Header("StartCount__3を入れる")] GameObject _startCount__3;
    float _countTime;
    int _displayingNumber;  //数字を表示していないときは0を入れる


    // Start is called before the first frame update
    void Start()
    {
        _countTime = 0;
        _startCount__1.SetActive(false);
        _startCount__2.SetActive(false);
        _startCount__3.SetActive(false);
        _displayingNumber = 0;
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

        if (_countTime <= 1 && _displayingNumber != 3)
        {
            //３番をアクティブにする
            _startCount__3.SetActive(true);

            _displayingNumber = 3;

            return;
        }

        if (_countTime > 1 && _countTime <= 2 && _displayingNumber != 2)
        {
            //３番を非アクティブにする
            _startCount__3.SetActive(false);

            //２番をアクティブにする
            _startCount__2.SetActive(true);

            _displayingNumber = 2;

            return;
        }

        if (_countTime > 2 && _countTime <= 3 && _displayingNumber != 1)
        {
            //2番を非アクティブにする
            _startCount__2.SetActive(false);

            //1番をアクティブにする
            _startCount__1.SetActive(true);
            _displayingNumber = 1;

            return;
        }

        if (_countTime > 3)
        {
            //1番を非アクティブにする
            _startCount__1.SetActive(false);

            _displayingNumber = 0;
            _countTime = 0;
            isStarting = false;
            hasStarted = true;
        }

    }

}
