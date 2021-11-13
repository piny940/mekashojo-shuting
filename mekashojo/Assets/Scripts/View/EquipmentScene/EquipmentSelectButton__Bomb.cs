namespace View
{
    public class EquipmentSelectButton__Bomb : EquipmentSelectButtonBase
    {
        private void Start()
        {
            type = Model.EquipmentData.equipmentType.Bomb;
            Initialize();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SelectedWeaponChanged();
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
