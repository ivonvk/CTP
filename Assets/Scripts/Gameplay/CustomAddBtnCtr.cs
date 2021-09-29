using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAddBtnCtr : BasePanel
{
    public InputField CustomTextInput;
    private string WordType = "";
    Button DeleteBtn;

   public CustomWordPanel panel;
    public override void Start()
    {
        base.Start();
        CustomTextInput = GetCtr<InputField>("CustomTextInput");
        DeleteBtn = GetCtr<Button>("DeleteBtn");

       
        AddFunction(DeleteBtn.gameObject, Delete, Nothing, Nothing, Nothing);
    }
    public void SetPanel(CustomWordPanel custom)
    {
        panel = custom;
    }
    public string GetTargetWord()
    {
        return CustomTextInput.text;
    }
    public void SetTargetWord(string s)
    {


        CustomTextInput.text = s;
    }

    public string GetWordType()
    {
        return WordType;
    }
    public void SetWordType(string s)
    {
        WordType = s;

    }

    void Delete()
    {
        panel.RemoveCustomText(this);
        PoolMgr.GetInstance().PushObj(gameObject);
    }
}
