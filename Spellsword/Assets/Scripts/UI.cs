using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Singleton;

    //public TMP_Text scoreText;
    public GameObject itemTextPrefab;
    public GameObject entityTextPrefab;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text manaText;
    public TMP_Text goldText;
    public TMP_Text spellSelectText;
    public TMP_Text winText;
    public TMP_Text deathText;

    private int score;
    private int health;
    private int maxHealth;
    private int stamina;
    private int maxStamina;
    private int mana;
    private int maxMana;
    private int gold;
    private Canvas canvas;
    private Player player;

    private void Start()
    {
        Singleton = this;
        canvas = FindObjectOfType<Canvas>();
        player = FindObjectOfType<Player>();

        ScoreInternal(0);
        HealthInternal(player.maxHealth, player.maxHealth);
        StaminaInternal(player.maxStamina, player.maxStamina);
        ManaInternal(player.maxMana, player.maxMana);
        GoldInternal(0);
        spellSelectText.enabled = false;
        winText.enabled = false;
        deathText.enabled = false;
    }

    public static void SetCanvas(Canvas newCanvas)
    {
        Singleton.SetCanvasInternal(newCanvas);
    }

    public static TMP_Text CreateItemText()
    {
        return Singleton.CreateItemTextInternal();
    }

    public static TMP_Text CreateEntityText()
    {
        return Singleton.CreateEntityTextInternal();
    }

    public static void SetObjectText(TMP_Text text, Vector3 position)
    {
        Singleton.SetObjectTextInternal(text, position);
    }

    public static void ScorePoints(int amount)
    {
        Singleton.ScoreInternal(amount);
    }

    public static void SetHealth(int hp, int max)
    {
        Singleton.HealthInternal(hp, max);
    }

    public static void SetStamina(int stam, int max)
    {
        Singleton.StaminaInternal(stam, max);
    }

    public static void SetMana(int mp, int max)
    {
        Singleton.ManaInternal(mp, max);
    }

    public static void SetGold(int amount)
    {
        Singleton.GoldInternal(amount);
    }

    public static void EnableSpellSelect()
    {
        Singleton.EnableSpellSelectInternal();
    }

    public static void DisableSpellSelect()
    {
        Singleton.DisableSpellSelectInternal();
    }

    public static void WinText()
    {
        Singleton.WinTextInternal();
    }

    public static void DeathText()
    {
        Singleton.DeathTextInternal();
    }

    private void SetCanvasInternal(Canvas newCanvas)
    {
        canvas = newCanvas;
    }

    private TMP_Text CreateItemTextInternal()
    {
        return Instantiate(itemTextPrefab, canvas.transform).GetComponent<TMP_Text>();
    }

    private TMP_Text CreateEntityTextInternal()
    {
        return Instantiate(entityTextPrefab, canvas.transform).GetComponent<TMP_Text>();
    }

    private void SetObjectTextInternal(TMP_Text text, Vector3 position)
    {
        text.transform.position = position;
    }

    private void ScoreInternal(int amount)
    {
        score = amount;
        scoreText.text = "Score: " + score;
    }

    private void HealthInternal(int hp, int max)
    {
        health = hp;
        maxHealth = max;
        healthText.text = "Health: " + health + " / " + maxHealth;
    }

    private void StaminaInternal(int stam, int max)
    {
        stamina = stam;
        maxStamina = max;
        staminaText.text = "Stamina: " + stamina + " / " + maxStamina;
    }

    private void ManaInternal(int mp, int max)
    {
        mana = mp;
        maxMana = max;
        manaText.text = "Mana: " + mana + " / " + maxMana;
    }

    private void GoldInternal(int amount)
    {
        gold = amount;
        goldText.text = "Gold: " + gold;
    }

    private void EnableSpellSelectInternal()
    {
        spellSelectText.enabled = true;
    }

    private void DisableSpellSelectInternal()
    {
        spellSelectText.enabled = false;
    }

    private void WinTextInternal()
    {
        winText.enabled = true;
    }

    private void DeathTextInternal()
    {
        deathText.enabled = true;
    }
}
