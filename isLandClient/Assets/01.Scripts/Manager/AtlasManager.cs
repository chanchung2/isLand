using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : Singleton<AtlasManager>
{
    private SpriteAtlas[] m_ArraySpriteAtlas;
    
    public void Initialization()
    {
        m_ArraySpriteAtlas = new SpriteAtlas[System.Enum.GetValues(typeof(kATLAS)).Length];

        for (int i = 0; i < m_ArraySpriteAtlas.Length; i++)
        {
            m_ArraySpriteAtlas[i] = Resources.Load<SpriteAtlas>(Constants.kATALS_PATHS[i]);
        }
    }

    public Sprite GetSprite(kATLAS atlas, string spriteName)
    {
        SpriteAtlas spriteAtlas = m_ArraySpriteAtlas[(int)atlas];

        return spriteAtlas.GetSprite(spriteName);
    }
}
