using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMovement;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            rb.velocity = Vector2.zero;
            //animator.SetBool("isWalking", false);
            return;
        }
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        //animator.SetBool("isWalking", rb.velocity.magnitude > 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
}
