using UnityEngine;

namespace Controller
{
    //武器のクラスのインスタンスを一つの構造体としてまとめておく
    public struct WeaponInstances
    {
        public Model.Cannon__Player cannon__Player;
        public Model.Laser__Player laser__Player;
        public Model.BeamMachineGun__Player beamMachineGun__Player;
        public Model.Balkan__Player balkan__Player;
        public Model.Missile__Player missile__Player;
    }

    public class ModelClassController : MonoBehaviour
    {
        [SerializeField, Header("EnemyControlDataを入れる")] private Model.EnemyControlData _enemyControlData;

        private WeaponInstances _weaponInstances;

        public static Model.PauseController pauseController;

        public static Model.PlayerStatusController playerStatusController;

        public static Model.PlayerPositionController playerPositionController;

        public static Model.Cannon__Player cannon__Player;

        public static Model.Laser__Player laser__Player;

        public static Model.BeamMachineGun__Player beamMachineGun__Player;

        public static Model.Balkan__Player balkan__Player;

        public static Model.Missile__Player missile__Player;

        public static Model.WeaponManager weaponManager;

        public static Model.EnemyController enemyController;

        private void Awake()
        {
            pauseController = new Model.PauseController();

            playerStatusController = new Model.PlayerStatusController(pauseController);

            playerPositionController = new Model.PlayerPositionController(pauseController);

            //TODO:ここ選ばれてない武器のクラスの分メモリが無駄やからswitch文使って無駄をなくす！
            cannon__Player = new Model.Cannon__Player(playerStatusController);

            laser__Player = new Model.Laser__Player(playerStatusController);

            beamMachineGun__Player = new Model.BeamMachineGun__Player(playerStatusController);

            balkan__Player = new Model.Balkan__Player(playerStatusController);

            missile__Player = new Model.Missile__Player(playerStatusController);

            _weaponInstances = new WeaponInstances()
            {
                cannon__Player = cannon__Player,
                laser__Player = laser__Player,
                beamMachineGun__Player = beamMachineGun__Player,
                balkan__Player = balkan__Player,
                missile__Player = missile__Player,
            };

            weaponManager = new Model.WeaponManager(pauseController, playerPositionController, _weaponInstances);

            enemyController = new Model.EnemyController(pauseController, _enemyControlData);
        }

        // Update is called once per frame
        void Update()
        {
            pauseController.CheckPausing();

            pauseController.StartCount();

            playerStatusController.ChargeEnergyAutomatically();

            playerPositionController.ChangeVelocity();

            weaponManager.SwitchWeapon();

            weaponManager.ProceedAttack();

            enemyController.CreateNewEnemy(_enemyControlData);
        }
    }
}
