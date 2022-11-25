using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpSpeed = 30f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Transform bulletPos;
    //[SerializeField] TextMeshProUGUI playerLifes;

    // [SerializeField] float bulletSpeed = 10f;

    [SerializeField] GameObject bullet;

    float gravityScaleAtStart;
    bool isAlive = true;

    Vector2 playerMovement;
    Rigidbody2D rigidBodyPlayer;
    Vector2 deathKick = new Vector2(0, 20f);

    CapsuleCollider2D bodyCapsuleCollier;
    BoxCollider2D feetCollier;
    Animator animatorPlayer;
    void Start()
    {
        rigidBodyPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        bodyCapsuleCollier = GetComponent<CapsuleCollider2D>();
        feetCollier = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidBodyPlayer.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return; // if dont add this line the dead body will die again.
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }



    private void Die()
    {
        if (bodyCapsuleCollier.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Debug.Log("touched enemy");
            isAlive = false;
            animatorPlayer.SetTrigger("Die");
            rigidBodyPlayer.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }

    private void ClimbLadder()
    {
        if (!isAlive) return;
        if (!feetCollier.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animatorPlayer.SetBool("isClimbing", false);
            rigidBodyPlayer.gravityScale = gravityScaleAtStart;
            return;
        }
        bool isClimbing = Mathf.Abs(rigidBodyPlayer.velocity.y) > Mathf.Epsilon;
        animatorPlayer.SetBool("isClimbing", isClimbing);
        //animatorPlayer.SetBool("isClimbinga", true);



        rigidBodyPlayer.gravityScale = 0;
        rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, playerMovement.y * climbSpeed);
    }

    private void FlipSprite()
    {
        if (!isAlive) return;

        bool isRunning = Mathf.Abs(rigidBodyPlayer.velocity.x) > Mathf.Epsilon;
        if (isRunning)
        {
            transform.localScale = new Vector2(MathF.Sign(rigidBodyPlayer.velocity.x), 1f);

        }
    }

    private void Run()
    {
        if (!isAlive) return;

        rigidBodyPlayer.velocity = new Vector2(playerMovement.x * playerSpeed, rigidBodyPlayer.velocity.y);
        bool isRunning = Mathf.Abs(rigidBodyPlayer.velocity.x) > Mathf.Epsilon;
        animatorPlayer.SetBool("isRunning", isRunning);

    }

    void OnJump()
    {
        if (!isAlive) return;

        if (!feetCollier.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        rigidBodyPlayer.velocity += new Vector2(0f, jumpSpeed);


    }

    void OnMove(InputValue inputValue)
    {
        if (!isAlive) return;

        playerMovement = inputValue.Get<Vector2>();

    }

    void OnFire()
    {
        GameObject bulletCopy = Instantiate(bullet, bulletPos.position, transform.rotation);
        Debug.Log("Fired");

    }


}
