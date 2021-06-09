using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBaseImp : MonoBehaviour
{
    // 1度ボタンが押されてから、次に押下可能になるまでの時間(s)
    private const int BLOCK_TIME = 1;
    private float _elapsedTime = BLOCK_TIME;

    /// <summary>
    /// ボタンが押下可能かどうかを返す
    /// </summary>
    /// <returns></returns>
    protected bool CanPush()
    {
        if (_elapsedTime >= BLOCK_TIME)
        {
            _elapsedTime = 0;
            return true;
        }

        return false;
    }

    /// <summary>
    /// ボタンのタイムアウト情報を更新する。Update()メソッドで実行される必要がある。
    /// </summary>
    protected void ButtonUpdate()
    {
        if (_elapsedTime < BLOCK_TIME)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
}
