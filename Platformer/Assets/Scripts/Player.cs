using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public int health;
    public float bulletSpeed;
    public float reloadTime;

    public GameObject bulletPrefab;

    private float nextFire = 0f;
    private bool isJumping = false;
    private float flashEnd = 0f;
    private bool isHit = false;
    private bool isActive = true;

    private GameObject barrel;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Color defaultColor;

    private void Start()
    {
        barrel = transform.Find("Barrel").gameObject;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        defaultColor = sprite.color;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isActive)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector2 vel = rb.velocity;
                vel.x = -1 * speed;
                rb.velocity = vel;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Vector2 vel = rb.velocity;
                vel.x = speed;
                rb.velocity = vel;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Vector2 vel = rb.velocity;
                vel.y = jumpSpeed;
                rb.velocity = vel;

                isJumping = true;
            }

            RotateBarrel();

            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                nextFire = Time.time + reloadTime;
                Fire();
            }

            if (isHit && Time.time > flashEnd)
            {
                sprite.color = defaultColor;
                isHit = false;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                health = 999;
                UI.SetHealth(health);
            }
        }
    }

    private void RotateBarrel()
    {
        Vector2 toMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - barrel.transform.position;
        barrel.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg);
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (barrel.transform.right * .75f), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = barrel.transform.right * bulletSpeed;
    }

    private void Hit()
    {
        health--;
        UI.SetHealth(health);

        sprite.color = Color.red;
        flashEnd = Time.time + 0.25f;
        isHit = true;

        UI.ScorePoints(-10);

        if(health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isActive = false;
        UI.Death();
        Invoke("Restart", 3f);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.collider.tag == "Platform" || collision.collider.tag == "Wall") && collision.collider.friction > 0)
        {
            isJumping = false;
        }

        Bullet possibleBullet = collision.collider.GetComponent<Bullet>();
        if (possibleBullet != null && possibleBullet.fromEnemy && isActive)
        {
            Hit();
        }

        if (collision.collider.tag == "Orb")
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy e in enemies)
            {
                e.Deactivate();
            }

            collision.gameObject.SetActive(false);
            isActive = false;
            UI.Win();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pit")
        {
            GameOver();
        }
    }
}
