using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{  
    void Awake()
    {
        DataManager.Instance.Initialization();
    }

    void Start()
    {
        ObjectManager.Instance.CreateObject<UIMain>(Constants.kPREFAB_UI_MAIN);
        ObjectManager.Instance.CreateObject<Player>(Constants.kPREFAB_PLAYER);
    }
}
