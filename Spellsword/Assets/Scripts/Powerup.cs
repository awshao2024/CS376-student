using TMPro;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public int cost;
    [Multiline]
    public string nameString;
    public TMP_Text text;
    protected bool playerTrigger;
    protected Player player;

    void Start()
    {
        playerTrigger = false;
        text.text = nameString + "\n" + cost + " Gold";
        player = null;
    }

    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }

        UI.SetObjectText(text, Camera.main.WorldToScreenPoint(transform.position - transform.up));

        if(Input.GetKeyDown(KeyCode.F) && playerTrigger)
        {
            if(!TakeGold())
            {
                return;
            }

            Audio.PowerupAudio();
            Use();
            Destroy(text.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTrigger = false;
        }
    }

    protected virtual bool TakeGold()
    {
        return player.SpendGold(cost);
    }

    protected abstract void Use();
}
