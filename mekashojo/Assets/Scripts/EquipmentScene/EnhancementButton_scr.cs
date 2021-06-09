using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementButton_scr : ButtonBaseImp
{
    /// <summary>
    /// �{�^���������\�ł��邩�ǂ���������
    /// </summary>
    public bool isActive
    {
        get { return GetComponent<Button>().interactable; }
        set { GetComponent<Button>().interactable = value; }
    }

    /// <summary>
    /// �{�^�����\������Ă��邩�ǂ���������
    /// </summary>
    public bool isVisible
    {
        get { return this.gameObject.activeSelf; }
        set { this.gameObject.SetActive(value); }
    }

    /// <summary>
    /// �{�^�����������ꂽ���̃A�N�V����
    /// </summary>
    public Action EnhanceAction;

    public void OnPush()
    {
        if (CanPush())
        {
            EnhanceAction();
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
