using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public int maxStamina;
    public int maxMana;
    public int stamRegenRate;
    public int dashCost;
    public float dashDistance;
    public Sword sword;
    public Spell spell1;
    public Spell spell2;

    private int stamina;
    private int mana;
    private int gold;
    private bool selectingSpell;
    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        text.text = "";

        spell2 = null;
        health = maxHealth;
        stamina = maxStamina;
        mana = maxMana;
        speed = baseSpeed;
        gold = 0;
        selectingSpell = false;
        isDead = false;
        actionsAllowed = true;

        StartCoroutine(RegenStamina());
    }

    void Update()
    {
        UI.SetObjectText(text, Camera.main.WorldToScreenPoint(transform.position + transform.up));

        if(isDead)
        {
            actionsAllowed = false;
        }

        if (actionsAllowed)
        {
            Rotate();
            Move();

            if(Input.GetKeyDown(KeyCode.Space) && stamina >= dashCost)
            {
                stamina -= dashCost;
                UI.SetStamina(stamina, maxStamina);
                StartCoroutine(Dash());
            }

            if(Input.GetKeyDown(KeyCode.Q) && !selectingSpell && spell1 != null && mana >= spell1.manaCost)
            {
                mana -= spell1.manaCost;
                UI.SetMana(mana, maxMana);
                spell1.Cast();
            }

            if(Input.GetKeyDown(KeyCode.E) && !selectingSpell && spell2 != null && mana >= spell2.manaCost)
            {
                mana -= spell2.manaCost;
                UI.SetMana(mana, maxMana);
                spell2.Cast();
            }

            if(Input.GetMouseButtonDown(0))
            {
                sword.SwingSword();
            }

            if(Input.GetKeyDown(KeyCode.Period))
            {
                health = maxHealth;
                UI.SetHealth(health, maxHealth);
            }
        }
    }

    public override void TakeDamage(int dmg)
    {
        health = Mathf.Max(health - dmg, 0);

        if (health <= 0 && !isDead)
        {
            Death();
        }

        UI.SetHealth(health, maxHealth);
    }

    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        health += amount;
        UI.SetHealth(health, maxHealth);
    }

    public void AddMaxStamina(int amount)
    {
        maxStamina += amount;
        stamina += amount;
        UI.SetStamina(stamina, maxStamina);
    }

    public void AddMaxMana(int amount)
    {
        maxMana += amount;
        mana += amount;
        UI.SetMana(mana, maxMana);
    }

    public void AddDamageSword(float ratio)
    {
        sword.AddDamageMultiplier(ratio);
    }

    public void AddDamageSpell(float ratio)
    {
        if(spell1 != null && spell2 == null) 
        {
            spell1.AddDamageMultiplier(ratio);
        }
        else if(spell1 == null && spell2 != null)
        {
            spell2.AddDamageMultiplier(ratio);
        }
        else if(spell1 != null && spell2 != null)
        {
            StartCoroutine(SpellSelect(null, ratio));
        }
    }

    public void SetSword(GameObject prefab)
    {
        actionsAllowed = false;
        sword = Instantiate(prefab, transform).GetComponent<Sword>();
        sword.transform.localPosition = new Vector2(sword.GetComponent<BoxCollider2D>().size.x / 2, 0);
        sword.gameObject.layer = LayerMask.NameToLayer("PlayerSword");
        actionsAllowed = true;
    }

    public void SetSpell(GameObject prefab)
    {
        actionsAllowed = false;
        Spell newSpell = Instantiate(prefab, transform).GetComponent<Spell>();

        if (spell1 == null)
        {
            spell1 = newSpell;
        }
        else if (spell2 == null)
        {
            spell2 = newSpell;
        }
        else
        {
            StartCoroutine(SpellSelect(newSpell));
        }

        actionsAllowed = true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UI.SetGold(gold);
    }

    public bool SpendGold(int amount)
    {
        if(gold < amount)
        {
            return false;
        }
        else
        {
            gold -= amount;
            UI.SetGold(gold);
            return true;
        }
    }

    protected override void Death()
    {
        isDead = true;
        actionsAllowed = false;
        StopAllCoroutines();
        UI.DeathText();
        Audio.DeathAudio();
    }

    private void Rotate()
    {
        Vector2 toMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg);
    }

    private void Move()
    {
        Vector2 vel = rb.velocity;
        int movingX = 0;
        int movingY = 0;

        if(Input.GetKey(KeyCode.W))
        {
            movingY++;
            vel.y = speed;
        } 

        if(Input.GetKey(KeyCode.A))
        {
            movingX++;
            vel.x = -1 * speed;
        }

        if(Input.GetKey(KeyCode.S))
        {
            movingY++;
            vel.y = -1 * speed;
        }

        if(Input.GetKey(KeyCode.D))
        {
            movingX++;
            vel.x = speed;
        }

        if(movingX % 2 == 0)
        {
            vel.x = 0;
        }

        if(movingY % 2 == 0)
        {
            vel.y = 0;
        }

        rb.velocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pickup"))
        {
            Audio.PickupAudio();
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            if(pickup.isHealth)
            {
                health = Mathf.Min(health + pickup.amount, maxHealth);
                UI.SetHealth(health, maxHealth);
            }
            else
            {
                mana = Mathf.Min(mana + pickup.amount, maxMana);
                UI.SetMana(mana, maxMana);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Door") && MapManager.GetEnemiesCount() <= 0)
        {
            MapManager.EnterNewRoom();
        }
        else
        {
            Audio.CollideAudio();
        }
    }

    IEnumerator Dash()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();
        actionsAllowed = false;

        Audio.DashAudio();
        rb.velocity = dir * dashDistance * 10;
        yield return new WaitForSeconds(0.1f);

        rb.velocity = Vector2.zero;
        actionsAllowed = true;
    }

    IEnumerator RegenStamina()
    {
        while(UI.Singleton == null)
        {
            yield return null;
        }

        while(true)
        {
            stamina = Mathf.Min(stamina + Mathf.RoundToInt(stamRegenRate * 0.1f), maxStamina);
            UI.SetStamina(stamina, maxStamina);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SpellSelect(Spell newSpell, float ratio = 0)
    {
        selectingSpell = true;
        UI.EnableSpellSelect();

        bool waiting = true;
        while(waiting)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if (newSpell == null)
                {
                    spell1.AddDamageMultiplier(ratio);
                }
                else
                {
                    spell1 = newSpell;
                }
                waiting = false;
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                if (newSpell == null)
                {
                    spell2.AddDamageMultiplier(ratio);
                }
                else
                {
                    spell2 = newSpell;
                }
                waiting = false;
            }
            else
            {
                yield return null;
            }
        }

        UI.DisableSpellSelect();
        selectingSpell = false;
    }
}
