using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //セーブデータの削除
        PlayerPrefs.DeleteAll();

        SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);
    }
}
