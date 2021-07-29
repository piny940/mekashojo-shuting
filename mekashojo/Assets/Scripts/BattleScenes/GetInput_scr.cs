using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput_scr : MonoBehaviour
{
    [HideInInspector] public float horizontalKey { get { return Input.GetAxis("Horizontal"); } }

    [HideInInspector] public float verticalKey { get { return Input.GetAxis("Vertical"); } }

    [HideInInspector] public float bombKey { get { return Input.GetAxis("Jump"); } }

    [HideInInspector] public bool isMouseLeft { get { return Input.GetMouseButton(0); } }

    [HideInInspector] public bool isMouseRight { get { return Input.GetMouseButton(1); } }

    [HideInInspector] public bool isEscapeKey { get { return Input.GetKeyDown(KeyCode.Escape); } }

    [HideInInspector] public float mouseWheel { get { return Input.GetAxis("Mouse ScrollWheel"); } }

    [HideInInspector]
    public Vector3 mousePosition
    {
        get
        {
            Vector3 _mousePositionOnScreen = Input.mousePosition;
            Vector3 _mousePositionOnWorld = Camera.main.ScreenToWorldPoint(_mousePositionOnScreen);
            _mousePositionOnWorld.z = 0;
            return _mousePositionOnWorld;
        }
    }
}
