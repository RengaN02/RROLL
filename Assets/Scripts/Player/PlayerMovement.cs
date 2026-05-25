using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using Unity.Mathematics;

public class PlayerMovement : RenBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 10f;

    [Header("Dash Settings")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;

    private bool isDashing = false;
    private bool canDash = true;

    private PlayerController controller;
    private Rigidbody rb;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public override void Tick(float deltaTime)
    {
        if (isDashing) { return; }

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0; camRight.y = 0;
        camForward.Normalize(); camRight.Normalize();
        Vector3 direction = (camForward * controller.Vertical + camRight * controller.Horizontal).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if(!controller.RightClicking)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, deltaTime * 1000f);
                
            }
            rb.MovePosition(rb.position + direction * moveSpeed * deltaTime);
        }

        if (controller.DashRequested && canDash)
        {
            StartCoroutine(DashRoutine());
            controller.UseDashRequest();
        }

    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        canDash = false;
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(transform.forward * dashSpeed * TimeManager.deltaTime * timeScale, Space.World);
            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void Die()
    {
        GameManager.instance.meshAgents.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
