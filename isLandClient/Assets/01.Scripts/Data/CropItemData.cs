using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New CropItemData", menuName = "DataTable/CropData", order = 1)]
public class CropItemData : ScriptableObject
{
    public string CROP_ITEM_NAME;
    public int ITEM_CODE;
    public int MAX_LEVEL;
    public int[] LEVEL_UP_DELAY;
}