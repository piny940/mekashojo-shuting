using UnityEngine;

namespace View
{
    public class EquipmentSelectButton__Missile : EquipmentSelectButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        private void Start()
        {
            type = Model.EquipmentData.equipmentType.SubWeapon__Missile;
            Initialize();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                SelectedWeaponChanged();
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
