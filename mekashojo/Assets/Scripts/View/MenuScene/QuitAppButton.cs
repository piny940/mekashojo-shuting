using UnityEngine;

namespace View
{
    public class QuitAppButton : ButtonBase
    {
        public void OnPush()
        {
            if (CanPush())
            {
                Application.Quit();
            }
        }

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }
    }
}
