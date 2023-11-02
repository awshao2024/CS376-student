using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip addScore;
    public AudioClip loseScore;

    private AudioSource aus;

    private void Start()
    {
        aus = GetComponent<AudioSource>();
    }

    public void AddScoreAudio()
    {
        aus.PlayOneShot(addScore);
    }

    public void LoseScoreAudio()
    {
        aus.PlayOneShot(loseScore);
    }
}
