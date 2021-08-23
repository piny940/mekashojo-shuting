using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SelectedWeaponTexts : MonoBehaviour
    {
        // TODO: textとか名前つけてるけど実際は「Main」という文字が書かれた画像です。名前考えないといけない
        [SerializeField, Header("MainTextを入れる")] private GameObject _mainText;
        [SerializeField, Header("SubTextを入れる")] private GameObject _subText;

        private Image _mainTextImage;
        private Image _subTextImage;

        private const float UNSELECTED_WEAPON_TRANSPARENCY = 0.2f;

        private void Awake()
        {
            _mainTextImage = _mainText.GetComponent<Image>();
            _subTextImage = _subText.GetComponent<Image>();
        }

        // Start is called before the first frame update
        void Start()
        {
            // 初期化
            ChangeTextTransparncy(Controller.BattleScenesClassController.weaponManager.isMainSelected);

            Controller.BattleScenesClassController.weaponManager.OnWeaponSwitched.AddListener(ChangeTextTransparncy);
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
