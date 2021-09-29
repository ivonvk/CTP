using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WordIconsPanel : BasePanel
{

    private GameObject EnterMsgBox;
    private Button EnterBtn;
    private Button CancelBtn;
    private Button CloseBtn;

    private Button ClothesBtn;
    private Button FoodsBtn;
    private Button BuildingsBtn;
    private Button CarsBtn;
    private Button DrinksBtn;
    private Button ToolsBtn;
    private Button SeafoodsBtn;

    public override void Start()
    {
        base.Start();
        GetUI();
        AddFunctionUI();
        CloseEnterMsgBox();
    }
    public override void GetUI()
    {
        EnterMsgBox = GetCtr<Image>("EnterMsgBox").gameObject;
        EnterBtn = GetCtr<Button>("EnterBtn");
        CancelBtn = GetCtr<Button>("CancelBtn");
        CloseBtn = GetCtr<Button>("CloseBtn");

        ClothesBtn = GetCtr<Button>("ClothesBtn");
        FoodsBtn = GetCtr<Button>("FoodsBtn");
        BuildingsBtn = GetCtr<Button>("BuildingsBtn");
        CarsBtn = GetCtr<Button>("CarsBtn");
        DrinksBtn = GetCtr<Button>("DrinksBtn");
        ToolsBtn = GetCtr<Button>("ToolsBtn");
        SeafoodsBtn = GetCtr<Button>("SeafoodsBtn");
    }

    public override void AddFunctionUI()
    {
        AddFunction(EnterBtn.gameObject, EnterGame, Nothing, Nothing, Nothing);
        AddFunction(CancelBtn.gameObject, CloseEnterMsgBox, Nothing, Nothing, Nothing);

        AddFunction(CloseBtn.gameObject, HideMe, Nothing, Nothing, Nothing);

        AddFunction(ClothesBtn.gameObject, ClothesIcons, Nothing, Nothing, Nothing);
        AddFunction(FoodsBtn.gameObject, FoodsIcons, Nothing, Nothing, Nothing);

        

        AddFunction(BuildingsBtn.gameObject, BuildingsIcons, Nothing, Nothing, Nothing);
        AddFunction(CarsBtn.gameObject, CarsIcons, Nothing, Nothing, Nothing);
        AddFunction(DrinksBtn.gameObject, DrinksIcons, Nothing, Nothing, Nothing);
        AddFunction(ToolsBtn.gameObject, ToolsIcons, Nothing, Nothing, Nothing);
        AddFunction(SeafoodsBtn.gameObject, SeafoodsIcons, Nothing, Nothing, Nothing);



        //
        BuildingsBtn.gameObject.SetActive(false);
        CarsBtn.gameObject.SetActive(false);
        DrinksBtn.gameObject.SetActive(false);
        ToolsBtn.gameObject.SetActive(false);
        SeafoodsBtn.gameObject.SetActive(false);
    }
    void ClothesIcons()
    {
        OpenEnterMsgBox(true);
        GameMgr.GetInstance().SetWordType("Clothes",false);
    } void FoodsIcons()
    {
        OpenEnterMsgBox(true);
        GameMgr.GetInstance().SetWordType("Foods", false);
    } void BuildingsIcons()
    {
        OpenEnterMsgBox(true);

    } void CarsIcons()
    {
        OpenEnterMsgBox(true);

    } void DrinksIcons()
    {
        OpenEnterMsgBox(true);
    } void ToolsIcons()
    {
        OpenEnterMsgBox(true);

    } void SeafoodsIcons()
    {
        OpenEnterMsgBox(true);
    }
    void CloseEnterMsgBox()
    {
      
            EnterMsgBox.SetActive(false);
        
    } 
    public void OpenEnterMsgBox(bool b)
    {
        EnterMsgBox.SetActive(b);
    }
    void EnterGame()
    {
        Debug.Log("Enter the game...");
        ScenesMgr.GetInstance().LoadSceneAsync("GameScene", UpdateScene);
    }
    void UpdateScene()
    {
        PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").HideMe();
        CloseEnterMsgBox();
        gameObject.SetActive(false);
        MonoMgr.GetInstance().Reset();
    }

}
