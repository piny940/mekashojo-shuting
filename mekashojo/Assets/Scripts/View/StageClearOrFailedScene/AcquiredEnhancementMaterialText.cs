using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class AcquiredEnhancementMaterialText : MonoBehaviour
    {
        // 入手した強化用素材の数
        private Dictionary<Model.EquipmentData.equipmentType, int> _acquiredEnhancementMaterialsCount;


        // Start is called before the first frame update
        void Start()
        {
            _acquiredEnhancementMaterialsCount = Controller.BattleScenesController.acquiredEnhancementMaterialData.acquiredEnhancementMaterialsCount;

            //Textの内容を更新する（未実装）
        }
    }
}
