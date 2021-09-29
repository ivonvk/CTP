using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCubeCtr : MonoBehaviour
{
    public Image WordImage;
    public string WordsType;
    public string TargetWords;
    string[] TargetTexts;
    public void SetWordType(string s)
    {
        TargetWords = s;


        TextAsset TargetTitle = Resources.Load<TextAsset>("Text/" + WordsType);
        TargetTexts = TargetTitle.ToString().Split(new char[] { '\n' });
        
    }


}
