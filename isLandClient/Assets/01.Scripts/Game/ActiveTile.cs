using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_SpriteActiveTile;

    void Start()
    {
        m_SpriteActiveTile.enabled = false;

        var player = ObjectManager.Instance.GetObject<Player>();
        player.OnActiveButtonPress += OnActiveButtonPress;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag != kTAG.ColliderObject.ToString())
            return ;

        const string kACTIVE_OFF_SPRITE_NAME = "off";
        m_SpriteActiveTile.sprite = AtlasManager.Instance.GetSprite(kATLAS.Tile, kACTIVE_OFF_SPRITE_NAME);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != kTAG.ColliderObject.ToString())
            return ;
            
        const string kACTIVE_ON_SPRITE_NAME = "on";
        m_SpriteActiveTile.sprite = AtlasManager.Instance.GetSprite(kATLAS.Tile, kACTIVE_ON_SPRITE_NAME);            
    }

    private void OnActiveButtonPress(bool isPress, Vector3 startPos)
    {
        m_SpriteActiveTile.enabled = isPress;

        if (isPress)
        {
            this.transform.position = startPos;
        }
    }
}
