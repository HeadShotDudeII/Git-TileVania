using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyBody;
    BoxCollider2D enemyBoxCollider;
    [SerializeField] float speed = 5f;
    int direction = 1;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        FlipSprite();

    }

    private void move()
    {
        enemyBody.velocity = new Vector2(speed * direction, enemyBody.velocity.y);
    }

    private void FlipSprite()
    {
        if (enemyBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            direction *= -1;
            transform.localScale = new Vector2(direction, 1);

            //transform.localScale = new Vector2(-Math.Sign(enemyBody.velocity.x), 1);
        }
    }
}
