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
    [HideInInspector] public bool escapeKey;
    [HideInInspector] public Vector3 mousePosition;

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
        mousePosition = Input.mousePosition;    //ワールド座標に座標変換する必要がある！！
        //エスケープキーの取得(押された瞬間のみtrue)
        escapeKey = Input.GetKeyDown(KeyCode.Escape);
        
    }
}
