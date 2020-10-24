using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : CropObject
{
    protected override void UpdateCropObject()
    {
        m_SpriteCrop.sprite = AtlasManager.Instance.GetSprite(kATLAS.CropObject, "tree_1_" + m_UserCropItemData.CURRENT_LEVEL);
    }

    protected override bool CanLevelUp()
    {
        return true;
    }
}
