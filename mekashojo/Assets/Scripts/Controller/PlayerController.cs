using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public struct PlayerBulletElements
    {
        public Model.PlayerFire playerFire;
        public GameObject bulletObject;
    }

    public struct DropMaterialElements
    {
        public Model.DropMaterialManager dropMaterialManager;
        public GameObject materialObject;
    }

    public class PlayerController : MonoBehaviour
    {
        public static Model.PlayerFire cannonFire;

        public static Model.PlayerFire laserFire;

        public static Model.BombFire__Player bombFire__Player;

        public static Dictionary<int, PlayerBulletElements> playerBulletTable;

        public static Dictionary<int, DropMaterialElements> dropMaterialTable;


        private void Awake()
        {
            playerBulletTable = new Dictionary<int, PlayerBulletElements>();

            dropMaterialTable = new Dictionary<int, DropMaterialElements>();
        }

        // Start is called before the first frame update
        void Start()
        {
            // 実行順序の関係でコンストラクタはStartに書かないといけない
            cannonFire
                = new Model.PlayerFire(
                    Vector3.zero,
                    BattleScenesController.playerDebuffManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.EquipmentData.equipmentType.MainWeapon__Cannon
                    );

            laserFire
                = new Model.PlayerFire(
                    Vector3.zero,
                    BattleScenesController.playerDebuffManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    Model.EquipmentData.equipmentType.MainWeapon__Laser
                    );

            bombFire__Player
                = new Model.BombFire__Player(
                    BattleScenesController.playerDebuffManager
                    );
        }

        // Update is called once per frame
        void Update()
        {
            foreach (PlayerBulletElements playerBulletElements in playerBulletTable.Values)
            {
                playerBulletElements.playerFire.RunEveryFrame(
                    playerBulletElements.bulletObject.gameObject.transform.position);
            }

            foreach (DropMaterialElements dropMaterialElements in dropMaterialTable.Values)
            {
                dropMaterialElements.dropMaterialManager.RunEveryFrame(
                    dropMaterialElements.materialObject.transform.position);
            }
        }

        public static int EmergePlayerBullet(Model.EquipmentData.equipmentType type, GameObject bulletObject, Vector3 initialVelocity)
        {
            Model.PlayerFire playerFire
                = new Model.PlayerFire(
                    initialVelocity,
                    BattleScenesController.playerDebuffManager,
                    BattleScenesController.enemyManager,
                    BattleScenesController.stageStatusManager,
                    type
                    );

            PlayerBulletElements playerBulletElements = new PlayerBulletElements()
            {
                playerFire = playerFire,
                bulletObject = bulletObject,
            };

            int id = IDManager.GetPlayerBulletID();

            playerBulletTable.Add(id, playerBulletElements);

            return id;
        }
    }
}
