using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCubeCtr : SoldierCtr
{
    public GameObject[] DisplayCube;
    public ComboStats stats;
    private Text OrderText;
    public bool isSlashed = false;
    public override void Awake()
    {
        UICav = transform.Find("EnemyInfo");
        OrderText = transform.Find("EnemyInfo").Find("Image").Find("NumberOrderText").GetComponent<Text>();
        LockUIRot = transform.Find("EnemyInfo").rotation.eulerAngles;
        DisplayCube = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            DisplayCube[i] = transform.GetChild(i).GetComponent<MeshRenderer>().gameObject;
        }

        SetMatDefault();
        stats = new ComboStats();
        stats.combo = this;
    }
    public override void FixedUpdate()
    {

        return;
    }
    public void SetComboStats(string s)
    {

        // stats = cs;
        stats.text = s;
    }
    public bool GetSlash()
    {
        return isSlashed;
    }
    public void SetSlash(bool b)
    {
        isSlashed = b;
        if (!isSlashed)
        {
            SetMatDefault();
        }
    }

    public override bool GetDamage(int dmg)
    {
        if (isSlashed)
        {
            return false;
        }



        PanelMgr.GetInstance().GetPanelFunction("AlphaTypingPanel").GetAlphaTyping().SendAlphaClickMsg(stats);
        PoolMgr.GetInstance().GetObj("Prefabs/Effect", (o) => { o.GetComponent<EffectCtr>().SetupEffect(transform.position + new Vector3(0, 1f, 0)); });

   
      
        
        return true;
    }
    public void GetTap()
    {
        SetSlash(true);
        SetMatDmg();
        PoolMgr.GetInstance().GetObj("Prefabs/Effect", (o) => { o.GetComponent<EffectCtr>().SetupEffect(transform.position + new Vector3(0, 1f, 0)); });
    }
    public override void SetMatDefault()
    {
        DisplayCube[0].gameObject.SetActive(true);
        DisplayCube[1].gameObject.SetActive(false);
    }
    public override void SetMatDmg()
    {
        DisplayCube[0].gameObject.SetActive(false);
        DisplayCube[1].gameObject.SetActive(true);
    }
    public override void SetupSoldier(Vector3 pos)
    {

        UICav.gameObject.SetActive(true);
        transform.position = pos;
        Invoke("SetupDes", 1f);

    }
    public override void SetupDes()
    {


    }
    public void SetAlpha(string str)
    {
        stats.text = str;
        OrderText.text = str;

    }
    public override void PushObj()
    {
        SetMatDefault();

        PoolMgr.GetInstance().PushObj(gameObject);
    }
    public string GetComboCubeText()
    {
        return stats.text;
    }
}
