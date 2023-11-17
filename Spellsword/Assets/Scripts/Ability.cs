using UnityEngine;

public class Ability : Powerup
{
    public bool isSpell;
    public GameObject prefab;

    protected override void Use()
    {
        if (isSpell)
        {
            player.SetSpell(prefab);
        }
        else
        {
            player.SetSword(prefab);
        }
    }
}
