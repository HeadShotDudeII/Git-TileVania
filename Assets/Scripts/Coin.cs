using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D coinRigidbody2d;
    [SerializeField] AudioClip coinSFX;



    // Start is called before the first frame update
    void Start()
    {
        coinRigidbody2d = GetComponent<Rigidbody2D>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddScore();
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
