using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementMaterialBaseIMP : MonoBehaviour
{
    protected EquipmentData_scr.equipmentType weaponType;

    

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player_BattleScene.ToString())
        {

        }
    }

    void PickedUp(EquipmentData_scr.equipmentType equipmentType)
    {
        switch (equipmentType)
        {
            

        }
    }
}
