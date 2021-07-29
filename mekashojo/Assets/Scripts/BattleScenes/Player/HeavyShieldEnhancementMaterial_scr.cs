using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyShieldEnhancementMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.HeavyShieldEnhancementMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
