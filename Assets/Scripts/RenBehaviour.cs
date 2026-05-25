using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RenBehaviour : MonoBehaviour
{
    [HideInInspector] public float timeScale = 1f;
    [HideInInspector] public float fixedTimeScale = 1f;

    protected virtual void OnEnable() 
    {
        if (TimeManager.Instance != null) TimeManager.Instance.Register(this);
    }

    protected virtual void OnDisable() 
    {
        if (TimeManager.Instance != null) TimeManager.Instance.Unregister(this);
    }

    public virtual void Tick(float deltaTime)
    {
        
    }

    public virtual void FixedTick(float fixedDeltaTime)
    {
        
    }
}