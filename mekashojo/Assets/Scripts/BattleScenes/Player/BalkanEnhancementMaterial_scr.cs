using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalkanEnhancementMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.BalkanEnhancementMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
