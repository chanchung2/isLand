using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CropObject), true)]
public class CropObjectEditor : Editor
{
    private List<string> m_ListItemInfo = new List<string>();
    private List<int> m_ListItemCode = new List<int>();

    private DataBase m_DataBase;

    private CropObject m_CropObject;

    private int m_SelectIndex = 0;

    void OnEnable()
    {
        m_CropObject = target as CropObject;

        if (m_DataBase == null)
        {
            m_DataBase = Resources.Load<ScriptableObject>("Data/DataBase") as DataBase;
        }

        m_ListItemInfo.Add("0 - None");
        m_ListItemCode.Add(0);
        foreach (var data in m_DataBase.CROP_OBJECT_DATA)
        {
            m_ListItemInfo.Add(string.Format("{0} - {1}", data.ITEM_CODE, data.CROP_OBJECT_NAME));
            m_ListItemCode.Add(data.ITEM_CODE);
        }

        var findIndex = m_ListItemCode.FindIndex(findData => findData == m_CropObject.UserCropItemData.ITEM_CODE);
        if (findIndex != -1)
        {
            m_SelectIndex = findIndex;            
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("ITEM_CODE");
        m_SelectIndex = EditorGUILayout.Popup(m_SelectIndex , m_ListItemInfo.ToArray());
        
        m_CropObject.UserCropItemData.ITEM_CODE = m_ListItemCode[m_SelectIndex];

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
