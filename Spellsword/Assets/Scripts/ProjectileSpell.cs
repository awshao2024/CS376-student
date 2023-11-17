using UnityEngine;

public class ProjectileSpell : Spell
{
    public int projSpeed;
    public bool fromEnemy;

    public override void Cast()
    {
        Audio.SpellAudio();
        Projectile proj = Instantiate(prefab, transform.parent.position + transform.parent.right, Quaternion.identity).GetComponent<Projectile>();
        proj.SetDamage(Mathf.RoundToInt(baseDamage * damageMult));
        proj.fromEnemy = fromEnemy;
        proj.GetComponent<Rigidbody2D>().velocity = transform.parent.right * projSpeed;
    }
}