using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class GetDataScript : SingleMgr<GetDataScript>
{


    public GetDataScript instance;

    SingleMgr<GetDataScript>.TextData[] textDatas;


    public GetDataScript()
    {

        LoadData();

    }
    public void DeleteData(string name)
    {
        File.Delete(Application.persistentDataPath + "/CustomText/" + "\\" + name + "_.dat");
    }
    public void SaveData(string name, SingleMgr<GetDataScript>.TextData TextData)
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/CustomText/");

        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = File.OpenWrite(Application.persistentDataPath + "/CustomText/" + "\\" + name + "_.dat"))
        {
            bf.Serialize(fs, TextData);
        }
        SingleMgr<GetDataScript>.TextData[] old = textDatas;
        textDatas = new SingleMgr<GetDataScript>.TextData[GetTextDatas().Length+1];
        int end = 1;
        for (int i = 0; i < textDatas.Length; i++)
        {
            if (textDatas[i] != null)
            {
                textDatas[i] = old[i];
            }
            end = i;
        }
        textDatas[end] = TextData;
        LoadData();
        //PanelMgr.GetInstance().GetPanelFunction("GameLobbyPanel").GetGameLobbyPanel().UpdateCustomTexts();
    }
    public void LoadData()
    {

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/CustomText/");
        BinaryFormatter bf = new BinaryFormatter();



        if (dir.Exists)
        {

            var files = dir.GetFiles().Where(o => o.Name.EndsWith(".dat")).ToArray();

            if (files.Length > 0)
            {
                SingleMgr<GetDataScript>.TextData t = new SingleMgr<GetDataScript>.TextData();
                textDatas = new TextData[files.Length];

                for (int i = 0; i < files.Length; i++)
                {

                    using (FileStream fs = File.OpenRead(files[i].FullName))
                    {

                        Debug.Log(t.GetType().FullName);
                        t = (SingleMgr<GetDataScript>.TextData)bf.Deserialize(fs);
                        textDatas[i] = t;
                    }
                }

            }
            else

            {
                textDatas = new SingleMgr<GetDataScript>.TextData[1];
                SingleMgr<GetDataScript>.TextData setup = new SingleMgr<GetDataScript>.TextData();
                setup.WordType = "Default";
                setup.text = new string[1];
                for(int i = 0; i < setup.text.Length ; i++)
                {
                    setup.text[i] = "Default";
                }
                SaveData( "Default", setup);
            }

        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/CustomText/");
        }


    }
    public SingleMgr<GetDataScript>.TextData[] GetTextDatas()
    {
        return textDatas;
    }
}
