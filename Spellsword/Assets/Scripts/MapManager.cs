using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Singleton;

    private int roomCount;
    private int roomEnemies;
    private int currentRoom;
    private bool foundShop;
    private string[] sceneNames;

    void Awake()
    {
        Singleton = this;
        roomCount = 0;
        roomEnemies = 1;
        currentRoom = -1;
        foundShop = false;
        sceneNames = new string[] {"Room1", "Room2", "Room3", "Shop", "End"};
    }

    public static int GetRoomCount()
    {
        return Singleton.roomCount;
    }

    public static int GetEnemiesCount()
    {
        return Singleton.roomEnemies;
    }

    public static void CountEnemyKilled()
    {
        Singleton.EnemyKilledInternal();
    }

    public static void EnterNewRoom()
    {
        Singleton.NewRoomInternal();
    }

    private void EnemyKilledInternal()
    {
        roomEnemies--;
    }

    private void NewRoomInternal()
    {
        GameObject[] shopTexts = GameObject.FindGameObjectsWithTag("PowerupText");

        foreach(GameObject puText in shopTexts)
        {
            Destroy(puText);
        }

        Powerup[] shopPowerups = FindObjectsOfType<Powerup>();

        foreach (Powerup pu in shopPowerups)
        {
            Destroy(pu.gameObject);
        }

        roomCount++;
        int roomId = Random.Range(0, 4);

        if(roomCount % 10 == 0)
        {
            foundShop = false;
        }

        if (!foundShop && roomCount % 10 == 9)
        {
            roomId = 3;
        }

        if(roomCount == 30)
        {
            roomId = 4;
        }

        if(roomId == 3)
        {
            foundShop = true;
        }

        currentRoom = roomId;
        roomEnemies = 1 + Mathf.RoundToInt((roomCount % 10) * 0.5f);

        UI.ScorePoints(roomCount);
        Audio.ScoreAudio();
        StartCoroutine(LoadNewScene(roomId));
    }

    IEnumerator LoadNewScene(int sceneId)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneNames[sceneId], LoadSceneMode.Single);

        while(!sceneLoad.isDone)
        {
            yield return null;
        }

        UI.SetCanvas(FindObjectOfType<Canvas>());
        FindObjectOfType<Player>().transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;

        if (sceneId < 3)
        {
            roomEnemies = FindObjectOfType<EnemySpawner>().SpawnEnemies();
        }
        else if(sceneId == 3)
        {
            roomEnemies = 0;
            FindObjectOfType<ItemSpawner>().SpawnPowerups();
        }
        else
        {
            roomEnemies = 0;
            UI.WinText();
            Audio.WinAudio();
        }

        yield return new WaitForEndOfFrame();
    }
}
