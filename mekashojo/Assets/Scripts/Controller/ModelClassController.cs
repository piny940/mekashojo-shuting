using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class ModelClassController : MonoBehaviour
    {
        [SerializeField, Header("EnemyControlDataを入れる")] private Model.EnemyControlData _enemyControlData;

        private Model.WeaponInstances _weaponInstances;

        public static Model.PauseController pauseController;

        public static Model.PlayerStatusController playerStatusController;

        public static Model.PlayerPositionController playerPositionController;

        public static Model.Cannon__Player cannon__Player;

        public static Model.Missile__Player missile__Player;

        public static Model.WeaponManager weaponManager;

        public static Model.EnemyController enemyController;

        private void Awake()
        {
            pauseController = new Model.PauseController();

            playerStatusController = new Model.PlayerStatusController(pauseController);

            playerPositionController = new Model.PlayerPositionController(pauseController);

            cannon__Player = new Model.Cannon__Player(playerStatusController);

            missile__Player = new Model.Missile__Player(playerStatusController);

            _weaponInstances = new Model.WeaponInstances()
            {
                cannon__Player = cannon__Player,
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
