using UnityEngine;

namespace View
{
    public class Shield__Player : MonoBehaviour
    {
        [SerializeField, Header("HeavyShieldを入れる")] GameObject _heavyShield;
        [SerializeField, Header("LightShieldを入れる")] GameObject _lightShield;
        private GameObject _shield;

        // Start is called before the first frame update
        void Start()
        {
            // 選択中のシールドを処理する
            if (Model.EquipmentData.equipmentData.selectedShieldName == Model.EquipmentData.equipmentType.Shield__Heavy)
            {
                _shield = _heavyShield;
            }
            else
            {
                _shield = _lightShield;
            }

            // 初期化
            _heavyShield.SetActive(false);
            _lightShield.SetActive(false);
            transform.localScale = new Vector3(1, 1, 0);

            Controller.BattleScenesClassController.shield__Player.OnisUsingShieldChanged.AddListener((bool isUsingShield) =>
            {
                _shield.SetActive(isUsingShield);
            });

            Controller.BattleScenesClassController.shield__Player.OnShieldSizeChanged.AddListener((float shieldSize) =>
            {
                transform.localScale = new Vector3(shieldSize, shieldSize, 1);
            });
        }
    }
}
