using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float delayTimer;
    public bool fromEnemy;
    protected int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("DestroyInternal", delayTimer);

        if ((collision.collider.CompareTag("Player") && fromEnemy) || (collision.collider.CompareTag("Enemy") && !fromEnemy))
        {
            ApplySpellEffects(collision.collider.GetComponent<Entity>());
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void DestroyInternal()
    {
        Destroy(gameObject);
    }

    public virtual void SetDamage(int dmg)
    {
        damage = dmg;
    }

    protected abstract void ApplySpellEffects(Entity target);
}
