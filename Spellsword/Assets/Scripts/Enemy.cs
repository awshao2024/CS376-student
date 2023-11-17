using UnityEngine;

public abstract class Enemy : Entity
{
    public float cooldown;
    public float stopDistance;
    public int goldReward;
    protected float nextAttack;
    protected Player player;
    protected ItemSpawner pickupSpawner;

    void Start()
    {
        pickupSpawner = FindObjectOfType<ItemSpawner>();
        rb = GetComponent<Rigidbody2D>();
        player = null;
        text.text = "";

        health = maxHealth;
        speed = baseSpeed;
        actionsAllowed = true;
        nextAttack = Time.time + 1f;
    }

    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }

        UI.SetObjectText(text, Camera.main.WorldToScreenPoint(transform.position + transform.up));

        if (actionsAllowed)
        {
            Track();
            Move();

            if (Time.time > nextAttack)
            {
                Attack();
            }
        }
    }

    protected override void Death()
    {
        Audio.KillAudio();
        player.AddGold(goldReward);
        pickupSpawner.MaybeDropPickup(transform.position);
        Destroy(text.gameObject);
        Destroy(gameObject);
        MapManager.CountEnemyKilled();
    }

    protected virtual void Track()
    {
        Vector2 toPlayer = player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg);
    }

    protected abstract void Move();

    protected abstract void Attack();
}
