using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GameMgr : SingleMgr<GameMgr>
{

    private bool IsGameStarter = false;
    string WordsType = "Food";

    private LevelsOperate Levels;
    private PlayerCtr playerCtr;
    private bool EndGame = false;
    private string TargetWords;


    int CurrentWords = 0;
    string[] TargetTexts;
    Dictionary<string, string[]> AllTargetTexts = new Dictionary<string, string[]>();
    public GameObject InstanceObj;

    public bool GameEnded = false;
    private bool RandomStage = false;
    private bool CustomGame = false;
    public void SetCustomGame(bool b)
    {
        CustomGame = b;
    }
    public bool GetCustomGame()
    {
        return CustomGame;
    }
    public void GetAllEnemySpawnPoints()
    {
  
        AnimationInstancing.AnimationInstancingMgr.Instance.Clear();
        AnimationInstancing.AnimationInstancingMgr.Instance.SetupAgain();
        if (Levels)
        {
            
            MonoBehaviour.Destroy(Levels.gameObject);
        }
        Levels = new GameObject("LevelsOperate").AddComponent<LevelsOperate>();
        if (playerCtr)
        {
            MonoBehaviour.Destroy(Levels.gameObject);
        }
        playerCtr = new GameObject("PlayerCtr").AddComponent<PlayerCtr>();
    }
    public void LoadTexts()
    {
        TextAsset[] t = Resources.LoadAll<TextAsset>("Text");


        for (int i = 0; i < t.Length; i++)
        {
            if (!AllTargetTexts.ContainsKey(t[i].name))
            {
                AllTargetTexts.Add(t[i].name, t[i].ToString().Split(new char[] { '\n' }));
                Debug.Log(t[i].name);
            }
            
        }
       
        
    }

    public bool GetGameEnded()
    {
        return GameEnded;
    }
    public void SetGameEnded(bool b)
    {
        GameEnded = b;
    }
    public LevelsOperate GetLevelsOperate()
    {
        return Levels;
    }
    public PlayerCtr GetPlayerCtr()
    {
        return playerCtr;
    }
    public string GetTargetWords()
    {
        return TargetWords;
    }
    public bool ConfirmInputWord(string str)
    {
        if (str.ToUpper() == TargetWords)
        {
            Levels.ClearComboSlashed();
            RandomSetWord();

            return true;
        }
        return false;
    }
    public void ResetLevel()
    {
        CurrentWords = 0;
        if (GetCustomGame())
        {

        }
    }
    public void RandomSetWord()
    {
        SetGameEnded(false);

      

        /*if (CurrentWords == 2)
        {
         EventsCenter.GetInstance().TriggerEvent("GameEnd", true);
        }*/
        if (CurrentWords + 1 < TargetTexts.Length+1)
        {
            TargetWords = TargetTexts[CurrentWords].ToUpper();
            TargetWords = TargetWords.Trim();
            
            Debug.Log(TargetWords);
            CurrentWords += 1;
        }
        else if (TargetTexts.Length>0)
        {
            
            EventsCenter.GetInstance().TriggerEvent("GameEnd", true);

        }
    }
    public void GameEnd(bool win)
    {
        SetGameEnded(true);

        if (win)
        {
            PanelMgr.GetInstance().GetPanelFunction("GameEndPanel").GetGameEndPanel().GameWin();
        }
        else
        {
            PanelMgr.GetInstance().GetPanelFunction("GameEndPanel").GetGameEndPanel().GameLose();
        }
                PanelMgr.GetInstance().GetPanelFunction("AlphaTypingPanel").HideMe();
    }

    public void SwitchEndGame(bool b)
    {
        EndGame = b;
    }

    public bool GetEndGame()
    {
        return EndGame;
    }




    public GameMgr()
    {
        //EventsCenter.GetInstance().ClearEvents();
        if (InstanceObj == null)
        {
            InstanceObj = new GameObject("InstanceObj");
            InstanceObj.AddComponent<InsObj>();
        }
        if (!CustomGame)
        {
            LoadTexts();
        }
        else
        {
              LoadTexts();
        }
        
 


        Application.targetFrameRate = 60;
    }

 
    public void Reset()
    {
    }

    public string GetWordType()
    {
        return WordsType;
    }
    public void SetWordType(string s,bool custom)
    {
        if (!custom)
        {
            WordsType = s;
          
            TargetTexts = AllTargetTexts[s];
        }
        else
        {
            string[] r;
            for (int i=0;i< GetDataScript.GetInstance().GetTextDatas().Length; i++)
            {
                if (GetDataScript.GetInstance().GetTextDatas()[i].WordType.ToString().Trim() == s.ToString().Trim())
                {
                    r = GetDataScript.GetInstance().GetTextDatas()[i].text;
                    TargetTexts = r;
                    WordsType = s;

                    break;
                }
            }
          






        }
    }
    public bool GetRandomStage()
    {
        return RandomStage;
    }
    public void SetRandomStage(bool b)
    {
        RandomStage = b;

    }
    public void GameMgrResetEventTrigger()
    {
        
        EventsCenter.GetInstance().AddEventListener<bool>("GameEnd", GameEnd);
        Debug.Log("check");
    }
    void Nothing()
    {
        //SetTheGameStartedOrNot(false);
    }



    public void Progress(int value)
    {
        EventsCenter.GetInstance().TriggerEvent("Progress");
    }

    public void MainObjectDead()
    {

        Debug.Log("事件管理员得知重要物件已死亡");
    }

    public void NPCObjectDead(bool isEnemy)
    {
        EventsCenter.GetInstance().TriggerEvent("NPCObjectDead");
    }

    public void SceneEnd()
    {


        //Reset();
        if (MonoMgr.GetInstance() != null && MonoMgr.GetInstance().GetInitialMgr() != null)
        {
            MonoMgr.GetInstance().GetInitialMgr().ResetScript();
        }
        if (PanelMgr.GetInstance() != null)
        {
            PanelMgr.GetInstance().ResetAllPanel();
        }
    }




}
