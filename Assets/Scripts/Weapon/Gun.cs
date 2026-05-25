using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun", order = 1)]
public class Gun : Weapon
{
    public float range = 100f;

    public override void Attack(bool lookinAtSomething, RaycastHit lookinAt)
    {
        Transform transform = player.transform;
        onFire?.Invoke();

        Vector3 direction = lookinAtSomething ? (lookinAt.point - playerAttackPoint.position).normalized : transform.forward;

        Debug.Log(direction);

        RaycastHit hit;

        if(Physics.Raycast(playerAttackPoint.position, direction, out hit, range))
        {
            
        }

    }
}
