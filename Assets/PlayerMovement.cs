using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float climbSpeed = 3f;

    [Header("Map Bounds")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool onLadder = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
{
    Vector2 velocity;

    if (onLadder)
    {
        rb.gravityScale = 0f;
        velocity = new Vector2(movement.x * speed, movement.y * climbSpeed);
    }
    else
    {
        rb.gravityScale = 1f;
        velocity = movement * speed;
    }

    Vector2 nextPosition = rb.position + velocity * Time.fixedDeltaTime;

    nextPosition.x = Mathf.Clamp(nextPosition.x, minX, maxX);
    nextPosition.y = Mathf.Clamp(nextPosition.y, minY, maxY);

    rb.MovePosition(nextPosition);
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}