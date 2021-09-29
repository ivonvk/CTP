using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SoldierCtr : MonoBehaviour
{

    public AnimationInstancing.AnimationInstancing[] anim;
    public NavMeshAgent nav;

    public GameObject PlayerBase;

    public bool GettingDmg = false;
    public SkinnedMeshRenderer skinnedMesh;
    public Material defaultMat, DmgMat;
    public Transform[] Characters;
    public Scrollbar HPBar;

    public float MaxHP = 10;
    public float CurrentHP = 10;
    public Vector3 LockUIRot;
    public Transform UICav;
    public BoxCollider boxCollider;
    public float DefalutWalkSpeed;

    public float AttackSpeed = 1f;

    float CurAtkSpeed = 0;

    public int Damage = 2;
    float WalkSpeedReset;

    float RandomWalkSpeed;
    public virtual void Awake()
    {
        UICav = transform.Find("EnemyInfo");
        HPBar = transform.Find("EnemyInfo").Find("Scrollbar").GetComponent<Scrollbar>();
        LockUIRot = transform.Find("EnemyInfo").rotation.eulerAngles;
        nav = GetComponent<NavMeshAgent>();
        DefalutWalkSpeed = nav.speed;
        anim = new AnimationInstancing.AnimationInstancing[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            anim[i] = transform.GetChild(i).GetComponent<AnimationInstancing.AnimationInstancing>();
        }
        Characters = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Characters[i] = transform.GetChild(i);
        }
        SetMatDefault();
        boxCollider = GetComponent<BoxCollider>();
    }
    public virtual void LateUpdate()
    {
        UICav.eulerAngles = LockUIRot;
    }
    public virtual void Update()
    {
        if (nav != null)
        {
            Attack();
        }
    }
    public virtual void FixedUpdate()
    {


        if (nav.enabled)
        {
            RandomWalkSpeed += 1f * Time.deltaTime;
            if (nav.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(nav.velocity, Vector3.up);
            }
            if (WalkSpeedReset > 0)
            {
                WalkSpeedReset -= 1f * Time.deltaTime;
            }
            else
            {
                nav.speed = DefalutWalkSpeed;
            }

            if (RandomWalkSpeed > 2f)
            {
                RandomWalkSpeed = 0;
                WalkSpeedIncrease(3.5f, Random.Range(0.75f, 1.25f));

                anim[0].PlayAnimation("walk");
            }
        }

    }

    public void Attack()
    {

        if (nav.isOnNavMesh && Vector3.Distance(transform.position, nav.destination) <= 70f && Vector3.Distance(transform.position, nav.destination) > 7f)
        {
            if (CurAtkSpeed <= 0)
            {
                GameMgr.GetInstance().GetPlayerCtr().GetDamage(Damage);
                CurAtkSpeed = AttackSpeed;
                anim[0].PlayAnimation("attack");
            }
            else
            {
                CurAtkSpeed -= 1f * Time.deltaTime;
                anim[0].PlayAnimation("idle");
            }
            nav.SetDestination(transform.position);

        }
    }
    public void WalkSpeedIncrease(float f, float t)
    {
        WalkSpeedReset = t;
        nav.speed += f;
    }
    public virtual void Start()
    {
        PlayerBase = GameObject.FindGameObjectWithTag("Base");
    }
    public virtual void SetupSoldier(Vector3 pos)
    {

        UICav.gameObject.SetActive(true);
        CurrentHP = MaxHP;
        transform.position = pos;
        nav.enabled = true;
        boxCollider.enabled = true;
        if (nav.isOnNavMesh)
        {
            nav.Warp(pos);
        }
        anim[0].PlayAnimation("idle");
        Invoke("SetupDes", 1f);

    }
    public virtual void SetupDes()
    {
        if (GameMgr.GetInstance().GetGameEnded())
        {
            return;
        }
        else
        {
            nav.SetDestination(PlayerBase.transform.position);

            anim[0].PlayAnimation("walk");
            WalkSpeedIncrease(70f, 3.25f);
        }


    }
    public float GetCurrentHP()
    {
        return CurrentHP;
    }
    public virtual bool GetDamage(int dmg)
    {

        CurrentHP -= dmg;
        HPBar.size = CurrentHP / MaxHP;
        if (CurrentHP <= 0 && nav.enabled)
        {
            anim[0].PlayAnimation("dying");
            SetMatDmg();
            Invoke("SetMatDefault", 0.25f);
            nav.enabled = false;
            boxCollider.enabled = false;
           
            UICav.gameObject.SetActive(false);
            GameMgr.GetInstance().GetLevelsOperate().RemoveSctr(this);
            Invoke("PushObj", 20f);
            return true;

        }
        else if (CurrentHP > 0)
        {
            PoolMgr.GetInstance().GetObj("Prefabs/Effect", (o) => { o.GetComponent<EffectCtr>().SetupEffect(transform.position + new Vector3(0, 1f, 0)); });

            SetMatDmg();
            Invoke("SetMatDefault", 0.25f);
        }
        return false;
    }
    public virtual void PushObj()
    {

        nav.enabled = false;

        PoolMgr.GetInstance().PushObj(gameObject);
    }
    public virtual void SetMatDefault()
    {
        anim[0].gameObject.SetActive(true);
        anim[1].gameObject.SetActive(false);
    }
    public virtual void SetMatDmg()
    {
        anim[0].gameObject.SetActive(false);
        anim[1].gameObject.SetActive(true);
    }

}
