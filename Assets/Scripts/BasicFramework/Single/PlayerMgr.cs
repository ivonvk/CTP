using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character
{
    public string CharacterName = "Ivon";
   public int Level = 0;
   
}
public class PlayerMgr : SingleMgr<PlayerMgr>
{
    private string playerName = "Chan";

    private int MasterLevel = 1;
    private int AP = 1;
    private int Grade;
    private List<string> Grades = new List<string> { "A", "B", "C", "D", "E" };

    private float CurrentEXP;
    private float MaxEXP;



    public void SetPlayerName(string str)
    {
        playerName = str;
    }
    public string GetPlayerName()
    {
        return playerName;
    }
    public int GetActionPoint()
    {
        return AP;
    }
    public int GetMasterLevel()
    {
        return MasterLevel;
    }
    public float GetCurrentEXP()
    {
        return CurrentEXP;
    }
    public float GetMaxEXP()
    {
        return MaxEXP;
    }
    public void CurrentEXPSetup(float f)
    {
        CurrentEXP += f;
        if (CurrentEXP > MaxEXP)
        {
            MasterLevel += 1;
            CurrentEXP = 0;
        }
    }
    public void SetGrade(int i)
    {
        Grade += i;
        if(Grade> Grades.Count)
        {
            Grade = Grades.Count;
        }
        if(Grade < 0)
        {
            Grade = 0;
        }
    }
    public string GetGrade()
    {
        return Grades[Grade];
    }


}
