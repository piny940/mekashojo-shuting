using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton_scr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPush()
    {
        PlayerPrefs.DeleteAll();
        SaveDataManager_scr.saveDataManager.SaveData();
        SceneManager.LoadScene("MenuScene");
    }
}
