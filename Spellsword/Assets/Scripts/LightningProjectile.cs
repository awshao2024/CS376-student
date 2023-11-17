public class LightningProjectile : Projectile
{
    public float stunDuration;

    protected override void ApplySpellEffects(Entity target)
    {
        target.TakeDamage(damage);
        target.TakeStun(stunDuration);
    }
}
