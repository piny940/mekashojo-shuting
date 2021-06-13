using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnhancementMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.BombEnhancementMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }
}
