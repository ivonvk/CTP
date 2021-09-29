using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//using SpeechLib;
public class AlphaTypingPanel : BasePanel
{
    private Button AtoFBtn;
    private Button GtoLBtn;
    private Button MtoRBtn;
    private Button StoXBtn;
    private Button YtoZBtn;

    private Button ResetBtn;
    private Button SwitchBoardBtn;


   // private GameObject AtoF;
   // private GameObject GtoL;
   // private GameObject MtoR;
   // private GameObject StoX;
   // private GameObject YtoZ;

    private GameObject MenuBox;

    private GameObject AlphaBoardBox;

    private Slider SlashBar;
    

    private Text SlashBarText;

    private Slider HPBar;
    private Text HPBarText;
    private Text InputText;


    private Text TipsNextText;
    float DisplayTips =2f;

    private Slider ComboBar;
    private Text ComboText;

    private Button ComboBtn;

    private Button MenuBtn;

    private Button MenuCloseBtn;
    private Button MenuCancelBtn;

   
    bool TipsTransfering = true;


    private Animator ComboAnim;
    private Animator SlashAnim;
    private Animator HPBarAnim;

    private Animator LightFrameAnim;
    //SpVoice voice = new SpVoice();
    bool voicePlayed = false;

    
    public override void Start()
    {
        base.Start();
        GetUI();
        AddFunctionUI();

       // SwitchAlphaBoard();


        ResetText();

        SwitchBoardBtn.gameObject.SetActive(false);
        MenuCancel();
        AddCombo(0);

  
        
    }
    private void OnEnable()
    {
       
            Debug.Log("alpha");
        SetTipsStartValue();
            ResetPanel();
        
    }
    public override void ResetPanel()
    {
        base.ResetPanel();
        StartCoroutine(Operation());
    }

    private IEnumerator Operation()
    {
        while (gameObject.activeInHierarchy)
        {
            if (GameMgr.GetInstance().GameEnded)
            {

                goto end;
            }
            else if(TipsNextText!=null)
            {
                TipsNextText.text = GameMgr.GetInstance().GetTargetWords();
            }
            if (SlashBar && GameMgr.GetInstance().GetPlayerCtr())
            {
                SlashBar.value = GameMgr.GetInstance().GetPlayerCtr().GetSlashPower() / 100;
                SlashBarText.text = "Slash!: " + GameMgr.GetInstance().GetPlayerCtr().GetSlashPower().ToString("F0");
            }


            if (TipsTransfering)
            {
                if (DisplayTips <= 0)
                {
                    Color c = TipsNextText.color;
                    c.a -= 0.1f;
                    TipsNextText.color = c;


                    if (c.a <= 0)
                    {

                        TipsTransfering = false;
                    }
                    if (!voicePlayed)
                    {

                        // TextToSpeech.instance.StartSpeak(TipsNextText.text);


                        //  voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        // voice.Speak(TipsNextText.text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                        voicePlayed = true;
                    }
                }
                else
                {
                    DisplayTips -= 0.1f;


                }
            }
            end:
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    public void ResetText()
    {

        TipsNextText.text = GameMgr.GetInstance().GetTargetWords();
    }
    public override void GetUI()
    {
        SlashBar = GetCtr<Slider>("SlashBar");

        SlashBarText = GetCtr<Text>("SlashBarText");

        HPBar = GetCtr<Slider>("HPBar");

        HPBarText = GetCtr<Text>("HPBarText");


        AtoFBtn = GetCtr<Button>("AtoFBtn");
        GtoLBtn = GetCtr<Button>("GtoLBtn");
        MtoRBtn = GetCtr<Button>("MtoRBtn");
        StoXBtn = GetCtr<Button>("StoXBtn");
        YtoZBtn = GetCtr<Button>("YtoZBtn");

        InputText = GetCtr<Text>("InputText");
        //  TipsText = GetCtr<Text>("TipsText");
        TipsNextText = GetCtr<Text>("TipsNextText");
        ResetBtn = GetCtr<Button>("ResetBtn");
        SwitchBoardBtn = GetCtr<Button>("SwitchBoardBtn");

        ComboBar = GetCtr<Slider>("ComboBar");
        ComboText = GetCtr<Text>("ComboText");
        ComboBtn = GetCtr<Button>("ComboBtn");

        MenuBtn = GetCtr<Button>("MenuBtn");


        MenuCloseBtn = GetCtr<Button>("MenuCloseBtn");
        MenuCancelBtn = GetCtr<Button>("MenuCancelBtn");

        ComboAnim = GetCtr<Image>("ComboBtn").GetComponent<Animator>();



    //    AtoF = GetCtr<Image>("AtoF").gameObject;
      //  GtoL = GetCtr<Image>("GtoL").gameObject;
      //  MtoR = GetCtr<Image>("MtoR").gameObject;
      //  StoX = GetCtr<Image>("StoX").gameObject;
     //   YtoZ = GetCtr<Image>("YtoZ").gameObject;

        //AlphaBoardBox = GetCtr<Image>("AlphaBoardBox").gameObject;

        MenuBox = GetCtr<Image>("MenuBox").gameObject;

        SlashAnim = SlashBar.GetComponent<Animator>();
        HPBarAnim = HPBar.GetComponent<Animator>();
        LightFrameAnim = GetCtr<Image>("Light").GetComponent<Animator>();
    }
    public override void AddFunctionUI()
    {

        AddFunction(ResetBtn.gameObject, ResetInput, Nothing, Nothing, Nothing);
        //AddFunction(SwitchBoardBtn.gameObject, SwitchAlphaBoard, Nothing, Nothing, Nothing);



        AddFunction(MenuBtn.gameObject, MenuOpen, Nothing, Nothing, Nothing);

        AddFunction(MenuCloseBtn.gameObject, BackToLobby, Nothing, Nothing, Nothing);
        AddFunction(MenuCancelBtn.gameObject, MenuCancel, Nothing, Nothing, Nothing);

    }
    void MenuCancel()
    {
        MenuBox.gameObject.SetActive(false);
    } void MenuOpen()
    {
        MenuBox.gameObject.SetActive(true);
    }
    void BackToLobby()
    {
        Debug.Log("Back to lobby...");

        ScenesMgr.GetInstance().LoadSceneAsync("GameLobby", UpdateScene);

    }
    void UpdateScene()
    {

        MenuCancel();
        gameObject.SetActive(false);
        MonoMgr.GetInstance().Reset();
    }

    void Update()
    {
        

    }
    public Vector3 GetComboBtnPos()
    {
        return ComboBtn.transform.position;
    }  
    public Button GetComboBtn()
    {
        return ComboBtn;
    }
    public void SetComboAnim(string s)
    {
        ComboAnim.Play(s);
    }  
    public void SetSlashAnim(string s)
    {
        SlashAnim.Play(s);
    }  
    public void SetHPBarAnim(string s)
    {
        HPBarAnim.Play(s);
    }
    public void HPBarGetDmg(float curHP,float maxHP)
    {
        HPBar.value = curHP / maxHP;
        HPBarText.text = "HP: " + curHP.ToString("F0") + "/" + maxHP.ToString("F0");
        LightFrameAnim.Play("getdmg");
    }

    void SwitchAlphaBoard()
    {
        AlphaBoardBox.SetActive(!AlphaBoardBox.activeInHierarchy);
    }
    void OpenAtoFFrame()
    {
        CloseAllAlphaBox();
        //AtoF.SetActive(true);

    }

    void OpenGtoLFrame()
    {
        CloseAllAlphaBox();
        //GtoL.SetActive(true);

    }
    void OpenMtoRFrame()
    {
        CloseAllAlphaBox();
        //MtoR.SetActive(true);

    }
    void OpenStoXFrame()
    {
        CloseAllAlphaBox();
       // StoX.SetActive(true);

    }
    void OpenYtoZFrame()
    {
        CloseAllAlphaBox();
        //YtoZ.SetActive(true);
    }


    void CloseAllAlphaBox()
    {
      /*  AtoF.SetActive(false);
        GtoL.SetActive(false);
        MtoR.SetActive(false);
        StoX.SetActive(false);
        YtoZ.SetActive(false);*/
    }
    public void ResetInput()
    {
        GameMgr.GetInstance().GetPlayerCtr().CooldownSlash(false);
        InputText.text = GameMgr.GetInstance().GetPlayerCtr().AddCurrentInputStr("");
        GameMgr.GetInstance().GetLevelsOperate().SetAllComboSlashed(false);

    }
    public void ResetInput(bool s)
    {
        GameMgr.GetInstance().GetPlayerCtr().CooldownSlash(s);
        InputText.text = GameMgr.GetInstance().GetPlayerCtr().AddCurrentInputStr("");

        GameMgr.GetInstance().GetLevelsOperate().SetAllComboSlashed(false);

    }
    public string GetInputText()
    {
        return InputText.text;
    }
    public void SendAlphaClickMsg(string s)
    {
        string str = GameMgr.GetInstance().GetTargetWords();


        if (str[InputText.text.Length].ToString() == s)
        {

            for (int i = 0; i < GameMgr.GetInstance().GetLevelsOperate().GetComboCubeCtr().Count; i++)
            {
                if (GameMgr.GetInstance().GetLevelsOperate().GetComboCubeCtr()[i] != null)
                {

                    if (GameMgr.GetInstance().GetLevelsOperate().GetComboCubeCtr()[i].stats.text == s && !GameMgr.GetInstance().GetLevelsOperate().GetComboCubeCtr()[i].GetSlash())
                    {
                        InputText.text = GameMgr.GetInstance().GetPlayerCtr().AddCurrentInputStr(s);
                        GameMgr.GetInstance().GetLevelsOperate().GetComboCubeCtr()[i].GetTap();
                        break;
                    }
                }
            }
        }
        if (GameMgr.GetInstance().ConfirmInputWord(InputText.text))
        {
            SetTipsStartValue();
            ResetInput(true);
            // TipsText.text = str;
            
            Invoke("AttackSoldersDelay", 1.5f);
            GameMgr.GetInstance().GetLevelsOperate().AddSpawnTimer(15f);
        }
        else if (InputText.text != GameMgr.GetInstance().GetTargetWords() && InputText.text.Length >= GameMgr.GetInstance().GetTargetWords().Trim().Length)
        {
            GameMgr.GetInstance().GetLevelsOperate().SpeedUpAllSoldiers(20f);
            ResetInput(false);
            GameMgr.GetInstance().GetLevelsOperate().SetAllComboSlashed(false);
        }
    }
    void AttackSoldersDelay()
    {
        
        GameMgr.GetInstance().GetLevelsOperate().AttackAllSoldiers(5);
    }
    public void SendAlphaClickMsg(ComboStats s)
    {
        string str = GameMgr.GetInstance().GetTargetWords();


        if (str[InputText.text.Length].ToString() == s.text)
        {
            if (!s.combo.GetSlash())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(
            PanelMgr.GetInstance().GetPanelFunction("AlphaTypingPanel").GetAlphaTyping().GetComboBtn().transform.position);

             

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    PoolMgr.GetInstance().GetObj("Prefabs/ComboFly", (o) =>
                    {
                        o.transform.position = s.combo.gameObject.transform.position+new Vector3(0,30,-15);
                        o.GetComponent<UIEffectCtr>().SetTargetPos(hit.point + new Vector3(0, 0, 0));

                    });
                }

                ComboAnim.Play("combo");
                InputText.text = GameMgr.GetInstance().GetPlayerCtr().AddCurrentInputStr(s.text);
                // GameMgr.GetInstance().GetLevelsOperate().comboCtr[s][i].SetMatDmg();
                //GameMgr.GetInstance().GetLevelsOperate().comboCtr[s][i].SetSlash(true);
                s.combo.GetTap();
                AddCombo(1);
            }
        }
        else
        {
            GameMgr.GetInstance().GetPlayerCtr().AddSlashPower(-2);
            SlashAnim.Play("reduce");

        }
        if (GameMgr.GetInstance().ConfirmInputWord(InputText.text))
        {

            SetTipsStartValue();
            ResetInput(true);
          //  TipsText.text = str;
            GameMgr.GetInstance().GetLevelsOperate().AttackAllSoldiers(5);
            GameMgr.GetInstance().GetLevelsOperate().AddSpawnTimer(15f);
            AddCombo(-10);
        }
        else if (InputText.text.Length >= GameMgr.GetInstance().GetTargetWords().Trim().Length)
        {
            SlashAnim.Play("reduce");
            GameMgr.GetInstance().GetLevelsOperate().SpeedUpAllSoldiers(20f);
            ResetInput(false);
            GameMgr.GetInstance().GetLevelsOperate().SetAllComboSlashed(false);
        }
    }
    public void SetTipsStartValue()
    {
       
        // DisplayTips = 4f;

        TipsTransfering = true;
        voicePlayed = false;
        DisplayTips = 2f;
        /* Color c = TipsText.color;
         c.a = 0f;
         TipsText.color = c;*/
        if (TipsNextText!=null)
        {
            Color c2 = TipsNextText.color;
            c2.a = 1f;
            TipsNextText.color = c2;
        }
    }
    public void AddCombo(int i)
    {
        
        ComboBar.value += i;
        ComboText.text = ComboBar.value.ToString();
        LightFrameAnim.Play("combo");
    }
 

    public override AlphaTypingPanel GetAlphaTyping()
    {
        return this;
    }
    


}
