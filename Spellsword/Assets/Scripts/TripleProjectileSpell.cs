using UnityEngine;

public class TripleProjectileSpell : Spell
{
    public int projSpeed;
    public bool fromEnemy;

    public override void Cast()
    {
        Quaternion[] rotations = new Quaternion[] { Quaternion.Euler(0, 0, -30), Quaternion.identity, Quaternion.Euler(0, 0, 30) };

        Audio.SpellAudio();
        for (int i = 0; i < 3; i++)
        {
            Projectile proj = Instantiate(prefab, transform.parent.position + (rotations[i] * transform.parent.right), rotations[i]).GetComponent<Projectile>();
            proj.SetDamage(Mathf.RoundToInt(baseDamage * damageMult));
            proj.fromEnemy = fromEnemy;
            proj.GetComponent<Rigidbody2D>().velocity = rotations[i] * transform.parent.right * projSpeed;
        }
    }
}