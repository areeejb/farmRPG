using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float climbSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool onLadder = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (onLadder)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(movement.x * speed, movement.y * climbSpeed);
        }
        else
        {
            rb.gravityScale = 1;
            rb.linearVelocity = movement * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}