using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : ScriptableObject
{
    public int damage = 20;
    public float cooldown = 0.5f;

    public GameObject prefab;

    [NonSerialized] public GameObject weapon;

    [NonSerialized] public UnityEvent onFire = new UnityEvent();
    [NonSerialized] public UnityEvent afterHit = new UnityEvent();

    [NonSerialized] public bool initialized = false;

    [NonSerialized] public GameObject player;
    [NonSerialized] public Transform playerAttackPoint;

    public virtual void Initialize(GameObject player, Transform playerAttackPoint, UnityAction onFire, UnityAction afterHit)
    {
        if(initialized) return;
        SetOwner(player, playerAttackPoint, onFire, afterHit);
        initialized = true;
    }

    public virtual void Pickup(GameObject player, Transform playerAttackPoint, UnityAction onFire, UnityAction afterHit)
    {
        if(player != null) return;
        SetOwner(player, playerAttackPoint, onFire, afterHit);
    }

    public virtual void Drop()
    {
        ReleaseOwner();
    }

    void SetOwner(GameObject player, Transform playerAttackPoint, UnityAction onFire, UnityAction afterHit)
    {
        this.onFire.AddListener(onFire);
        this.afterHit.AddListener(afterHit);
        this.player = player;
        this.playerAttackPoint = playerAttackPoint;
        CreateObject();
    }

    void ReleaseOwner()
    {
        onFire.RemoveAllListeners();
        afterHit.RemoveAllListeners();
        player = null;
        playerAttackPoint = null;
        DestroyObject();
    }

    void CreateObject()
    {

        
    }

    void DestroyObject()
    {
        
    }

    public virtual void Attack(bool lookinAtSomething, RaycastHit lookinAt)
    {
        
    }

}
