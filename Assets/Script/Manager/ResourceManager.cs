using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //그냥 유니티내장함수로만 쓰니 헷갈려서, 자주 쓰는 기능들 선반에 꺼내둔다는 느낌으로 랩핑함.  
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
        return;

        Object.Destroy(go);
    }

}
