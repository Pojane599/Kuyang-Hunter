using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerController playerController;
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    // Input
    private float mobileInputX = 0f;
    private Vector2 moveInput;
    private bool isJumping = false;

    private enum MovementState { idle, run, jump, fall }

    [Header("Jump Settings")]
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();

        playerController.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerController.Movement.Move.canceled += ctx => moveInput = Vector2.zero;

        playerController.Movement.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            moveInput = new Vector2(mobileInputX, 0f);
        }
        else
        {
            moveInput = playerController.Movement.Move.ReadValue<Vector2>();
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true);
        }
        // Attack();
    }

    private void FixedUpdate()
    {
        // Debug.Log($"[FixedUpdate] moveInput.x: {moveInput.x}, moveSpeed: {moveSpeed}, rb.velocity.x: {rb.velocity.x}");

        Vector2 targetVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;

        UpdateAnimation();

        if (isGrounded() && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJumping = false;
        }
    }


    private void UpdateAnimation()
    {
        MovementState state;

        float horizontal = moveInput.x;

        if (horizontal > 0f)
        {
            state = MovementState.run;
            sprite.flipX = false;
        }
        else if (horizontal < 0f)
        {
            state = MovementState.run;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    // ======= MOBILE INPUT METHODS FOR UI BUTTONS =======

    public void MoveLeft(bool isPressed)
    {
        Debug.Log("MoveLeft: " + isPressed); 
        if (isPressed)
            mobileInputX = -1f;
        else if (mobileInputX == -1f)
            mobileInputX = 0f;
    }

    public void MoveRight(bool isPressed)
    {
        Debug.Log("MoveRight: " + isPressed); 
        if (isPressed)
            mobileInputX = 1f;
        else if (mobileInputX == 1f)
            mobileInputX = 0f;
    }


    public void MobileJump()
    {
        if (isGrounded())
        {
            Jump();
        }
    }

    public void attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameObject in enemiesHit)
        {
            Debug.Log("Hit enemy");
            EnemyHealth enemyHealth = enemyGameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Gunakan method TakeDamage()
            }
        }
    }

    public void endAttack()
    {
        anim.SetBool("attack", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    // public void TakeDamage(int amount)
    // {
    //     health -= amount;
    //     if (health <= 0)
    //     {
    //         Die();
    //     }
    // }
}
