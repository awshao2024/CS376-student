public class FireProjectile : Projectile
{
    public int tickDamage;
    public float dotDuration;

    protected override void ApplySpellEffects(Entity target)
    {
        target.TakeDamage(damage);
        target.TakeDamageOverTime(tickDamage, dotDuration);
    }
}
