using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeClickBtn : WordClickBtn
{
    public override void SetLocalWordsType(string s,bool c)
    {
        WordsType = s;
        WordsTypeTxt.text = s;
    }
    public override void SetWordsType()
    {

         PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").GetGameLobbyPanel().OpenStageBox(WordsType);

       // PanelMgr.GetInstance().GetPanelFunction("CustomWordPanel").GetCustomWordPanel().ShowMe();
        Debug.Log("Word Type Set: " + WordsType);
    }
}
