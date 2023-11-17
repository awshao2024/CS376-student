using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius;
    private int difficultyLevel;
    private GameObject[] enemyPrefabs;
    private GameObject[] spawnZones;

    void Start()
    {
        difficultyLevel = Mathf.Min(MapManager.GetRoomCount() / 10, 2);
        spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");
        enemyPrefabs = new GameObject[6];

        enemyPrefabs[0] = Resources.Load("ApprenticeMage") as GameObject;
        enemyPrefabs[1] = Resources.Load("Mercenary") as GameObject;
        enemyPrefabs[2] = Resources.Load("Mage") as GameObject;
        enemyPrefabs[3] = Resources.Load("Warrior") as GameObject;
        enemyPrefabs[4] = Resources.Load("Archmage") as GameObject;
        enemyPrefabs[5] = Resources.Load("Champion") as GameObject;
    }

    public int SpawnEnemies()
    {
        int count = 0;

        for(int i=0; i<MapManager.GetEnemiesCount(); i++)
        {
            int diffIndex = difficultyLevel * 2;
            GameObject prefab = enemyPrefabs[Random.Range(diffIndex, diffIndex+2)];
            Vector2 spawnPoint = GetSpawnPoint();

            if(spawnPoint != Vector2.zero)
            {
                Enemy enemy = Instantiate(prefab, spawnPoint, Quaternion.identity).GetComponent<Enemy>();
                TMP_Text text = UI.CreateEntityText();
                text.text = "";
                text.horizontalAlignment = HorizontalAlignmentOptions.Center;
                enemy.text = text;
                count++;
            }
        }

        return count;
    }

    private Vector2 GetSpawnPoint()
    {
        for(int i=0; i<10; i++)
        {
            Bounds zone = spawnZones[Random.Range(0, spawnZones.Length)].GetComponent<SpriteRenderer>().bounds;
            Vector2 spawnPoint = new Vector2(Random.Range(zone.min.x, zone.max.x), Random.Range(zone.min.y, zone.max.y));

            if (Physics2D.CircleCast(spawnPoint, spawnRadius, Vector2.right, 0).collider == null)
            {
                return spawnPoint;
            }
        }

        return Vector2.zero;
    }
}
