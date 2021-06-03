using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput_scr : MonoBehaviour
{
    [HideInInspector] public float horizontalKey;
    [HideInInspector] public float verticalKey;
    [HideInInspector] public float bombKey;
    [HideInInspector] public bool isMouseLeft;
    [HideInInspector] public bool isMouseRight;
    [HideInInspector] public bool isEscapeKey;
    [HideInInspector] public Vector3 mousePosition;
    [HideInInspector] public float mouseWheel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //水平キーの取得
        horizontalKey = Input.GetAxis("Horizontal");
        //垂直キーの取得
        verticalKey = Input.GetAxis("Vertical");
        //ボムキーの取得
        bombKey = Input.GetAxis("Jump");
        //左クリックの取得（押されている間ずっとtrue)
        isMouseLeft = Input.GetMouseButton(0);
        //右クリックの取得（押されている間ずっとtrue)
        isMouseRight = Input.GetMouseButton(1);
        //マウスカーソルの座標の取得
        mousePosition = GetMousePosition();
        //エスケープキーの取得(押された瞬間のみtrue)
        isEscapeKey = Input.GetKeyDown(KeyCode.Escape);
        //マウスホイールの取得
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");
    }

    /// <summary>
    /// マウス座標の取得
    /// </summary>
    /// <returns></returns>
    Vector3 GetMousePosition()
    {
        Vector3 _mousePositionOnScreen = Input.mousePosition;
        Vector3 _mousePositionOnWorld = Camera.main.ScreenToWorldPoint(_mousePositionOnScreen);
        _mousePositionOnWorld.z = 0;
        return _mousePositionOnWorld;
    }


}
