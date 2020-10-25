using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SaveCropObjectData
{
    public List<UserCropObjectData> LIST_CROP_OBJECT = new List<UserCropObjectData>();

    public void Add(UserCropObjectData data)
    {
        LIST_CROP_OBJECT.Add(data);
    }

    public void Clear()
    {
        LIST_CROP_OBJECT.Clear();
    }

    public List<UserCropObjectData> GetDatas()
    {
        return LIST_CROP_OBJECT;
    }
}

public class SceneObjectEditor : EditorWindow
{
    private GameObject m_ObjectTileMap;
    private SaveCropObjectData m_SaveCropObjectData = new SaveCropObjectData();

    [MenuItem("isLandEditor/SceneObjectEditor")]
    public static void CreateWindow()
    {
        GetWindow<SceneObjectEditor>().Show();
    }

    void OnEnable()
    {
        m_ObjectTileMap = GameObject.Find("ObjectTileMap"); 
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("ObjectTileMap");
        m_ObjectTileMap = EditorGUILayout.ObjectField(m_ObjectTileMap, typeof(GameObject), true) as GameObject;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            m_SaveCropObjectData.Clear();

            var allChildren = m_ObjectTileMap.GetComponentsInChildren<Transform>();
            var listDestoryObj = new List<GameObject>();
            foreach (var transform in allChildren)
            {
                var obj = transform.gameObject;

                var cropObject = obj.GetComponent<CropObject>();
                if (cropObject == null)
                    continue;

                // 저장.
                m_SaveCropObjectData.Add(cropObject.UserCropItemData);     
                listDestoryObj.Add(obj);        
            }

            foreach (var obj in listDestoryObj)
            {
                DestroyImmediate(obj);
            }

            Debug.Log("SaveSuccess");

            DataManager.Instance.SaveData<SaveCropObjectData>(m_SaveCropObjectData);
        }
        if (GUILayout.Button("Load"))
        {
            m_SaveCropObjectData.Clear();
            m_SaveCropObjectData = DataManager.Instance.LoadData<SaveCropObjectData>();

            DataManager.Instance.Initialization();
            foreach (var data in m_SaveCropObjectData.GetDatas())
            {
                var objectName = DataManager.Instance.GetCropObjectName(data.ITEM_CODE);
                
                string prefabPath = string.Format("Prefabs/CropObject/{0}", objectName);
                var prefab = Resources.Load(prefabPath);

                var obj = Instantiate(prefab, data.POS, Quaternion.identity) as GameObject;
                obj.transform.parent = m_ObjectTileMap.transform;
            }

            Debug.Log("LoadSuccess");
        }        
        EditorGUILayout.EndHorizontal();
    }
}
