using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public enum kTAP_TYPE
    {
        Inventory = 0,
        Favor,
        Map,
        Maker,
        Setting,
    }

    [SerializeField]
    private Canvas[] m_ArrayCanvasMenuTap;

    private kTAP_TYPE m_CurrentTap = kTAP_TYPE.Inventory;

    private List<UIItemSlot> m_ListItemSlot = new List<UIItemSlot>();
    private const int kINVENTORY_SLOT_MAX_COUNT = 32;

    void Start()
    {
        UpdateUI();
    }

#region UpdateUI
    private void UpdateUI()
    {
        foreach (var canvas in m_ArrayCanvasMenuTap)
        {
            canvas.enabled = false;
        }

        m_ArrayCanvasMenuTap[(int)m_CurrentTap].enabled = true;

        switch (m_CurrentTap)
        {
            case kTAP_TYPE.Inventory : 
                UpdateInventory();
                break;
            case kTAP_TYPE.Favor :
                break;
            case kTAP_TYPE.Map :
                break;                
            case kTAP_TYPE.Maker :
                break;
            case kTAP_TYPE.Setting :
                break;
        }
    }

    private void UpdateInventory()
    {
        foreach (var obj in m_ListItemSlot)
        {
            ObjectManager.Instance.PushPoolObject<UIItemSlot>(obj.gameObject);
        }
        m_ListItemSlot.Clear();

        var parentPos = m_ArrayCanvasMenuTap[(int)m_CurrentTap].transform;
        for (int i = 0; i < kINVENTORY_SLOT_MAX_COUNT; i++)
        {
            var obj = ObjectManager.Instance.PopPoolObject<UIItemSlot>(parentPos);
            
            m_ListItemSlot.Add(obj);
        }
    }
#endregion

#region TouchEvent
    public void OnTouchInventory()
    {
        if (m_CurrentTap == kTAP_TYPE.Inventory)
            return ;
        
        m_CurrentTap = kTAP_TYPE.Inventory;
        UpdateUI();
    }

    public void OnTouchFavor()
    {
         if (m_CurrentTap == kTAP_TYPE.Favor)
            return ;
        
        m_CurrentTap = kTAP_TYPE.Favor;
        UpdateUI();       
    }
    public void OnTouchMap()
    {
        if (m_CurrentTap == kTAP_TYPE.Map)
            return ;
        
        m_CurrentTap = kTAP_TYPE.Map;
        UpdateUI();
    }
    public void OnTouchMaker()
    {
        if (m_CurrentTap == kTAP_TYPE.Maker)
            return ;
        
        m_CurrentTap = kTAP_TYPE.Maker;
        UpdateUI();
    }
    public void OnTouchSetting()
    {
        if (m_CurrentTap == kTAP_TYPE.Setting)
            return ;
        
        m_CurrentTap = kTAP_TYPE.Setting;
        UpdateUI();
    }
#endregion
}
