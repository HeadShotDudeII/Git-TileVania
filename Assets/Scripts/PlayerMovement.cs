using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    float gravityScaleAtStart;

    Vector2 playerMovement;
    Rigidbody2D rigidBodyPlayer;

    CapsuleCollider2D capsuleColliderPlayer;
    Animator animatorPlayer;
    void Start()
    {
        rigidBodyPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        capsuleColliderPlayer = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rigidBodyPlayer.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void ClimbLadder()
    {

        if (!capsuleColliderPlayer.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidBodyPlayer.gravityScale = gravityScaleAtStart;
            return;
        }
        bool isClimbing = Mathf.Abs(rigidBodyPlayer.velocity.y) > Mathf.Epsilon;
        animatorPlayer.SetBool("isClimbing", isClimbing);


        rigidBodyPlayer.gravityScale = 0;
        rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, playerMovement.y * climbSpeed);
    }

    private void FlipSprite()
    {
        bool isRunning = Mathf.Abs(rigidBodyPlayer.velocity.x) > Mathf.Epsilon;
        if (isRunning)
        {
            transform.localScale = new Vector2(MathF.Sign(rigidBodyPlayer.velocity.x), 1f);

        }
    }

    private void Run()
    {
        rigidBodyPlayer.velocity = new Vector2(playerMovement.x * playerSpeed, rigidBodyPlayer.velocity.y);
        bool isRunning = Mathf.Abs(rigidBodyPlayer.velocity.x) > Mathf.Epsilon;
        animatorPlayer.SetBool("isRunning", isRunning);

    }

    void OnJump()
    {
        if (!capsuleColliderPlayer.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        rigidBodyPlayer.velocity += new Vector2(0f, jumpSpeed);

    }

    void OnMove(InputValue inptuValue)
    {
        playerMovement = inptuValue.Get<Vector2>();

    }


}
