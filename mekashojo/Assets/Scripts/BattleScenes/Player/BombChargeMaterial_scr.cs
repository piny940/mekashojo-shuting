using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChargeMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.BombChargeMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
