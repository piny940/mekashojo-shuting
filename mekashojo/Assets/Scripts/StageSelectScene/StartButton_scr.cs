using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton_scr : ButtonBaseImp
{
    private bool _hasNotSelected = true;

    private string _selectingStageName;
    public string selectingStageName
    {
        private get { return this._selectingStageName; }
        set
        {
            if (_hasNotSelected)
            {
                this.isActive = true;
                _hasNotSelected = false;
            }
            this._selectingStageName = value;
        }
    }

    public bool isActive
    {
        get { return GetComponent<Button>().interactable; }
        set { GetComponent<Button>().interactable = value; }
    }

    private void Start()
    {
        this.isActive = false;
    }

    public void OnPush()
    {
        if (CanPush())
        {
            SceneManager.LoadScene(selectingStageName);
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
