using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;
    bool is_scored = false;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
            Scored();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Scored();
        }
    }

    private void Scored()
    {
        if(!is_scored)
        {
            is_scored = true;
            ScoreKeeper.AddToScore(GetComponent<Rigidbody2D>().mass);
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
