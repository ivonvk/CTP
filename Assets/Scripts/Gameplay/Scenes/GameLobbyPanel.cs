using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GameLobbyPanel : BasePanel
{

    private GameObject EnterMsgBox;
    private Button EnterBtn;
    private Button CancelBtn;

    private Button CustomStageCloseBtn;
    private Button DefaultStageCloseBtn;
    private Button StageTypeListCloseBtn;

    private Text PlayerNameTxt;
    private Text MasterLevelTxt;
    private Text EXPTxt;
    private Text ActionPointTxt;

    private ScrollRect DefaultStageBox;
    private ScrollRect CustomStageBox;
    private ScrollRect StageTypeListBox;



    private Button StageBtn;
    private Button HeroBtn;
    private Button HouseBtn;

    private Button SwitchToIconsBtn;
    private Button RandomStageBtn;

    private Button CustomBtn;
    private GameObject[] CustomAddBtn = new GameObject[100];

    private GameObject TopUI;

    public override void GetUI()
    {
        DefaultStageBox = GetCtr<ScrollRect>("DefaultStageBox");
        CustomStageBox = GetCtr<ScrollRect>("CustomStageBox");
        StageTypeListBox = GetCtr<ScrollRect>("StageTypeListBox");

        EnterMsgBox = GetCtr<Image>("EnterMsgBox").gameObject;

        EnterBtn = GetCtr<Button>("EnterBtn");
        CancelBtn = GetCtr<Button>("CancelBtn");

        CustomStageCloseBtn = GetCtr<Button>("CustomStageCloseBtn");
        DefaultStageCloseBtn = GetCtr<Button>("DefaultStageCloseBtn");
        StageTypeListCloseBtn = GetCtr<Button>("StageTypeListCloseBtn");
        SwitchToIconsBtn = GetCtr<Button>("SwitchToIconsBtn");
        RandomStageBtn = GetCtr<Button>("RandomStageBtn");

        CustomBtn = GetCtr<Button>("CustomBtn");



        PlayerNameTxt = GetCtr<Text>("PlayerNameTxt");
        MasterLevelTxt = GetCtr<Text>("MasterLevelTxt");
        EXPTxt = GetCtr<Text>("EXPTxt");
        ActionPointTxt = GetCtr<Text>("ActionPointTxt");

        StageBtn = GetCtr<Button>("StageBtn");
        HeroBtn = GetCtr<Button>("HeroBtn");
        HouseBtn = GetCtr<Button>("HouseBtn");

        TopUI = GetCtr<Image>("TopUI").gameObject;


        PlayerNameTxt.text = PlayerMgr.GetInstance().GetPlayerName();
        MasterLevelTxt.text = "MLevels: " + PlayerMgr.GetInstance().GetMasterLevel().ToString();
        EXPTxt.text = "EXP: " + PlayerMgr.GetInstance().GetCurrentEXP().ToString() + "/" + PlayerMgr.GetInstance().GetMaxEXP().ToString();
        ActionPointTxt.text = "AP: " + PlayerMgr.GetInstance().GetActionPoint().ToString();
    }

    public override void AddFunctionUI()
    {
        AddFunction(EnterBtn.gameObject, EnterGame, Nothing, Nothing, Nothing);
        AddFunction(CancelBtn.gameObject, CloseEnterMsgBox, Nothing, Nothing, Nothing);
        AddFunction(StageBtn.gameObject, OpenStageTypeListBox, Nothing, Nothing, Nothing);
        AddFunction(HeroBtn.gameObject, CloseBoxes, Nothing, Nothing, Nothing);
        AddFunction(HouseBtn.gameObject, CloseBoxes, Nothing, Nothing, Nothing);

        AddFunction(CustomStageCloseBtn.gameObject, CloseCustomStage, Nothing, Nothing, Nothing);
        AddFunction(DefaultStageCloseBtn.gameObject, CloseDefaultStage, Nothing, Nothing, Nothing);
        AddFunction(StageTypeListCloseBtn.gameObject, CloseStageTypeList, Nothing, Nothing, Nothing);





        AddFunction(SwitchToIconsBtn.gameObject, OpenWordIconsPanel, Nothing, Nothing, Nothing);
        AddFunction(RandomStageBtn.gameObject, SetRandomStage, Nothing, Nothing, Nothing);

        AddFunction(CustomBtn.gameObject, OpenCustomPanel, Nothing, Nothing, Nothing);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateCustomTexts();
        }
    }
    public override void Start()
    {
        base.Start();

        GetUI();
        AddFunctionUI();
        RandomStageBtn.gameObject.SetActive(false);
        SwitchToIconsBtn.gameObject.SetActive(false);

        HeroBtn.gameObject.SetActive(false);
        HouseBtn.gameObject.SetActive(false);
        TopUI.gameObject.SetActive(false);
        


        UpdateCustomTexts();


        TextAsset[] text = Resources.LoadAll<TextAsset>("Text");
        for (int i = 0; i < text.Length; i++)
        {
            PoolMgr.GetInstance().GetObj("UI/Panel/WordClickBtn", (o) =>
            {
                o.transform.SetParent(DefaultStageBox.content);
                DefaultStageBox.content.sizeDelta = new Vector2(DefaultStageBox.content.sizeDelta.x, DefaultStageBox.content.sizeDelta.y + o.GetComponent<RectTransform>().sizeDelta.y + DefaultStageBox.content.GetComponent<GridLayoutGroup>().spacing.y);
                o.GetComponent<WordClickBtn>().SetLocalWordsType(text[i].name, false);
            });
        }
        PoolMgr.GetInstance().GetObj("UI/Panel/TypeClickBtn", (o) =>
        {
            o.transform.SetParent(StageTypeListBox.content);
            StageTypeListBox.content.sizeDelta = new Vector2(StageTypeListBox.content.sizeDelta.x, StageTypeListBox.content.sizeDelta.y + o.GetComponent<RectTransform>().sizeDelta.y + StageTypeListBox.content.GetComponent<GridLayoutGroup>().spacing.y);
            o.GetComponent<WordClickBtn>().SetLocalWordsType("Default Stage", false);
        });
        PoolMgr.GetInstance().GetObj("UI/Panel/TypeClickBtn", (o) =>
        {
            o.transform.SetParent(StageTypeListBox.content);
            StageTypeListBox.content.sizeDelta = new Vector2(StageTypeListBox.content.sizeDelta.x, StageTypeListBox.content.sizeDelta.y + o.GetComponent<RectTransform>().sizeDelta.y + StageTypeListBox.content.GetComponent<GridLayoutGroup>().spacing.y);
            o.GetComponent<WordClickBtn>().SetLocalWordsType("Custom Stage", true);
        });

        CloseBoxes();
    }
    public void UpdateCustomTexts()
    {
        if (GetDataScript.GetInstance().GetTextDatas() != null)
        {


            for (int i = 0; i < CustomAddBtn.Length - 1; i++)
            {
                if (CustomAddBtn[i] != null)
                {
                    PoolMgr.GetInstance().PushObj(CustomAddBtn[i]);
                    CustomAddBtn[i] = null;
                }
            }


            for (int i = 0; i < GetDataScript.GetInstance().GetTextDatas().Length; i++)
            {
                if (GetDataScript.GetInstance().GetTextDatas()[i] != null)
                {
                    PoolMgr.GetInstance().GetObj("UI/Panel/WordClickBtn", (o) =>
                    {
                        o.transform.SetParent(CustomStageBox.content);
                        CustomStageBox.content.sizeDelta = new Vector2(CustomStageBox.content.sizeDelta.x, CustomStageBox.content.sizeDelta.y + o.GetComponent<RectTransform>().sizeDelta.y + CustomStageBox.content.GetComponent<GridLayoutGroup>().spacing.y);
                        o.GetComponent<WordClickBtn>().SetLocalWordsType(GetDataScript.GetInstance().GetTextDatas()[i].WordType, true);
                        for(int i=0;i< CustomAddBtn.Length - 1; i++)
                        {
                            if (CustomAddBtn[i] == null)
                            {
                                CustomAddBtn[i] = o;
                                break;
                            }
                        }

                    });
                }
            }
        }
    }
    void OpenStageTypeListBox()
    {
        StageTypeListBox.gameObject.SetActive(true);
    }
    public void CloseBoxes()
    {
        EnterMsgBox.SetActive(false);
        CustomStageBox.gameObject.SetActive(false);
        DefaultStageBox.gameObject.SetActive(false);
        StageTypeListBox.gameObject.SetActive(false);
    }
    void CloseCustomStage()
    {
        OpenStageTypeListBox();
        CustomStageBox.gameObject.SetActive(false);

    }
    void CloseDefaultStage()
    {
        OpenStageTypeListBox();
        DefaultStageBox.gameObject.SetActive(false);

    }
    void CloseStageTypeList()
    {
        CloseBoxes();

    }
    void OpenCustomPanel()
    {
        
 
        PanelMgr.GetInstance().GetPanelFunction("CustomWordPanel").ShowMe();
    }
    void CloseEnterMsgBox()
    {
        EnterMsgBox.gameObject.SetActive(false);

    }
    public void OpenEnterMsgBox()
    {
        EnterMsgBox.SetActive(true);
    }




    void EnterGame()
    {
        Debug.Log("Enter the game...");

        if (GameMgr.GetInstance().GetRandomStage())
        {
            ScenesMgr.GetInstance().LoadSceneAsync("RandomGameScene", UpdateScene);
        }
        else
        {
            

            switch (GameMgr.GetInstance().GetWordType()) {
                case "Clothes":
                    ScenesMgr.GetInstance().LoadSceneAsync("ClothesGameScene", UpdateScene);
                    break;
                case "Common Pets":
                    ScenesMgr.GetInstance().LoadSceneAsync("CommonPetsGameScene", UpdateScene);
                    break;
                case "Foods":
                    ScenesMgr.GetInstance().LoadSceneAsync("FoodsGameScene", UpdateScene);
                    break;
                case "Fruits":
                    ScenesMgr.GetInstance().LoadSceneAsync("FruitsGameScene", UpdateScene);
                    break;
                case "Plants":
                    ScenesMgr.GetInstance().LoadSceneAsync("PlantsGameScene", UpdateScene);
                    break;




                }
        }

    }

    void UpdateScene()
    {
        CloseBoxes();
        gameObject.SetActive(false);

        MonoMgr.GetInstance().Reset();
    }
    public void OpenStageBox(string s)
    {
        CloseBoxes();
        switch (s)
        {
            case "Default Stage":
                DefaultStageBox.gameObject.SetActive(true);
                break;
            case "Custom Stage":
                CustomStageBox.gameObject.SetActive(true);
                break;
        }
    }

    void SetRandomStage()
    {
        GameMgr.GetInstance().SetRandomStage(true);
        OpenEnterMsgBox();
    }
    void OpenWordIconsPanel()
    {
        PanelMgr.GetInstance().GetPanelFunction("WordIconsPanel").ShowMe();
    }
    public override GameLobbyPanel GetGameLobbyPanel()
    {
        return this;
    }
}
