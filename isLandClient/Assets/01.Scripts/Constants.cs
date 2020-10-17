using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum kTAG
{
    SoilTile,
    ColliderObject,
}

public enum kATLAS
{
    Character = 0,
    Tile,
    UI,
}

public static class Constants
{
    public static readonly Vector3 kTILE_PIVOT_POS = new Vector3(0.5f, 0.5f, 0.5f);

    public static readonly string[] kATALS_PATHS = {"Atlas/Character", "Atlas/Tile", "Atlas/UI"};

    #region Prefab Path.
    
    public static readonly string kPREFAB_PLAYER = "Prefabs/Player";
    public static readonly string kPREFAB_ATIVE_TILE = "Prefabs/ActiveTile";

    public static readonly string kPREFAB_UI_MAIN = "Prefabs/UI/UIMain";
    public static readonly string kPREFAB_UI_MENU = "Prefabs/UI/UIMenu";

    public static readonly string kPREFAB_UI_ITEM_SLOT = "Prefabs/UI/UIItemSlot";

    #endregion
}
