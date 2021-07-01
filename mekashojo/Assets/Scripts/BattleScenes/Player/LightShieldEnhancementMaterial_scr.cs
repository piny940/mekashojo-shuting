using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShieldEnhancementMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.LightShieldEnhancementMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
