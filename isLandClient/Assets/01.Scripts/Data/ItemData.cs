using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "DataTable/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public int ITEM_CODE;
    public string ITEM_NAME;
}
