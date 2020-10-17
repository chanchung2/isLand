using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour
{

    public void OnTouchMenu()
    {
        ObjectManager.Instance.CreateObject<UIMenu>(Constants.kPREFAB_UI_MENU);
    }
}
