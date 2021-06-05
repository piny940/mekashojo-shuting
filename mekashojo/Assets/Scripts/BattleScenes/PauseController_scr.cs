using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController_scr : MonoBehaviour
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("PauseScreenを入れる")] GameObject _pauseScreen;
    [SerializeField, Header("StartCountを入れる")] StartCount_scr _startCount;

    // Start is called before the first frame update
    void Start()
    {
        _pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_getInput.isEscapeKey && _startCount.hasStarted)
        {
            _startCount.hasStarted = false;
            _pauseScreen.SetActive(true);
        }
    }

    
}
