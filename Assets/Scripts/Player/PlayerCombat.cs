using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : RenBehaviour
{
    public Transform attackPoint;

    public CameraShake cameraShake;

    public Weapon weapon;

    private PlayerController controller;

    private float nextAttackTime = 0f;

    void Awake()
    {
        controller = GetComponent<PlayerController>();

        weapon.Initialize(gameObject, attackPoint, OnFire, AfterHit);
    }

    public override void Tick(float deltaTime)
    {
        if (controller.FireRequested)
        {
            if(Time.time >= nextAttackTime)
            {
                weapon.Attack(controller.lookinAtSomething, controller.lookinAt); 
                nextAttackTime = Time.time + weapon.cooldown;
            }
            controller.UseFireRequest();
        }
    }

    public void OnFire()
    {
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(0.03f, 0.08f));
        }
    }

    public void AfterHit()
    {
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(0.1f, 0.2f));
        }
    }

}
