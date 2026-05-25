using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : RenBehaviour
{
    PlayerCombat combat;
    PlayerMovement movement;

    public RaycastHit lookinAt;
    public bool lookinAtSomething;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public bool DashRequested { get; private set; }

    public bool FireRequested { get; private set; }

    public bool RightClicking { get; private set; }

    void Awake()
    {
        combat = GetComponent<PlayerCombat>();
        movement = GetComponent<PlayerMovement>();
    }

    public override void Tick(float deltaTime)
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashRequested = true;
        }

        if (Input.GetButton("Fire1"))
        {
            FireRequested = true;
        }

        RightClicking = Input.GetButton("Fire2");
    }

    public void UseDashRequest() => DashRequested = false;

    public void UseFireRequest() => FireRequested = false;
}
