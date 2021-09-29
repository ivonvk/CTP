using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerCtr : MonoBehaviour
{
    public LineRenderer Line;
    public float lineWidth = 0.0225f;
    public float minimumVertexDistance = 0.01f;
    private bool SliderAttacking = false;
    private bool isLineStarted;

    public List<GameObject> SlashObjs = new List<GameObject>(new GameObject[100]);


    private float SlashPower = 100;

    private string CurrentInputStr;
    private int Combo = 0;

    private float Cooldown;
    private float SlashCooler;


    public float MaxHP = 100;
    public float CurHP;

    float colorDown;
    RaycastHit hit;
    Ray ray;
    public void AddCombo(int i)
    {
        Combo += i;
    }

    public float GetSlashPower()
    {
        return SlashPower;
    }
    public void AddSlashPower(int i)
    {
        SlashPower += i ;
    }
    public bool GetSliderAttacking()
    {
        return SliderAttacking;
    }
    public void SetSliderAttacking(bool b)
    {
        SliderAttacking = b;
    }
    void Start()
    {
        CurHP = MaxHP;

        Line = gameObject.AddComponent<LineRenderer>();

        Line.materials[0] = new Material(Shader.Find("Standard (Specular setup)"));
        Line.generateLightingData = true;
        Line.materials[0].color = new Color(1f, 0, 0, 0);
        // set the color of the line
        Line.startColor = new Color(1f, 0, 0, 0.5f);
        Line.endColor = new Color(1f, 0, 0, 0.5f);

        // set width of the renderer
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;

        isLineStarted = false;
        gameObject.layer = 10;
        Line.positionCount = 0;
    }
    public void CooldownSlash(bool slashed)
    {
        if (!slashed)
        {
            Cooldown = 1f;
        }
    }
    void Update()
    {
        if (colorDown > 0)
        {
            colorDown -= 2f * Time.deltaTime;
        }
        else
        {
            Line.materials[0].color = Color.red;
        }
        if(Cooldown > 0f)
        {
            Line.materials[0].color = Color.red;
            GameMgr.GetInstance().GetLevelsOperate().SetAllComboSlashed(false);
            Cooldown -= 1f * Time.deltaTime;
            Line.positionCount = 0;
            Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);

            Line.positionCount = 2;
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            for (int i = 0; i < SlashObjs.Count; i++)
            {
                SlashObjs[i] = null;
            }
            isLineStarted = false;
        }
 
        if (SlashPower > 0&&Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()&& Cooldown<=0f)
        {
            Line.positionCount = 0;
            Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);

            Line.positionCount = 2;
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            isLineStarted = true;


            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer;
            if (GetSliderAttacking())
            {
                layer = 1 << 6;
            }
            else
            {
                layer = 1 << 9;
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (!SlashObjs.Contains(hit.collider.gameObject))
                {
                    colorDown = 0.5f;
                    hit.collider.gameObject.GetComponent<SoldierCtr>().GetDamage(2);
                    Line.materials[0].color = Color.white;
                    /* for (int i = 0; i < SlashObjs.Count; i++)
                      {
                          if (!SlashObjs[i])
                          {
                              Line.materials[0].color = Color.white;
                              // SlashObjs[i] = hit.collider.gameObject;
                              if (hit.collider.gameObject.GetComponent<SoldierCtr>().GetDamage(2))
                              {
                                  //   SlashPower += 10f;


                                  if (SlashPower > 100)
                                  {
                                      SlashPower = 100f;
                                  }
                              }

                              colorDown = 0.5f;
                              break;
                          }
                      }*/
                }
            }
        }

        if (SlashPower > 0&&Input.GetMouseButton(0) && isLineStarted&& Cooldown <= 0f)
        {
            if (GetSliderAttacking())
            {
                SlashPower -= 2f * Time.deltaTime;
            }
            else
            {
                SlashPower -= 0.5f * Time.deltaTime;
            }
            Vector3 currentPos = GetWorldCoordinate(Input.mousePosition);
            float distance = Vector3.Distance(currentPos, Line.GetPosition(Line.positionCount - 1));

            if (distance > minimumVertexDistance)
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layer;
                if (GetSliderAttacking())
                {
                    layer = 1 << 6;
                }
                else
                {
                    layer = 1 << 9;
                }
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                {
                    if (!SlashObjs.Contains(hit.collider.gameObject))
                    {

                        colorDown = 0.5f;
                        hit.collider.gameObject.GetComponent<SoldierCtr>().GetDamage(2);
                        Line.materials[0].color = Color.white;
                      /* for (int i = 0; i < SlashObjs.Count; i++)
                        {
                            if (!SlashObjs[i])
                            {
                                Line.materials[0].color = Color.white;
                                // SlashObjs[i] = hit.collider.gameObject;
                                if (hit.collider.gameObject.GetComponent<SoldierCtr>().GetDamage(2))
                                {
                                    //   SlashPower += 10f;
                                   

                                    if (SlashPower > 100)
                                    {
                                        SlashPower = 100f;
                                    }
                                }

                                colorDown = 0.5f;
                                break;
                            }
                        }*/
                    }
                }
                UpdateLine();
            }
        }
        else
        {
            
            if (SlashCooler <5)
            {
                SlashCooler += 1f * Time.deltaTime;
                
            }
            else
            {
                SlashCooler = 0f;

                PanelMgr.GetInstance().GetPanelFunction("AlphaTypingPanel").GetAlphaTyping().SetSlashAnim("add");
                if (SlashPower < 0)
                {
                    SlashPower = 0;
                }
                if (SlashPower < 100)
                {
                    SlashPower += 10;
                }
                if (SlashPower > 100)
                {
                    SlashPower = 100f;
                }
            }
            Line.positionCount = 0;
            Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);

            Line.positionCount = 2;
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            for (int i = 0; i < SlashObjs.Count; i++)
            {
                SlashObjs[i] = null;
            }
            isLineStarted = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
         
            Line.positionCount = 0;
            Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);

            Line.positionCount = 2;
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            for (int i = 0; i < SlashObjs.Count; i++)
            {
                SlashObjs[i] = null;
            }

            isLineStarted = false;
        }
    }

    private void UpdateLine()
    {
        
        for(int i = 0; i < Line.positionCount; i++)
        {
            if (i + 1 < Line.positionCount)
            {
                Line.SetPosition(i, Line.GetPosition(i + 1));
            }
        }
        Line.positionCount++;
        Line.startWidth = 0.02f+lineWidth * Random.Range(0.5f,1f);
        Line.endWidth = lineWidth * Random.Range(0.5f, 1f);
        Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
        if (Line.positionCount > 50)
        {
            Line.positionCount = 2;
        }

    }

    public Vector3 GetWorldCoordinate(Vector3 mousePosition)
    {
        Vector3 mousePos = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public string AddCurrentInputStr(string str)
    {
        if (str != "")
        {
            CurrentInputStr += str;
        }
        else
        {
            CurrentInputStr = "";
            GameMgr.GetInstance().GetLevelsOperate().SetAllComboSlashed(false);
            GameMgr.GetInstance().GetLevelsOperate().AddSpawnTimer(15f);
        }

        
        return CurrentInputStr;
    }
    public string GetCurrentInputStr()
    {
        return CurrentInputStr;
    }
    
    
    public void GetDamage(float dmg)
    {
        CurHP -= dmg;
        PanelMgr.GetInstance().GetPanelFunction("AlphaTypingPanel").GetAlphaTyping().HPBarGetDmg(CurHP, MaxHP);
        if (CurHP <= 0f)
        {
            EventsCenter.GetInstance().TriggerEvent("GameEnd",false);
        }
    }
}

