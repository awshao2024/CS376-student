using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Singleton;

    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text winText;
    public TMP_Text deathText;

    private Audio aud;
    private int score;
    private int health;

    public static void ScorePoints(int points)
    {
        Singleton.ScoreInternal(points);
    }

    public static void SetHealth(int hp)
    {
        Singleton.HealthInternal(hp);
    }

    public static void Win()
    {
        Singleton.WinInternal();
    }

    public static void Death()
    {
        Singleton.DeathInternal();
    }

    private void Start()
    {
        Singleton = this;

        winText.enabled = false;
        deathText.enabled = false;

        aud = FindObjectOfType<Audio>();

        ScoreInternal(0);
        HealthInternal(FindObjectOfType<Player>().health);
    }

    private void ScoreInternal(int points)
    {
        if(points < 0)
        {
            aud.LoseScoreAudio();
        } else if(points > 0)
        {
            aud.AddScoreAudio();
        }

        score += points;
        scoreText.text = "Score: " + score;
    }

    private void HealthInternal(int hp)
    {
        health = hp;
        healthText.text = "HP: " + health;
    }

    private void WinInternal()
    {
        winText.text = "You Won!";
        winText.enabled = true;
    }

    private void DeathInternal()
    {
        deathText.text = "You Died!";
        deathText.enabled = true;
    }
}
