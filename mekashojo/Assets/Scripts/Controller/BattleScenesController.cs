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
        [SerializeField, Header("EnemyControlDataを入れる")] private StageSettings _stageSettings;

        private WeaponInstances _weaponInstances;

        public static StageSettings stageSettings;

        public static Model.StageStatusManager stageStatusManager;

        public static Model.PlayerDebuffManager playerDebuffManager;

        public static Model.Shield__Player shield__Player;

        public static Model.PlayerStatusManager playerStatusManager;

        public static Model.PlayerPositionManager playerPositionManager;

        public static Model.Cannon__Player cannon__Player;

        public static Model.Laser__Player laser__Player;

        public static Model.BeamMachineGun__Player beamMachineGun__Player;

        public static Model.Balkan__Player balkan__Player;

        public static Model.Missile__Player missile__Player;

        public static Model.Bomb__Player bomb__Player;

        public static Model.WeaponManager weaponManager;

        public static Model.EnemyManager enemyManager;

        public static Model.AcquiredEnhancementMaterialData acquiredEnhancementMaterialData;

        private void Awake()
        {
            stageSettings = _stageSettings;

            stageStatusManager = new Model.StageStatusManager();

            playerDebuffManager = new Model.PlayerDebuffManager(stageStatusManager);

            shield__Player = new Model.Shield__Player(stageStatusManager);

            enemyManager = new Model.EnemyManager(stageStatusManager, _stageSettings);

            playerStatusManager = new Model.PlayerStatusManager(playerDebuffManager, shield__Player, stageStatusManager);

            playerPositionManager = new Model.PlayerPositionManager(shield__Player, playerDebuffManager, enemyManager, stageStatusManager);

            cannon__Player = new Model.Cannon__Player(playerStatusManager);

            laser__Player = new Model.Laser__Player(playerStatusManager);

            beamMachineGun__Player = new Model.BeamMachineGun__Player(playerStatusManager);

            balkan__Player = new Model.Balkan__Player(playerStatusManager);

            missile__Player = new Model.Missile__Player(playerStatusManager);

            bomb__Player = new Model.Bomb__Player(playerDebuffManager, playerStatusManager, stageStatusManager);

            _weaponInstances = new WeaponInstances()
            {
                cannon__Player = cannon__Player,
                laser__Player = laser__Player,
                beamMachineGun__Player = beamMachineGun__Player,
                balkan__Player = balkan__Player,
                missile__Player = missile__Player,
            };

            weaponManager = new Model.WeaponManager(stageStatusManager, playerDebuffManager, _weaponInstances);

            acquiredEnhancementMaterialData = new Model.AcquiredEnhancementMaterialData();
        }

        // Update is called once per frame
        void Update()
        {
            stageStatusManager.RunEveryFrame();

            playerDebuffManager.RunEveryFrame();

            shield__Player.RunEveryFrame();

            playerStatusManager.RunEveryFrame();

            playerPositionManager.RunEveryFrame();

            weaponManager.RunEveryFrame();

            bomb__Player.RunEveryFrame();

            enemyManager.RunEveryFrame(_stageSettings);
        }
    }
}
