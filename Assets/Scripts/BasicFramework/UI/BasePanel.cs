using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板基类
/// 1.显示和隐藏自己
/// 2.能够自动找到该面板下的对象子控件，并且存储起来
/// 3.能够获得指定对象的控件
/// </summary>
/// 
public delegate void PlayerBtnDown();
public delegate void PlayerBtnUp();
public delegate void PlayerBtnEnter();
public delegate void PlayerBtnExit();
public class BasePanel : MonoBehaviour
{
    [HideInInspector] private BtnEventTrigger Go, To, Your, Panel, Script;
    [HideInInspector] private BtnEventTrigger Then, Create, The, BtnEventTrigger;
    [HideInInspector] private BtnEventTrigger Override, With, CreatedBtn, to, the, Function;
    private Dictionary<string, List<UIBehaviour>> ctrDic = new Dictionary<string, List<UIBehaviour>>();

    public virtual void Start()
    {
        FindChildrenCtr<Scrollbar>();
        FindChildrenCtr<Button>();
        FindChildrenCtr<Image>();
        FindChildrenCtr<RawImage>();
        FindChildrenCtr<Text>();
        FindChildrenCtr<Slider>();
        FindChildrenCtr<ScrollRect>();
        FindChildrenCtr<InputField>();
    }

    public virtual void OnDrawGizmos()
    {

        FindChildrenCtr<Scrollbar>();
        FindChildrenCtr<Button>();
        FindChildrenCtr<Image>();
        FindChildrenCtr<RawImage>();
        FindChildrenCtr<Text>();
        FindChildrenCtr<Slider>();
        FindChildrenCtr<ScrollRect>();
        FindChildrenCtr<InputField>();
    }
    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }
    public virtual void ResetSaveData()
    {
       
    }
    public virtual void DisplayAllSavedFiles()
    {
    }
    public virtual void ResetPanel()
    {

    }
    public virtual GameLobbyPanel GetGameLobbyPanel()
    {
        return null;
    }

    public virtual AlphaTypingPanel GetAlphaTyping()
    {
        return null;
    }
    public virtual GameEndPanel GetGameEndPanel()
    {
        return null;
    }public virtual CustomWordPanel GetCustomWordPanel()
    {
        return null;
    }
    public virtual void Nothing()
    {

    }
    public virtual void GetUI()
    {
    }
    public virtual void AddFunctionUI()
    {

    } 
    public virtual void AddFunction(GameObject obj, PlayerBtnDown down, PlayerBtnEnter enter, PlayerBtnUp up, PlayerBtnExit exit)
    {
        BtnEventTrigger b = obj.AddComponent<BtnEventTrigger>();
        b.BtnDownMethodToCall = down;
        b.BtnEnterMethodToCall = enter;
        b.BtnUpMethodToCall = up;
        b.BtnExitMethodToCall = exit;
    }
    public virtual void SetupCheck()
    {
        HideMe();
    }
    /// <summary>
    /// 获取指定控件
    /// </summary>
    /// <typeparam name="T">返回控件类型</typeparam>
    /// <param name="name">对象名</param>
    /// <returns></returns>
    public T GetCtr<T>(string name) where T : UIBehaviour
    {
        if (!ctrDic.ContainsKey(name))
            return null;

        for (int i = 0; i < ctrDic[name].Count; i++)
        {
            if (ctrDic[name][i] is T)
                return ctrDic[name][i] as T;
        }

        return null;
    }

    /// <summary>
    /// 找到所有子对象的指定控件
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    private void FindChildrenCtr<T>() where T : UIBehaviour
    {
        T[] array = this.GetComponentsInChildren<T>();

        for (int i = 0; i < array.Length; i++)
        {
            if (!ctrDic.ContainsKey(array[i].gameObject.name))
                ctrDic.Add(array[i].gameObject.name, new List<UIBehaviour> { array[i] });
            else
                ctrDic[array[i].gameObject.name].Add(array[i]);
        }
    }
}
