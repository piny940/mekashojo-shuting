using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBaseImp : MonoBehaviour
{
    // 1�x�{�^����������Ă���A���ɉ����\�ɂȂ�܂ł̎���(s)
    private const int BLOCK_TIME = 1;
    private float _elapsedTime = BLOCK_TIME;

    /// <summary>
    /// �{�^���������\���ǂ�����Ԃ�
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
    /// �{�^���̃^�C���A�E�g�����X�V����BUpdate()���\�b�h�Ŏ��s�����K�v������B
    /// </summary>
    protected void ButtonUpdate()
    {
        if (_elapsedTime < BLOCK_TIME)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
}
