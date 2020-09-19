using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{  
    void Start()
    {
        ObjectManager.Instance.CreateObject<Player>(Constants.kPREFAB_PLAYER);
    }
}
