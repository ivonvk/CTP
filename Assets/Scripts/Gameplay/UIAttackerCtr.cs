using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackerCtr : UIEffectCtr
{
    public override void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public override void Update()
    {
        if (targetPos != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 800f * Time.deltaTime);
        }
        if (particle.isPlaying && Vector3.Distance(transform.position, targetPos) <= 1f)
        {
            pushTimer = 10f;
            particle.Stop();
        }
        if (!particle.isPlaying && pushTimer > 0)
        {
            pushTimer -= 3f * Time.deltaTime;

        }
        else if (!particle.isPlaying)
        {
            ResetObj();
        }
    }
}
