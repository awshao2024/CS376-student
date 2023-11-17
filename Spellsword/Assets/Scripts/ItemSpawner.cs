using TMPro;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int dropChance;
    private int difficultyLevel;
    private GameObject[] powerupPrefabs;
    private GameObject[] pickupPrefabs;
    private GameObject[] spawnZones;

    void Start()
    {
        difficultyLevel = Mathf.Min(MapManager.GetRoomCount() / 10, 2);
        spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");
        powerupPrefabs = new GameObject[12];
        pickupPrefabs = new GameObject[2];

        powerupPrefabs[0] = Resources.Load("HealthUpgrade") as GameObject;
        powerupPrefabs[1] = Resources.Load("StaminaUpgrade") as GameObject;
        powerupPrefabs[2] = Resources.Load("ManaUpgrade") as GameObject;
        powerupPrefabs[3] = Resources.Load("LongswordAbility") as GameObject;
        powerupPrefabs[4] = Resources.Load("EnergyBlastAbility") as GameObject;
        powerupPrefabs[5] = Resources.Load("TripleBlastAbility") as GameObject;
        powerupPrefabs[6] = Resources.Load("FireballAbility") as GameObject;
        powerupPrefabs[7] = Resources.Load("FrostflareAbility") as GameObject;
        powerupPrefabs[8] = Resources.Load("LightningStrikeAbility") as GameObject;
        powerupPrefabs[9] = Resources.Load("GreatswordAbility") as GameObject;
        powerupPrefabs[10] = Resources.Load("SwordUpgrade") as GameObject;
        powerupPrefabs[11] = Resources.Load("SpellUpgrade") as GameObject;
        pickupPrefabs[0] = Resources.Load("HealthPickup") as GameObject;
        pickupPrefabs[1] = Resources.Load("ManaPickup") as GameObject;
    }

    public void SpawnPowerups()
    {
        int bounds = 5;

        if(difficultyLevel == 1)
        {
            bounds = 10;
        }
        else if(difficultyLevel == 2)
        {
            bounds = 12;
        }

        for (int i = 0; i < spawnZones.Length; i++)
        {
            Powerup powerup = Instantiate(powerupPrefabs[Random.Range(0, bounds)], spawnZones[i].GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity).GetComponent<Powerup>();
            TMP_Text text = UI.CreateItemText();
            text.text = powerup.nameString + "\n" + powerup.cost + " Gold";
            text.horizontalAlignment = HorizontalAlignmentOptions.Center;
            powerup.text = text;
        }
    }

    public void MaybeDropPickup(Vector2 position)
    {
        int random = Random.Range(0, dropChance);

        if(random == 0)
        {
            int type = Random.Range(0, 3);

            if (type == 0)
            {
                Instantiate(pickupPrefabs[0], position, Quaternion.identity);
            }
            else
            {
                Instantiate(pickupPrefabs[1], position, Quaternion.identity);
            }
        }
    }
}
