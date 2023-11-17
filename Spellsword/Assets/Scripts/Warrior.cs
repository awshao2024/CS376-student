using UnityEngine;

public class Warrior : Enemy
{
    public float swingDelay;
    public Sword sword;
    private bool inRange = false;

    protected override void Move()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > stopDistance)
        {
            Vector2 dir = player.transform.position - transform.position;
            dir.Normalize();
            rb.velocity = dir * speed;
            inRange = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
            inRange = true;
        }
    }

    protected override void Attack()
    {
        if (inRange)
        {
            sword.DelayedSwingSword(swingDelay);
            nextAttack = Time.time + cooldown;
        }
    }
}
