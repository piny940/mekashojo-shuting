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

    public class BattleScenesController : MonoBehaviour
    {
        [SerializeField, Header("EnemyControlDataを入れる")] private EnemyControlData _enemyControlData;

        private WeaponInstances _weaponInstances;

        public static Model.PauseManager pauseManager;

        public static Model.PlayerStatusManager playerStatusManager;

        public static Model.PlayerPositionManager playerPositionManager;

        public static Model.Cannon__Player cannon__Player;

        public static Model.Laser__Player laser__Player;

        public static Model.BeamMachineGun__Player beamMachineGun__Player;

        public static Model.Balkan__Player balkan__Player;

        public static Model.Missile__Player missile__Player;

        public static Model.Bomb__Player bomb__Player;

        public static Model.Shield__Player shield__Player;

        public static Model.WeaponManager weaponManager;

        public static Model.EnemyManager enemyManager;

        public static Model.AcquiredEnhancementMaterialData acquiredEnhancementMaterialData;

        private void Awake()
        {
            pauseManager = new Model.PauseManager();

            shield__Player = new Model.Shield__Player(pauseManager);

            enemyManager = new Model.EnemyManager(pauseManager, _enemyControlData);

            playerStatusManager = new Model.PlayerStatusManager(shield__Player, pauseManager);

            playerPositionManager = new Model.PlayerPositionManager(shield__Player, enemyManager, pauseManager);

            cannon__Player = new Model.Cannon__Player(playerStatusManager);

            laser__Player = new Model.Laser__Player(playerStatusManager);

            beamMachineGun__Player = new Model.BeamMachineGun__Player(playerStatusManager);

            balkan__Player = new Model.Balkan__Player(playerStatusManager);

            missile__Player = new Model.Missile__Player(playerStatusManager);

            bomb__Player = new Model.Bomb__Player(playerStatusManager, playerPositionManager, pauseManager);

            _weaponInstances = new WeaponInstances()
            {
                cannon__Player = cannon__Player,
                laser__Player = laser__Player,
                beamMachineGun__Player = beamMachineGun__Player,
                balkan__Player = balkan__Player,
                missile__Player = missile__Player,
            };

            weaponManager = new Model.WeaponManager(pauseManager, playerPositionManager, _weaponInstances);

            acquiredEnhancementMaterialData = new Model.AcquiredEnhancementMaterialData();
        }

        // Update is called once per frame
        void Update()
        {
            pauseManager.RunEveryFrame();

            shield__Player.RunEveryFrame();

            playerStatusManager.RunEveryFrame();

            playerPositionManager.RunEveryFrame();

            weaponManager.RunEveryFrame();

            bomb__Player.RunEveryFrame();

            enemyManager.RunEveryFrame(_enemyControlData);
        }
    }
}
