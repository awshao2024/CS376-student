using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool fromEnemy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
