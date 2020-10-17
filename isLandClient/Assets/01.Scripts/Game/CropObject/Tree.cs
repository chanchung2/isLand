using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : CropObject
{
    protected override void UpdateSprite()
    {
        m_SpriteCrop.sprite = AtlasManager.Instance.GetSprite(kATLAS.CropObject, "tree_1_" + m_CurrentLevel);
    }

    protected override bool CanLevelUp()
    {
        return true;
    }
}
