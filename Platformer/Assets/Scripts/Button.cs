using UnityEngine;

public class Button : MonoBehaviour
{
    public Gate gate;

    private bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet possibleBullet = collision.collider.GetComponent<Bullet>();
        if (possibleBullet != null && !possibleBullet.fromEnemy && !isTriggered)
        {
            isTriggered = true;

            gate.Open();
            UI.ScorePoints(50);
        }
    }
}
