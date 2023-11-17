public class IceProjectile : Projectile
{
    public float slowRate;
    public float slowDuration;

    protected override void ApplySpellEffects(Entity target)
    {
        target.TakeDamage(damage);
        target.TakeSlow(slowRate, slowDuration);
    }
}
