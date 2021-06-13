using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnhancementMaterial_scr : DropMaterialBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        materialType = MaterialType.CannonEnhancementMaterial;

        Initialize();
    }

    private void Update()
    {
        Emerge();
    }

}
