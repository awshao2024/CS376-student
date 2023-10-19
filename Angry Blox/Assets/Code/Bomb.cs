using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdImpulse = 5;
    public GameObject ExplosionPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D c in collision.contacts)
        {
            if(c.normalImpulse >= ThresholdImpulse)
            {
                Boom();
            }
        }
    }

    private void Boom()
    {
        GetComponent<PointEffector2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;

        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);

        Invoke("Destruct", 0.1f);
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }
}
