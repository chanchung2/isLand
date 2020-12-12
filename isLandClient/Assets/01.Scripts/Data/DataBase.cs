using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTable", menuName = "DataTable/DataTable", order = 1)]
public class DataBase : ScriptableObject
{
    public List<CropObjectData> CROP_OBJECT_DATA;
    public List<ItemData> ITEM_DATA;
}