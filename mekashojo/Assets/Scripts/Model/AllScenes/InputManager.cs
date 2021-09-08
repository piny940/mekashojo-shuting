using UnityEngine;

namespace Model
{
    public static class InputManager
    {
        public static float horizontalKey { get { return Input.GetAxis("Horizontal"); } }

        public static float verticalKey { get { return Input.GetAxis("Vertical"); } }

        public static float bombKey { get { return Input.GetAxis("Jump"); } }

        public static bool isMouseLeft { get { return Input.GetMouseButton(0); } }

        public static bool isMouseRight { get { return Input.GetMouseButton(1); } }

        public static bool isEscapeKey { get { return Input.GetKeyDown(KeyCode.Escape); } }

        public static float mouseWheel { get { return Input.GetAxis("Mouse ScrollWheel"); } }

        public static Vector3 mousePosition
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
}
