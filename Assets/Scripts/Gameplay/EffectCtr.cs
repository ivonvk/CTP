using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtr : MonoBehaviour
{
    public float DestroyTimer = 1f;

    float Timer;
    AudioSource sound;
    void Awake()
    {

        sound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Timer = DestroyTimer;
    }
    private void FixedUpdate()
    {

        Timer -= 1f * Time.deltaTime;
        if (Timer <= 0)
        {
            
            PoolMgr.GetInstance().PushObj(gameObject);
        }
    }
    public void SetupEffect(Vector3 pos)
    {
        sound.pitch = Random.Range(0.5f, 2f);
        transform.position = pos;
        Timer = DestroyTimer;
        GetComponent<Light>().range = 0f;
    }
}
