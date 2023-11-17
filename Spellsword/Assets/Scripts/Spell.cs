using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public int baseDamage;
    public int manaCost;
    public GameObject prefab;
    protected float damageMult;

    void Start()
    {
        damageMult = 1f;
    }

    public virtual void AddDamageMultiplier(float ratio)
    {
        damageMult += ratio;
    }

    public abstract void Cast();
}
