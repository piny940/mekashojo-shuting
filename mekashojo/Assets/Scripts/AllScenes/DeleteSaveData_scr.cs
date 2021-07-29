using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSaveData_scr : MonoBehaviour
{
    //このクラスは「開発中に一度でもNewGameボタンを押してしまうとそれ以降Continueを押してもセーブデータがないよ画面を見ることができない現象」を解決するためのクラスであり、
    //ゲームをビルドまたは公開する前には必ず削除するようにしてください
    //データを削除する方法は、タイトル画面で「delete save data」と打つことです

    string _input = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (Input.anyKeyDown)
        {
            _input += Input.inputString;

            Debug.Log(_input);
        }

        if (_input =="delete save data")
        {
            _input = "";

            PlayerPrefs.DeleteKey("SaveData");

            Debug.Log("Deleted!");
        }
    }
#endif
}
