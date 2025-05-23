using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    ObjectManager _obj = new ObjectManager();
    ResourceManager _resource = new ResourceManager();
    MapManager _map = new MapManager();

    public static ObjectManager Object {get { return Instance._obj;}}
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static MapManager Map { get { return Instance._map; } }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }

}
