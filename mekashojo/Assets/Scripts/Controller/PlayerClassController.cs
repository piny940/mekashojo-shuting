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

    public class PlayerClassController : MonoBehaviour
    {
        public static Model.PlayerFire cannonFire;

        public static Model.PlayerFire laserFire;

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
            cannonFire = new Model.PlayerFire(ModelClassController.pauseController, false);

            laserFire = new Model.PlayerFire(ModelClassController.pauseController, false);
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
    }
}
