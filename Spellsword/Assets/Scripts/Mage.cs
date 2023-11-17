using UnityEngine;

public class Mage : Enemy
{
    public Spell spell;

    protected override void Move()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > stopDistance)
        {
            Vector2 dir = player.transform.position - transform.position;
            dir.Normalize();
            rb.velocity = dir * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override void Attack()
    {
        spell.Cast();
        nextAttack = Time.time + cooldown;
    }
}
