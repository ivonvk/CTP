using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectCtr : MonoBehaviour
{
    public Vector3 targetPos;
    public ParticleSystem particle;
    public float pushTimer = 0f;
    public virtual void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public virtual void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
        particle.Play();
    }
    public virtual void ResetObj()
    {
        targetPos = Vector3.zero;
        pushTimer = 0;
        PoolMgr.GetInstance().PushObj(gameObject);
    }
    public virtual void Update()
    {
        if (targetPos != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos,650f*Time.deltaTime);
        }
        if(particle.isPlaying&&Vector3.Distance(transform.position, targetPos) <= 1f)
        {
            pushTimer = 10f;
            particle.Stop();
        }
        if (!particle.isPlaying && pushTimer>0)
        {
            pushTimer -= 3f * Time.deltaTime ;

        }
        else if(!particle.isPlaying)
        {
            ResetObj();
        }
    }
}
