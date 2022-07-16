using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public List<GameObject> monstersPrefab;
    public List<GameObject> bossPrefab;
    public Room currentRoom;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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

