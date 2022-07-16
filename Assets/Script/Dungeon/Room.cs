using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room: MonoBehaviour
{
    public enum RoomState {ready, onGoing, finished};
    public enum RoomType {normal, spawn, chest, boss};

    [Header("Infos a ne pas changer")]
    public Vector2Int position;
   
    public bool openingL = false;
    public bool openingR = false;
    public bool openingT= false;
    public bool openingB = false;

    public int tailleCarreau = 4;

    private int maxRoomSize;
    public int width;

    public RoomType roomType = RoomType.normal;
    public RoomState roomState = RoomState.ready;
    private List<DoorTrigger> doorsTrigger = new List<DoorTrigger>();
    public List<GameObject> monsters;
    public int bossAlive = 0;

    public int maxNbMonsters;

    [Header("Rooms prefabs")]
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject doorPrefab;

    public virtual void setRoom(Vector2Int position, int width, int maxRoomSize, RoomType type)
    {
        roomType = type;
        this.width = width;
        this.maxRoomSize = maxRoomSize;

        transform.rotation = Quaternion.identity;
        this.position = position;

        if (type == RoomType.boss)
        {
            setUpBossRoom();
            return;
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(((maxRoomSize - width)/2)*tailleCarreau + i * tailleCarreau , ((maxRoomSize - width) / 2) * tailleCarreau + j * tailleCarreau,0), Quaternion.identity);
                floor.transform.parent = transform;
                floor.name = "floor " + i + " " + j;
            }
        }
         transform.position = new Vector3(position.x * maxRoomSize * tailleCarreau + position.x * 3 * tailleCarreau, position.y * maxRoomSize * tailleCarreau + (position.y * 3 * tailleCarreau), 0);
            
    }

    public virtual void setRoom(Vector2Int position, int width, int maxRoomSize)
    {
        roomType = RoomType.normal;
        this.width = width;
        this.maxRoomSize = maxRoomSize;

        transform.rotation = Quaternion.identity;
        this.position = position;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(((maxRoomSize - width) / 2) * tailleCarreau + i * tailleCarreau, ((maxRoomSize - width) / 2) * tailleCarreau + j * tailleCarreau, 0), Quaternion.identity);
                floor.transform.parent = transform;
                floor.name = "floor " + i + " " + j;
            }
        }
        transform.position = new Vector3(position.x * maxRoomSize * tailleCarreau + position.x * 3 * tailleCarreau, position.y * maxRoomSize * tailleCarreau + (position.y * 3 * tailleCarreau), 0);
    }

    void setUpBossRoom()
    {
        int temporatyMaxRoomSize;

        temporatyMaxRoomSize = 2 * maxRoomSize;
        width = temporatyMaxRoomSize;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(((temporatyMaxRoomSize - width) / 2) * tailleCarreau + i * tailleCarreau, ((temporatyMaxRoomSize - width) / 2) * tailleCarreau + j * tailleCarreau, 0), Quaternion.identity);
                floor.transform.parent = transform;
                floor.name = "floor " + i + " " + j;
            }
        }

        int positionY;
        int positionX;
        if (openingB == true || openingT == true)
        {
            if (System.Math.Sign(position.y) == -1)
            {
                positionY = position.y * maxRoomSize * tailleCarreau + (position.y * 3 * tailleCarreau) - temporatyMaxRoomSize / 2;
                print("HERE1");
            }
            else
            {
                positionY = position.y * maxRoomSize * tailleCarreau + (position.y * 3 * tailleCarreau);
                print("HERE");
            }

            positionX = position.x * maxRoomSize * tailleCarreau + position.x * 3 * tailleCarreau - temporatyMaxRoomSize / 4;

        }
        else
        {
            if (System.Math.Sign(position.x) == 1)
            {
                positionX = position.x * maxRoomSize * tailleCarreau + position.x * 3 * tailleCarreau;
            }
            else
            {
                positionX = position.x * maxRoomSize * tailleCarreau + position.x * 3 * tailleCarreau - temporatyMaxRoomSize / 2;
            }

            positionY = position.y * maxRoomSize * tailleCarreau + (position.y * 3 * tailleCarreau - temporatyMaxRoomSize / 4);


        }
        transform.position = new Vector3(positionX, positionY, 0);
        width /= 2;
    }

    public void setVoisins(bool left, bool right, bool top, bool bottom)
    {
        openingL = left;
        openingR = right;
        openingB = bottom;
        openingT = top;
    }

    public void setDoorsTrigger()
    {
        GameObject door;
        if (roomType == RoomType.boss) {
            if (openingB)
            {
                door = Instantiate(doorPrefab, new Vector3(0,0,0), Quaternion.identity);
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3(maxRoomSize - 0.5f, -1 + (maxRoomSize-width), 0);
                door.name = "doorTrigger Bottom " + position;
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }

            if (openingT)
            {
                door = Instantiate(doorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,180,0));
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize - 0.5f),(maxRoomSize - width) + 2*width, 0);
                door.name = "doorTrigger Top " + position;
                print(door.name);
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }

            if (openingL)
            {
                door = Instantiate(doorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,0,-90));
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize - width) -1, (maxRoomSize) -0.5f, 0);
                door.name = "doorTrigger Left " + position;
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }

            if (openingR)
            {
                door = Instantiate(doorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90));
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize + width ), (maxRoomSize ) -0.5f, 0);
                door.name = "doorTrigger Right " + position;
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }
        }
        else {
            if (openingB)
            {
                door = Instantiate(doorPrefab, new Vector3(0,0,0), Quaternion.identity);
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize/2 - 0.5f), -1 + (maxRoomSize-width)/2, 0);
                door.name = "doorTrigger Bottom " + position;
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }

            if (openingT)
            {
                door = Instantiate(doorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,180,0));
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize - 1) /2 +0.5f,(maxRoomSize - width)/2 + width, 0);
                door.name = "doorTrigger Top " + position;
                print(door.name);
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }

            if (openingL)
            {
                door = Instantiate(doorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,0,-90));
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize - width) /2 -1, (maxRoomSize) /2 -0.5f, 0);
                door.name = "doorTrigger Left " + position;
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }

            if (openingR)
            {
                door = Instantiate(doorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90));
                door.transform.parent = transform;
                door.transform.localPosition = new Vector3((maxRoomSize + width )/2, (maxRoomSize ) /2 -0.5f, 0);
                door.name = "doorTrigger Right " + position;
                doorsTrigger.Add(door.GetComponent<DoorTrigger>());
                door.GetComponent<DoorTrigger>().setRoom(this);
            }
        }
    }

    public virtual void spawnMonsters()
    {
        int n = Random.Range(1,maxNbMonsters);
        for (int i = 0; i < n; i++)
        {
            int mons = Random.Range(0, GameManager.instance.monstersPrefab.Count);
            GameObject monster = Instantiate(GameManager.instance.monstersPrefab[mons], new Vector3(0,0,-0.1f), Quaternion.identity);
            
            monster.transform.parent = transform;
            monster.transform.localPosition = new Vector3((maxRoomSize / 2) + i * 2, maxRoomSize / 2, -0.1f);
            monsters.Add(monster);
        }
    }

    public virtual void spawnBoss()
    {
        GameObject boss = Instantiate(GameManager.instance.bossPrefab[0], new Vector3(0,0,-0.1f), Quaternion.identity);

        boss.transform.parent = transform;
        boss.transform.localPosition = new Vector3(maxRoomSize, maxRoomSize, -0.1f);
        bossAlive = 1;
    }

    public void enteringRoom()
    {
        if(roomState == RoomState.ready)
        {
            roomState = RoomState.onGoing;
            GameManager.instance.currentRoom = this;
            for (int i = 0; i < doorsTrigger.Count; i++)
            {
                doorsTrigger[i].closeDoor();
            }
            if (roomType == RoomType.normal) {
                spawnMonsters();
            } else if (roomType == RoomType.boss) {
                spawnBoss();
            }
        }
    }

    public void roomCleared() {
        roomState = RoomState.finished;
        for (int i = 0; i < doorsTrigger.Count; i++)
        {
            doorsTrigger[i].openDoor();
        }
    }

    public void addWalls()
    {
        GameObject wall;

        if(roomType == RoomType.boss)
        {
            maxRoomSize *= 2;
            width *= 2;
        }

        for (int i = 0; i < width; i++)
        {
            if (!((i == width / 2 || i == width / 2 - 1) && openingB))
            {
                wall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                wall.transform.parent = transform;
                wall.transform.localPosition = new Vector3((((maxRoomSize - width) / 2) + i) * tailleCarreau, ((maxRoomSize - width) / 2) * tailleCarreau - 1,0);
                wall.name = "wall " + i + " " + 0 + " " + 180;
            }

            if (!((i == width / 2 || i == width / 2 - 1) && openingT))
            {
                wall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                wall.transform.parent = transform;
                wall.transform.localPosition = new Vector3((((maxRoomSize - width) / 2) + i) * tailleCarreau,((((maxRoomSize - width) / 2) + (width - 1)) * tailleCarreau) + 1, 0);
                wall.name = "wall " + i + " " + width + " " + 180;
            }

            if (i == 0)
            {
                for (int j = 0; j < width; j++)
                {
                    if((j == width/2 || j == width/2 -1) && openingL)
                    {
                        continue;
                    }
                    wall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    wall.transform.parent = transform;
                    wall.transform.localPosition = new Vector3(((maxRoomSize - width) / 2) * tailleCarreau - 1,(((maxRoomSize - width) / 2)+j) * tailleCarreau, 0);
                    wall.name = "wall " + i + " " + j + " " + -90;

                }
            }else if(i == width - 1)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((j == width / 2 || j == width / 2 - 1) && openingR)
                    {
                        continue;
                    }

                    wall = Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    wall.transform.parent = transform;
                    wall.transform.localPosition = new Vector3((((maxRoomSize - width) / 2) + width-1) * tailleCarreau + 1,(((maxRoomSize - width) / 2) + j) * tailleCarreau, 0);
                    wall.name = "wall " + i + " " + j + " " + 90;

                }
            }
        }
        if (roomType == RoomType.boss)
        {
            maxRoomSize /= 2;
            width /= 2;
        }
    }
}


