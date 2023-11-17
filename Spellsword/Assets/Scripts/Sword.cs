using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int baseDamage;
    public float swingSpeed;
    public bool fromEnemy;

    private float damageMult;
    private int swordDir;
    private bool isSwinging;

    private SpriteRenderer ren;
    private Collider2D col;

    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        damageMult = 1f;
        swordDir = 1;
        isSwinging = false;
        Deactivate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Player") && fromEnemy) || (collision.collider.CompareTag("Enemy") && !fromEnemy))
        {
            collision.collider.GetComponent<Entity>().TakeDamage(Mathf.RoundToInt(baseDamage * damageMult));
        }
    }

    public void AddDamageMultiplier(float ratio)
    {
        damageMult += ratio;
    }

    public void DelayedSwingSword(float delay)
    {
        Prepare();
        Invoke("SwingSword", delay);
    }

    public void SwingSword()
    {
        if (!isSwinging)
        {
            Audio.SwordAudio();
            StartCoroutine(Swing());
        }
    }

    private void Activate()
    {
        ren.enabled = true;
        col.enabled = true;
    }


    private void Deactivate()
    {
        ren.enabled = false;
        col.enabled = false;
    }

    private void Prepare()
    {
        ren.enabled = true;
    }

    IEnumerator Swing()
    {
        Activate();
        isSwinging = true;
        transform.localRotation = Quaternion.Euler(0, 0, swordDir * 45);

        float stopTime = Time.time + swingSpeed;

        while (Time.time <= stopTime)
        {
            // yes I know I should've slerp'd
            float rot = transform.localRotation.eulerAngles.z;
            rot += (-1 * swordDir) * (90 / swingSpeed) * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, rot);
            yield return null;
        }

        swordDir *= -1;
        isSwinging = false;
        Deactivate();
    }
}
