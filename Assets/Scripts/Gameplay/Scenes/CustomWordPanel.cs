using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class CustomWordPanel : BasePanel
{
    private GameObject ComfirmMsgBox;
    private GameObject CancelMsgBox;
    private Button ConfirmBtn;
    private Button BackBtn;

    private Button PlayBtn;

    private Button CancelBtn;
    private Button CloseBtn;
    private Button CloseBoxBtn;

    private Button AddTextBtn;

    private ScrollRect CustomTextBox;
    private string WordType;
    private GameObject AddTextBox;

    private InputField wordTypeInput;

    public List<CustomAddBtnCtr> customAddBtnCtrs = new List<CustomAddBtnCtr>(new CustomAddBtnCtr[100]);
    DirectoryInfo levelDirectoryPath;
    bool ready = false;
    public void RemoveCustomText(CustomAddBtnCtr customAdd)
    {
        if (customAddBtnCtrs.Contains(customAdd))
        {
            customAddBtnCtrs[customAddBtnCtrs.IndexOf(customAdd)] = null;
        }
    }

    void AddText()
    {
        PoolMgr.GetInstance().GetObj("UI/CustomAddBtn", (o) =>
        {
            o.transform.SetParent(CustomTextBox.content);
            CustomTextBox.content.sizeDelta = new Vector2(CustomTextBox.content.sizeDelta.x, CustomTextBox.content.sizeDelta.y + o.GetComponent<RectTransform>().sizeDelta.y + CustomTextBox.content.GetComponent<GridLayoutGroup>().spacing.y);
            for (int i = 0; i < customAddBtnCtrs.Count; i++)
            {
                if (customAddBtnCtrs[i] == null)
                {
                    customAddBtnCtrs[i] = o.GetComponent<CustomAddBtnCtr>();
                    customAddBtnCtrs[i].SetPanel(this);
                    i = customAddBtnCtrs.Count;
                }

            }
        });

        AddTextBox.transform.SetAsLastSibling();
    }


    public void Refresh()
    {
        GetDataScript.GetInstance().LoadData();
        for (int i = 0; i < customAddBtnCtrs.Count; i++)
        {
            if (customAddBtnCtrs[i] != null)
            {

                PoolMgr.GetInstance().PushObj(customAddBtnCtrs[i].gameObject);
                customAddBtnCtrs[i] = null;
            }
        }

        for (int i = 0; i < GetDataScript.GetInstance().GetTextDatas().Length; i++)
        {

            if (GetDataScript.GetInstance().GetTextDatas()[i].WordType == GameMgr.GetInstance().GetWordType())
            {
                for (int x = 0; x < GetDataScript.GetInstance().GetTextDatas()[i].text.Length; x++)
                {
                    Debug.Log(x+", "+ GetDataScript.GetInstance().GetTextDatas()[i].text[x]);
                    PoolMgr.GetInstance().GetObj("UI/CustomAddBtn", (o) =>
                    {

                        o.transform.SetParent(CustomTextBox.content);

                        CustomTextBox.content.sizeDelta = new Vector2(CustomTextBox.content.sizeDelta.x, CustomTextBox.content.sizeDelta.y + o.GetComponent<RectTransform>().sizeDelta.y + CustomTextBox.content.GetComponent<GridLayoutGroup>().spacing.y);

                        customAddBtnCtrs[x] = o.GetComponent<CustomAddBtnCtr>();
                        customAddBtnCtrs[x].SetPanel(this);
                        customAddBtnCtrs[x].SetWordType(GetDataScript.GetInstance().GetTextDatas()[i].WordType);
                        customAddBtnCtrs[x].SetTargetWord(GetDataScript.GetInstance().GetTextDatas()[i].text[x]);

                        wordTypeInput.text = GetDataScript.GetInstance().GetTextDatas()[i].WordType;

                    });

                }
                break;
            }
        }









        AddTextBox.transform.SetAsLastSibling();
    }

    public void Awake()
    {
        if (Application.isEditor)
        {
            levelDirectoryPath = new DirectoryInfo(Application.streamingAssetsPath + "/CustomText/");
        }
        else
        {
            levelDirectoryPath = new DirectoryInfo(Application.streamingAssetsPath + "/CustomText/");
        }
    }
    public override void Start()
    {
        
        SetupCheck();






    }
   public override void SetupCheck()
    {
        base.Start();
        if (ready)
        {
            MonoMgr.GetInstance().RemoveUpdateListener(SetupCheck);
            Close();
            return;
        }else 
      
        if (!ready)
        {
            GetUI();
            AddFunctionUI();
            MonoMgr.GetInstance().AddUpdateListener(SetupCheck);
            ready = true;
        }
    }
    void Confirm()
    {

        ComfirmMsgBox.SetActive(false);



        int count = 0;
        for (int i = 0; i < customAddBtnCtrs.Count; i++)
        {
            if (customAddBtnCtrs[i] != null)
            {
                count += 1;

            }
        }
        SingleMgr<GetDataScript>.TextData t = new SingleMgr<GetDataScript>.TextData();
        t.text = new string[count];
        t.WordType = wordTypeInput.text;
        for (int i = 0; i < t.text.Length; i++)
        {
            t.text[i] = customAddBtnCtrs[i].GetTargetWord();


        }
        if (t.text.Length > 0)
        {

            GetDataScript.GetInstance().SaveData(wordTypeInput.text, t);
            GameMgr.GetInstance().SetWordType(wordTypeInput.text, true);

            PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").GetGameLobbyPanel().UpdateCustomTexts();
        }

    }
    void Back()
    {
        ComfirmMsgBox.SetActive(false);

    }
    void Close()
    {

        ComfirmMsgBox.SetActive(false);
        CancelMsgBox.SetActive(false);
        HideMe();
    }
    void Cancel()
    {
        gameObject.SetActive(false);

    }
    public override void GetUI()
    {
        ComfirmMsgBox = GetCtr<Image>("ComfirmMsgBox").gameObject;
        CancelMsgBox = GetCtr<Image>("CancelMsgBox").gameObject;
        AddTextBox = GetCtr<Image>("AddTextBox").gameObject;

        ConfirmBtn = GetCtr<Button>("ConfirmBtn");
        BackBtn = GetCtr<Button>("BackBtn");
        CloseBtn = GetCtr<Button>("CloseBtn");
        CancelBtn = GetCtr<Button>("CancelBtn");
        AddTextBtn = GetCtr<Button>("AddTextBtn");
        CloseBoxBtn = GetCtr<Button>("CloseBoxBtn");

        PlayBtn = GetCtr<Button>("PlayBtn");

        CustomTextBox = GetCtr<ScrollRect>("CustomTextBox");

        wordTypeInput = GetCtr<InputField>("wordTypeInput");
    }

    public override void AddFunctionUI()
    {
        AddFunction(ConfirmBtn.gameObject, Confirm, Nothing, Nothing, Nothing);
        AddFunction(BackBtn.gameObject, Back, Nothing, Nothing, Nothing);

        AddFunction(CloseBtn.gameObject, Close, Nothing, Nothing, Nothing);
        AddFunction(CancelBtn.gameObject, Cancel, Nothing, Nothing, Nothing);
        AddFunction(AddTextBtn.gameObject, AddText, Nothing, Nothing, Nothing);
        AddFunction(CloseBoxBtn.gameObject, HideMe, Nothing, Nothing, Nothing);

        AddFunction(PlayBtn.gameObject, Play, Nothing, Nothing, Nothing);



    }
    void Play()
    {
        Debug.Log("Enter the game...");
        PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").GetGameLobbyPanel().CloseBoxes();
        PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").GetGameLobbyPanel().HideMe();
        GameMgr.GetInstance().SetWordType(wordTypeInput.text, true);

        ScenesMgr.GetInstance().LoadSceneAsync("GameScene", UpdateScene);
    }




    void UpdateScene()
    {
        // Close();

        gameObject.SetActive(false);
        GameMgr.GetInstance().SetRandomStage(false);
        GameMgr.GetInstance().SetCustomGame(true);

        MonoMgr.GetInstance().Reset();
    }

    public override CustomWordPanel GetCustomWordPanel()
    {
        return this;
    }


}
