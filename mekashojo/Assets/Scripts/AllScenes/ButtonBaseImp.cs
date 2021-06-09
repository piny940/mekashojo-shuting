using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBaseImp : MonoBehaviour
{
    private const int BLOCK_TIME = 1;
    private float _elapsedTime = BLOCK_TIME;

    protected bool CanPush()
    {
        if (_elapsedTime >= BLOCK_TIME)
        {
            _elapsedTime = 0;
            return true;
        }

        return false;
    }

    protected void ButtonUpdate()
    {
        if (_elapsedTime < BLOCK_TIME)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
}
