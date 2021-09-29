using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordClickBtn : MonoBehaviour
{
    public Text WordsTypeTxt;
    public string WordsType = "";
    public Button DeleteBtn;
    public bool Custom = false;
    public void Start()
    {
        PanelMgr.GetInstance().GetPanelFunction("CustomWordPanel").GetCustomWordPanel().Refresh();
    }
        public virtual void SetLocalWordsType(string s,bool c)
    {
        WordsType = s;
        WordsTypeTxt.text = s;
        Custom = c;
        DeleteBtn.gameObject.SetActive(false);
        if (Custom)
        {
            DeleteBtn.gameObject.SetActive(true);
           
        }
    }
    public virtual void SetWordsType()
    {
        GameMgr.GetInstance().SetWordType(WordsType, Custom);
        if (!Custom)
        {
            PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").GetGameLobbyPanel().OpenEnterMsgBox();
        }
        else
        {

            PanelMgr.GetInstance().GetPanelFunction("CustomWordPanel").GetCustomWordPanel().ShowMe();
            PanelMgr.GetInstance().GetPanelFunction("CustomWordPanel").GetCustomWordPanel().Refresh();
           


        }

        Debug.Log("Word Type Set: " + WordsType);
    }
    void DelayRefresh()
    {

        
    }
    public virtual void DeleteCustomType()
    {
        if (Custom)
        {
            GetDataScript.GetInstance().DeleteData(WordsType);
            PoolMgr.GetInstance().PushObj(gameObject);
        }
    }
}
