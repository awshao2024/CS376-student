using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 leftPosition;
    public Vector3 rightPosition;
    public float moveTime;

    private Vector3 speed;
    private Rigidbody2D rb;

    private void Start()
    {
        speed = (rightPosition - leftPosition) / moveTime;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.x <= leftPosition.x)
        {
            rb.velocity = speed;
        } else if(transform.position.x >= rightPosition.x)
        {
            rb.velocity = -speed;
        }
    }
}
