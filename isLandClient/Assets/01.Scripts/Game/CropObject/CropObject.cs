﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public abstract class CropObject : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider2D m_BoxColldier;
    [SerializeField]
    protected SpriteRenderer m_SpriteCrop;

    [SerializeField]
    protected UserCropObjectData m_UserCropItemData;   
    public UserCropObjectData UserCropItemData { get { return m_UserCropItemData ; } } 

    private float m_Time = 0;                           // 육성 시간.

    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            m_UserCropItemData.POS = this.transform.position;
        }
#endif
        // if (m_CurrentLevel >= m_MaxLevel)
        //     return ;

        // if (!CanLevelUp())
        //     return ;

        // m_Time += Time.deltaTime;
        // if (m_Time >= m_LevelUpDelay)
        // {
        //     m_Time -= m_LevelUpDelay;
        //     m_CurrentLevel++;

        //     UpdateSprite();
        // }
    }

    protected abstract void UpdateCropObject();
    protected abstract bool CanLevelUp();               // 성장 가능 여부.

    protected int GetCurrentLevel()
    {
        return m_UserCropItemData.CURRENT_LEVEL;
    }
}
