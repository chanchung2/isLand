using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTable", menuName = "DataTable/DataTable", order = 1)]
public class DataTable : ScriptableObject
{
    public List<CropItemData> CROP_ITEM_DATA;
}