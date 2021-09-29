using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingleMgr<T>where T:new()
{
    [System.Serializable]
    public class TextData
    {

        public string WordType = "Test";
        public string[] text = new string[] { "Test" };
    }
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();

        return instance;
    }
   
    
}
