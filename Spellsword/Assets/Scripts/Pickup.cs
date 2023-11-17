using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isHealth;
    public int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
