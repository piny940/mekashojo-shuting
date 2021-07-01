using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyChargeMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.EnergyChargeMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
