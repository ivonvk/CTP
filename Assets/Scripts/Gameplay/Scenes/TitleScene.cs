using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{

    void Start()
    {
        Screen.SetResolution(1080, 1920, true);
        string Platform = "";
#if UNITY_EDITOR
        Platform = "EDITOR";
#elif UNITY_IPHONE
		Platform = "IPHONE";
#elif UNITY_ANDROID
		Platform = "ANDROID";
#endif
        Debug.Log(Platform);
        Debug.Log("dataPath: " + Application.dataPath);
        Debug.Log("persistentDataPath: " + Application.persistentDataPath);
        Debug.Log("streamingAssetsPath: " + Application.streamingAssetsPath);
        Debug.Log("temporaryCachePath: " + Application.temporaryCachePath);
    }
    public void ChangeToGameLobbyScene()
    {
        ScenesMgr.GetInstance().LoadScene(1);

    }
}
