public class EnergyProjectile : Projectile
{
    protected override void ApplySpellEffects(Entity target)
    {
        target.TakeDamage(damage);
    }
}
