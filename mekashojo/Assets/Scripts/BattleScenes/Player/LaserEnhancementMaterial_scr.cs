using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnhancementMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.LaserEnhancementMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
