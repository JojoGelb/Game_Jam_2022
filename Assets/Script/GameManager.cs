using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject player;
    public List<GameObject> monstersPrefab;
    public List<GameObject> bossPrefab;
    public List<GameObject> itemsPrefab;
    [SerializeField]
    private Text frameRateText;


    [Header("Dont touch Zone")]
    public Room currentRoom;
    public bool pause = false;
    public AudioManager audioManager;

    private float timer = 0;
    int frame = 0;
    private void Update()
    {
        frameRateText.text = (1.0f/Time.deltaTime).ToString() + " fps";
        /*timer += Time.deltaTime;
        frame++;
        if(timer >= 1)
        {
            frameRateText.text = frame.ToString() + " fps";
            frame = 0;
            timer = 0;
        }*/
    }

    private void Awake()
    {
        if(instance == null)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            instance = this;
            audioManager = GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("Dupplicated GameManager Singleton, ignoring this one", gameObject);
        }
    }
    public void removeMonster(Entity monster)
    {
        print("i removed a monster");
        currentRoom.monsters.Remove(monster.gameObject);
        if(currentRoom.monsters.Count == 0 && currentRoom.roomType == Room.RoomType.normal)
        {
            currentRoom.roomCleared();
        }
    }

    public void removeBoss(Entity boss)
    {
        currentRoom.bossAlive = 0;
        currentRoom.roomCleared();
    }
}

