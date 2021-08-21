using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class PlayerClassController : MonoBehaviour
    {
        public static Model.PlayerFire cannonFire;


        // Start is called before the first frame update
        void Start()
        {
            cannonFire = new Model.PlayerFire(ModelClassController.pauseController, false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
