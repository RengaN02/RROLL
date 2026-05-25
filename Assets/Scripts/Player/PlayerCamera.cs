using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : RenBehaviour
{
    private PlayerController controller;

    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        if(controller.RightClicking)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, 100))
            {
                controller.lookinAt = hit;
                controller.lookinAtSomething = true;
                Vector3 targetPosition = hit.point;

                targetPosition.y = transform.position.y;
                
                Vector3 direction = targetPosition - transform.position;
                if (direction != Vector3.zero) 
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, fixedDeltaTime * 500f);
                }
            }
        } else
        {
            controller.lookinAtSomething = false;
        }
    }
}
