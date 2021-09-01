using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SelectedWeaponTexts : MonoBehaviour
    {
        [SerializeField, Header("MainTextImageを入れる")] private Image _mainTextImage;
        [SerializeField, Header("SubTextImageを入れる")] private Image _subTextImage;

        private const float UNSELECTED_WEAPON_TRANSPARENCY = 0.2f;

        // Start is called before the first frame update
        void Start()
        {
            // 初期化
            ChangeTextTransparncy(Controller.BattleScenesController.weaponManager.isMainSelected);

            Controller.BattleScenesController.weaponManager.OnIsMainSelectedChanged.AddListener(ChangeTextTransparncy);
        }

        private void ChangeTextTransparncy(bool isMainSelected)
        {
            if (isMainSelected)
            {
                _mainTextImage.color = new Color(1, 1, 1, 1);
                _subTextImage.color = new Color(1, 1, 1, UNSELECTED_WEAPON_TRANSPARENCY);
            }
            else
            {
                _mainTextImage.color = new Color(1, 1, 1, UNSELECTED_WEAPON_TRANSPARENCY);
                _subTextImage.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
