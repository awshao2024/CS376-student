using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public float stopDistance;
    public float bulletSpeed;
    public float reloadTime;

    public GameObject bulletPrefab;

    private bool isActive = false;
    private float nextFire = 0f;
    private float flashEnd = 0f;
    private bool isHit = false;

    private GameObject barrel;
    private Rigidbody2D rb;
    private Player player;
    private SpriteRenderer sprite;
    private Color defaultColor;
    private Quaternion defaultRotation;

    public void Deactivate()
    {
        isActive = false;
    }

    private void Start()
    {
        barrel = transform.Find("Barrel").gameObject;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        sprite = GetComponent<SpriteRenderer>();
        defaultColor = sprite.color;
        defaultRotation = barrel.transform.rotation;
    }


    private void Update()
    {
        if (isHit && Time.time > flashEnd)
        {
            sprite.color = defaultColor;
            isHit = false;
        }

        if (isActive)
        {
            MoveTowardPlayer();
            RotateBarrel();

            if (Time.time > nextFire)
            {
                nextFire = Time.time + reloadTime;
                Fire();
            }
        }
    }

    private void RotateBarrel()
    {
        Vector2 toPlayer = player.transform.position - transform.position;
        barrel.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg);
    }

    private void MoveTowardPlayer()
    {
        float dist = Mathf.Abs(player.transform.position.x - transform.position.x);
        float dir = (player.transform.position.x - transform.position.x) / dist;

        if(dist > stopDistance)
        {
            Vector2 vel = rb.velocity;
            vel.x = dir * speed;
            rb.velocity = vel;
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (barrel.transform.right * .75f), Quaternion.identity);
        bullet.GetComponent<Bullet>().fromEnemy = true;
        bullet.GetComponent<Rigidbody2D>().velocity = barrel.transform.right * bulletSpeed;
    }

    private void Hit()
    {
        health--;
        sprite.color = Color.red;
        flashEnd = Time.time + 0.25f;
        isHit = true;

        if (health <= 0)
        {
            UI.ScorePoints(20);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet possibleBullet = collision.collider.GetComponent<Bullet>();
        if (possibleBullet != null && !possibleBullet.fromEnemy)
        {
            Hit();
            isActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            barrel.transform.rotation = defaultRotation;
            isActive = false;
        }
    }
}
