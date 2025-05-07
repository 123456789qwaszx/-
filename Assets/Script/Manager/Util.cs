using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;
        
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false) // recursive는 반환받았을시 아래 for문을 굳이 반복안하고 끝내기 위한 조건
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name) // go 산하의 child.name이  불일치해도 진행, 일치해도 진행.
                {
                    T component = transform.GetComponent<T>(); // 배열 T의 특정인덱스의 이름을 component로 받아오고
                    if (component != null) // 불일치 했을경우 반복, 일치했을경우 그값을 반환
                        return component;
                }
            }
		}
        else // 반복이 필요없는 경우. 그냥 recursive의 찌꺼기임.
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }


}
