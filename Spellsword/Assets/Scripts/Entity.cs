using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int maxHealth;
    public float baseSpeed;
    public TMP_Text text;

    protected int health;
    protected float speed;
    protected bool actionsAllowed;
    protected Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        text.text = "";
    }

    void Update()
    {
        UI.SetObjectText(text, Camera.main.WorldToScreenPoint(transform.position + transform.up));
    }

    public virtual void TakeDamage(int dmg)
    {
        health = Mathf.Max(health - dmg, 0);

        if(health <= 0)
        {
            Death();
        }
    }

    public virtual void TakeDamageOverTime(int tickDmg, float duration)
    {
        text.text = "Burning!";
        StartCoroutine(DamageOverTime(tickDmg, duration));
    }

    public virtual void TakeSlow(float ratio, float duration)
    {
        speed *= (1 - ratio);
        text.text = "Slowed!";
        Invoke("ResetSpeed", duration);
    }

    public virtual void TakeStun(float duration)
    {
        rb.velocity = Vector2.zero;
        actionsAllowed = false;
        text.text = "Stunned!";
        Invoke("EndStun", duration);
    }

    protected virtual void ResetSpeed()
    {
        speed = baseSpeed;
        text.text = "";
    }

    protected virtual void EndStun()
    {
        actionsAllowed = true;
        text.text = "";
    }

    protected abstract void Death();

    IEnumerator DamageOverTime(int tickDmg, float duration)
    {
        for(int i=0; i<duration*2; i++)
        {
            health = Mathf.Max(health - tickDmg, 0);

            if(health <= 0)
            {
                Death();
            }

            yield return new WaitForSeconds(0.5f);
        }

        text.text = "";
    }
}
