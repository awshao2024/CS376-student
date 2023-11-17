using UnityEngine;

public class Upgrade : Powerup
{
    public float ratio;
    public int amount;
    public UpgradeType type;

    public enum UpgradeType
    {
        Health,
        Stamina,
        Mana,
        SwordDamage,
        SpellDamage
    }

    protected override void Use()
    {
        switch(type)
        {
            case UpgradeType.Health:
                player.AddMaxHealth(amount);
                break;
            case UpgradeType.Stamina:
                player.AddMaxStamina(amount);
                break;
            case UpgradeType.Mana:
                player.AddMaxMana(amount);
                break;
            case UpgradeType.SwordDamage:
                player.AddDamageSword(ratio);
                break;
            case UpgradeType.SpellDamage:
                player.AddDamageSpell(ratio);
                break;
            default:
                break;
        }
    }
}
