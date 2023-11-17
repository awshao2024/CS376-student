using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip swordSwing;
    public AudioClip spellCast;
    public AudioClip dash;
    public AudioClip collide;
    public AudioClip pickup;
    public AudioClip powerup;
    public AudioClip kill;
    public AudioClip score;
    public AudioClip victory;
    public AudioClip death;
    public AudioClip victoryMusic;
    public AudioClip deathMusic;

    public static Audio Singleton;
    private AudioSource aus;

    void Start()
    {
        Singleton = this;
        aus = GetComponent<AudioSource>();
    }

    public static void SwordAudio()
    {
        Singleton.SwordAudioInternal();
    }

    public static void SpellAudio()
    {
        Singleton.SpellAudioInternal();
    }

    public static void DashAudio()
    {
        Singleton.DashAudioInternal();
    }

    public static void CollideAudio()
    {
        Singleton.CollideAudioInternal();
    }

    public static void PickupAudio()
    {
        Singleton.PickupAudioInternal();
    }

    public static void PowerupAudio()
    {
        Singleton.PowerupAudioInternal();
    }

    public static void KillAudio()
    {
        Singleton.KillAudioInternal();
    }

    public static void ScoreAudio()
    {
        Singleton.ScoreAudioInternal();
    }

    public static void WinAudio()
    {
        Singleton.WinAudioInternal();
    }

    public static void DeathAudio()
    {
        Singleton.DeathAudioInternal();
    }

    private void SwordAudioInternal()
    {
        aus.PlayOneShot(swordSwing);
    }

    private void SpellAudioInternal()
    {
        aus.PlayOneShot(spellCast);
    }

    private void DashAudioInternal()
    {
        aus.PlayOneShot(dash);
    }

    private void CollideAudioInternal()
    {
        aus.PlayOneShot(collide);
    }

    private void PickupAudioInternal()
    {
        aus.PlayOneShot(pickup);
    }

    private void PowerupAudioInternal()
    {
        aus.PlayOneShot(powerup);
    }

    private void KillAudioInternal()
    {
        aus.PlayOneShot(kill);
    }

    private void ScoreAudioInternal()
    {
        aus.PlayOneShot(score);
    }

    private void WinAudioInternal()
    {
        aus.PlayOneShot(victory);
        aus.PlayOneShot(victoryMusic);
    }

    private void DeathAudioInternal()
    {
        aus.PlayOneShot(death);
        aus.PlayOneShot(deathMusic);
    }
}
