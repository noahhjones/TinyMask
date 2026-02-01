using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMovement;
    bool isFacingRight = true;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsWalking", false);
            return;
        }

        Flip();

        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        animator.SetBool("IsWalking", rb.velocity.magnitude > 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            sprite.flipX = !sprite.flipX;
        }
    }
}
