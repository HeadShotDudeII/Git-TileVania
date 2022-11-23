using UnityEngine;

public class Bullets : MonoBehaviour
{
    Rigidbody2D bulletRigidBody;
    [SerializeField] float bulletSpeed = 10f;
    PlayerMovement playerMovement;

    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        bulletSpeed *= playerMovement.gameObject.transform.localScale.x;


    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidBody.velocity = new Vector2(bulletSpeed, 0f);

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.name.ToString());
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);

        }

        Destroy(gameObject);
    }
}
