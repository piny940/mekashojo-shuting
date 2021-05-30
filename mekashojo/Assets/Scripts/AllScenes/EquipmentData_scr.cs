using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData_scr : MonoBehaviour
{
    public static EquipmentData_scr equipmentData = null;


    //シングルトン
    private void Awake()
    {
        if (equipmentData == null)
        {
            equipmentData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    
}
