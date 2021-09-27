using UnityEngine;

namespace View
{
    public class EquipmentSelectButton__Laser : EquipmentSelectButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        private void Start()
        {
            type = Model.EquipmentData.equipmentType.MainWeapon__Laser;
            Initialize();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_pushSound);
                SelectedWeaponChanged();
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
