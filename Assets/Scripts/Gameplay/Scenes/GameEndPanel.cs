using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : BasePanel
{
    private GameObject WinBox, LoseBox;
    private Button WinBtn, LoseBtn;


    public override void GetUI()
    {
        WinBox = GetCtr<Image>("WinBox").gameObject;
        LoseBox = GetCtr<Image>("LoseBox").gameObject;

        WinBtn = GetCtr<Button>("WinBtn");
        LoseBtn = GetCtr<Button>("LoseBtn");
    }
    public override void AddFunctionUI()
    {

        AddFunction(WinBtn.gameObject, BackToLobby, Nothing, Nothing, Nothing);
        AddFunction(LoseBtn.gameObject, BackToLobby, Nothing, Nothing, Nothing);
    }
    public void OnEnable()
    {
        base.Start();

        GetUI();
        AddFunctionUI();
       // DisableGameEndBox();
    }
    void BackToLobby()
    {
        Debug.Log("Back to lobby...");
        
        ScenesMgr.GetInstance().LoadSceneAsync("GameLobby", UpdateScene);
        
    }
    void UpdateScene()
    {


        PanelMgr.GetInstance().GetPanelFunction("WordIconsPanel").HideMe();
        DisableGameEndBox();
        HideMe();
        MonoMgr.GetInstance().Reset();
    }
   

    public void GameWin()
    {
        DisableGameEndBox();
        ShowMe();
        WinBox.SetActive(true);
    }
    public void GameLose()
    {
        DisableGameEndBox();
        ShowMe();
        LoseBox.SetActive(true);
    }
    public void DisableGameEndBox()
    {
        WinBox.SetActive(false);
        LoseBox.SetActive(false);
    }
    public override GameEndPanel GetGameEndPanel()
    {
        return this;
    }
}
