using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneObjectEditor : EditorWindow
{
    private GameObject m_ObjectTileMap;

    [MenuItem("isLandEditor/CropObject")]
    public static void Init()
    {
        GetWindow<SceneObjectEditor>().Show();
    }

    void OnEnable()
    {

    }

    void OnGUI()
    {
        // m_ObjectTileMap = EditorGUILayout.ObjectField("Object", m_ObjectTileMap);

        if (GUILayout.Button("asdasd"))
        {
            Debug.Log("asdasd");
        }
    }
}
