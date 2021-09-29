using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(AnimationInstancing.AnimationManager.Instance.LoadAnimationAssetBundle(Application.streamingAssetsPath + "/AssetBundle/animationtexture"));
    }


}
