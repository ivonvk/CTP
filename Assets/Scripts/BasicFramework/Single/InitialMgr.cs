using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InitialMgr : MonoBehaviour
{
    bool Initialized = false;
    private void Start()
    {
        //Invoke("UpdateInitialization", 1f);
        //UpdateInitialization();
        MonoMgr.GetInstance().AddUpdateListener(UpdateInitialization);
    }
    public void ResetScript()
    {
        Initialized = false;
        Debug.Log("ResetScript");
        //Invoke("UpdateInitialization", 1f);
        //  UpdateInitialization();
        MonoMgr.GetInstance().AddUpdateListener(UpdateInitialization);
    }
    void InitializationDelay()
    {
        UpdateInitialization();
    }
    public bool GetInitialized()
    {
        return Initialized;
    }
    void UpdateInitialization()
    {
        if (!Initialized)
        {
            Initialized = true;
            InputMgr.GetInstance().SetCanInput(true);
            InitialPanel(SceneManager.GetActiveScene().name);
            InitialPrefabs(SceneManager.GetActiveScene().name);
            InitialLevelToGameMgr(SceneManager.GetActiveScene().name);
        }
        /* else if(PanelMgr.GetInstance()!=null&& 
             PanelMgr.GetInstance().GetPanelFunction("InGameMenuPanel")!=null&&
             PanelMgr.GetInstance().GetPanelFunction("GameOverPanel") != null&&
              PanelMgr.GetInstance().GetPanelFunction("UpgradePanel") != null)
         {
             MonoMgr.GetInstance().RemoveUpdateListener(UpdateInitialization);
             if(SceneManager.GetActiveScene().name == "Game")
             {
                 PanelMgr.GetInstance().GetPanelFunction("InGameMenuPanel").HideMe();
                 PanelMgr.GetInstance().GetPanelFunction("GameOverPanel").HideMe();
                 PanelMgr.GetInstance().GetPanelFunction("UpgradePanel").HideMe();
             }
         }*/
    }
    private void InitialPanel(string sceneName)
    {
        switch (sceneName)
        {
            case "GameLobby":

                CreateInitialPanel<GameLobbyPanel>("GameLobbyPanel", layer_type.middle, false);
                CreateInitialPanel<WordIconsPanel>("WordIconsPanel", layer_type.middle, true);
                CreateInitialPanel<CustomWordPanel>("CustomWordPanel", layer_type.middle, true);
                break;

            case "GameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            case "ClothesGameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            case "CommonPetsGameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            case "FoodsGameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            case "FruitsGameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            case "PlantsGameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            case "RandomGameScene":
                CreateInitialPanel<AlphaTypingPanel>("AlphaTypingPanel", layer_type.middle, false);
                CreateInitialPanel<GameEndPanel>("GameEndPanel", layer_type.middle, true);
                break;
            default:
                break;
        }

    }
    private void InitialPrefabs(string sceneName)
    {
        switch (sceneName)
        {
            case "SampleScene":
                break;
            case "GameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            case "ClothesGameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            case "CommonPetsGameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            case "FoodsGameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            case "FruitsGameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            case "PlantsGameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            case "RandomGameScene":
                GameMgr.GetInstance().GetAllEnemySpawnPoints();
                break;
            default:
                break;
        }
    }
    private void InitialLevelToGameMgr(string sceneName)
    {
        switch (sceneName)
        {
            case "ClothesGameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;
            case "CommonPetsGameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;
            case "FoodsGameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;
            case "FruitsGameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;
            case "PlantsGameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;
            case "GameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;

            case "RandomGameScene":
                GameMgr.GetInstance().GameMgrResetEventTrigger();
                break;
            default:
                break;
        }
    }
    private void CreateInitialPanel<T>(string panel, layer_type pos, bool hide) where T : BasePanel
    {
        PanelMgr.GetInstance().CreatePanel<T>(panel, pos, (o) =>
        {
            Debug.Log(panel + " 面板被生成.");
            if (hide)
            {

                o.GetComponent<BasePanel>().SetupCheck();

            }

        });

    }
    void Nothing()
    {

    }
}
