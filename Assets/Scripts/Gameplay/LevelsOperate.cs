using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class ComboStats
{
    public string text;
    public ComboCubeCtr combo;
}
public class LevelsOperate : MonoBehaviour
{


    private float EnemyPower = 20f;
    public float SpawnTimer = 20f;
    public float SpawnEnd = 20f;
    public GameObject[] EnemySpawnPoint;
    public List<GameObject> ComboSpawn = new List<GameObject>();


    List<SoldierCtr> SoldierCtrs = new List<SoldierCtr>(new SoldierCtr[300]);
    public List<ComboCubeCtr> comboCube = new List<ComboCubeCtr>(new ComboCubeCtr[32]);
    public float SpawnDelay;
    public bool Spawning = false;
    int spawned = 0;
    Material riverMat;
    Material roadMat;
    Vector2 riverScroll;
    Vector2 roadScroll;
    List<string> EnemyNames = new List<string> { "Enemy1", "Enemy2" };
    List<int> checkerlist;

    
    void Awake()
    {
        EnemySpawnPoint = GameObject.FindGameObjectsWithTag("EneSpawn");
        Transform array = GameObject.FindGameObjectWithTag("ComboSpawn").transform;
    
        int count = 0;
        foreach(Transform t in array)
        {
            if(t!= array)
            {
                ComboSpawn.Add(array.GetChild(count).gameObject);
                count += 1;
            }
        }
    }
    private void Start()
    {
        GameMgr.GetInstance().ResetLevel();
        GameMgr.GetInstance().RandomSetWord();

        riverMat = ResMgr.GetInstance().ResourceLoad<Material>("Models/Materials/River");
        roadMat = ResMgr.GetInstance().ResourceLoad<Material>("Models/Materials/Road");
    }
    public void AddSpawnTimer(float f)
    {
        // SpawnTimer += f;
        SpawnTimer = 18f;
        Spawning = false;
    }

    void Update()
    {
        riverScroll = riverMat.mainTextureOffset;
        riverScroll.x += 0.05f * Time.deltaTime;
        if (riverMat.mainTextureOffset.x > 100)
        {
            riverScroll = Vector2.zero;
        }

        riverMat.mainTextureOffset = riverScroll;

        roadScroll = roadMat.mainTextureOffset;
        roadScroll.x += 0.05f * Time.deltaTime;
        roadScroll.y += 0.05f * Time.deltaTime;
        if (roadMat.mainTextureOffset.x > 100)
        {
            roadScroll = Vector2.zero;
        }

        roadMat.mainTextureOffset = roadScroll;

        if (GameMgr.GetInstance().GetGameEnded())
        {
            if (SoldierCtrs[0] != null)
            {
                for (int i = 0; i < SoldierCtrs.Count; i++)
                {
                    if (SoldierCtrs[i] != null)
                    {

                        SetAllComboSlashed(false);
                        ClearComboSlashed();
                        PoolMgr.GetInstance().PushObj(SoldierCtrs[i].gameObject);
                    }
                }
            }
            return;
        }



        if (SpawnTimer>= SpawnEnd && comboCube[0] == null)
        {
            int get = -1;
            checkerlist = new List<int>(new int[ComboSpawn.Count]);
            for (int i = 0; i < ComboSpawn.Count; i++)
            {
                checkerlist[i] = i;
            }
            for (int i = 0; i < GameMgr.GetInstance().GetTargetWords().Trim().Length; i++)
            {
                PoolMgr.GetInstance().GetObj("Prefabs/ComboCube", (o) =>
                {
                    o.name = "ComboCube";
                    ComboCubeCtr c = o.GetComponent<ComboCubeCtr>();
                     if (get < 4)
                     {

                         get += 1;
                     }
                     else
                     {

                         get = checkerlist[UnityEngine.Random.Range(5, checkerlist.Count)];
                         checkerlist.Remove(get);
                     }
                    if (!comboCube.Contains(c))
                    {
                        comboCube[get] = c;
                    }
                    c.SetupSoldier(ComboSpawn[get].transform.position);
                    c.SetAlpha(GameMgr.GetInstance().GetTargetWords()[i].ToString());
                });
            }
        }
        else if (!Spawning)
        {
            SetEnemyPower(0.1f * Time.deltaTime);
            SpawnTimer += 1f * Time.deltaTime;
            if (SpawnTimer > SpawnEnd)
            {
                Spawning = true;
            }
        }
        else
        {
            SpawnDelay -= 1f * Time.deltaTime;
            if (spawned + 1 < GameMgr.GetInstance().GetTargetWords().Trim().Length)
            {

                if (SpawnDelay <= 0)
                {
                    SpawnDelay = 2.5f;
                    
                    for (int i = 0; i < GameMgr.GetInstance().GetTargetWords().Trim().Length * 2;i++) {
                        int rand = (i % 2 == 0) ? 0 : 1;
                        PoolMgr.GetInstance().GetObj("Prefabs/" + EnemyNames[rand], (o) =>
                         {

                             o.name = EnemyNames[rand];
                             RaycastHit hit;
                             if (Physics.Raycast(EnemySpawnPoint[UnityEngine.Random.Range(0, EnemySpawnPoint.Length - 1)].transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << 8))
                             {
                                 SoldierCtr sctr = o.GetComponent<SoldierCtr>();
                                 sctr.SetupSoldier(hit.point);
                                 if (!SoldierCtrs.Contains(sctr))
                                 {
                                     for (int i = 0; i < SoldierCtrs.Count; i++)
                                     {
                                         if (SoldierCtrs[i] == null)
                                         {
                                             SoldierCtrs[i] = sctr;
                                             spawned += 1;
                                             break;
                                         }
                                     }
                                 }
                             }
                         });
                    }
                }
            }
            else
            {
                Spawning = false;
            }
        }
    }
    public void SpeedUpAllSoldiers(float f)
    {
        for (int i = 0; i < SoldierCtrs.Count; i++)
        {
            if (SoldierCtrs[i] != null)
            {
                SoldierCtrs[i].WalkSpeedIncrease(f, 2.5f);
            }
        }
    }
    public void AttackAllSoldiers(int dmg)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(
         PanelMgr.GetInstance().GetPanelFunction("AlphaTypingPanel").GetAlphaTyping().GetComboBtn().transform.position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int i = 0; i < SoldierCtrs.Count; i++)
            {
                if (SoldierCtrs[i] != null)
                {
                    PoolMgr.GetInstance().GetObj("Prefabs/ComboAttack", (o) =>
                    {
                        UIAttackerCtr uIAttackerCtr = o.GetComponent<UIAttackerCtr>();
                        //uIEffectCtr.ResetObj();
                        o.transform.position = hit.point;
                        uIAttackerCtr.SetTargetPos(SoldierCtrs[i].transform.position);
                    });
                    SoldierCtrs[i].GetDamage(dmg);
                }
            }
        }
    }


    public void RemoveSctr(SoldierCtr sctr)
    {
        if (SoldierCtrs.Contains(sctr))
        {
            if (spawned - 1 >= 0)
            {
                SoldierCtrs[SoldierCtrs.IndexOf(sctr)] = null;
                spawned -= 1;
            }
        }
    }
    public void SetAllComboSlashed(bool b)
    {
        for (int i = 0; i < comboCube.Count; i++)
        {
            if (comboCube[i] != null)
            {
                comboCube[i].SetSlash(b);
            }
        }
    }
    public void ClearComboSlashed()
    {
        SetAllComboSlashed(false);
        for (int i = 0; i < comboCube.Count; i++)
        {
            if (comboCube[i] != null)
            {

                PoolMgr.GetInstance().PushObj(comboCube[i].gameObject);
                comboCube[i] = null;
            }
        }

    }
    public void SetEnemyPower(float f)
    {
        EnemyPower += f;
        if (EnemyPower <= 0)
        {
            GameMgr.GetInstance().SwitchEndGame(true);
        }
    }
    public List<ComboCubeCtr> GetComboCubeCtr()
    {
        return comboCube;
    }
}
