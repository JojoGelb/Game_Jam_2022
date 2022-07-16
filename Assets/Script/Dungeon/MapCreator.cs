using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapCreator : MonoBehaviour
{

    public GameObject BasicRoom;

    public int numberOfRooms = 7;
    public int minRoomSize = 6;
    public int maxRoomSize = 10;
    private int roomCreated = 0;
    public int tailleCarreau = 1;

    private int mid = 16;

    public List<Room> rooms = new List<Room>();
    private Queue<Room> queue = new Queue<Room>();



    void Start()
    {

        mid = (maxRoomSize - 2)/2 * tailleCarreau;

        generateRooms();

        addConnection();

        addWalls();

    }

    void generateRooms()
    {
        int remainingOpening = 0;
        int width = maxRoomSize;

        GameObject roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
        roo.transform.parent = transform;
        Room room = roo.GetComponent<Room>();
        //roo.AddComponent(typeof(CastleRoom)) as CastleRoom;

        bool openL = false;
        bool openR = false;
        bool openB = false;
        bool openT = true;


        room.setRoom(new Vector2Int(0, 0), width, maxRoomSize, Room.RoomType.spawn);
        room.setVoisins(openL, openR, openT, openB);
        room.roomState = Room.RoomState.finished;
        rooms.Add(room);
        roomCreated += 2;

        width = Random.Range(minRoomSize, maxRoomSize + 1);
        if (width % 2 == 1) width++;

        roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
        roo.transform.parent = transform;
        room = roo.GetComponent<Room>();
        room.setRoom(new Vector2Int(0, 1), width, maxRoomSize, Room.RoomType.normal);
        room.setVoisins(true, true, true, true);
        rooms.Add(room);
        queue.Enqueue(room);
        roomCreated += 3;
        remainingOpening += 3;

        while (queue.Count > 0)
        {
            Room r = queue.Dequeue();

            if (r.openingL && !alreadyCreated(r.position.x - 1, r.position.y))
            {
                width = Random.Range(minRoomSize, maxRoomSize + 1);
                if (width % 2 == 1) width++;
                remainingOpening--;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setRoom(new Vector2Int(r.position.x - 1, r.position.y), width, maxRoomSize);
                openR = true;
                openB = false;
                openT = false;
                openL = false;

                if (roomCreated < numberOfRooms)
                {
                    int numOfOpening = numberOfRooms - roomCreated;
                    if (numOfOpening > 3)
                    {
                        numOfOpening = 3;
                    }
                    numOfOpening -= Random.Range(0, numOfOpening + 1);
                    for (int i = 0; i < numOfOpening; i++)
                    {
                        int numDoor = Random.Range(0, 3);

                        switch (numDoor)
                        {
                            case 0:
                                if (openL != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x - 1, room.position.y);
                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingR)
                                        {
                                            openL = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;
                                }
                                openL = true;
                                break;
                            case 1:
                                if (openT != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x, room.position.y + 1);
                                    //if (voisinDuVoisin != null && !voisinDuVoisin.openingB) { continue; }

                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingB)
                                        {
                                            openT = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;

                                }
                                openT = true;
                                break;
                            case 2:
                                if (openB != true)
                                {

                                    Room voisinDuVoisin = getRoomByPosition(room.position.x, room.position.y - 1);

                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingT)
                                        {
                                            openB = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;

                                }
                                openB = true;
                                break;

                            default:
                                break;
                        }

                    }

                }
                room.setVoisins(openL, openR, openT, openB);
                rooms.Add(room);
                queue.Enqueue(room);
            }
            if (r.openingR && !alreadyCreated(r.position.x + 1, r.position.y))
            {
                width = Random.Range(minRoomSize, maxRoomSize + 1);
                if (width % 2 == 1) width++;
                remainingOpening--;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setRoom(new Vector2Int(r.position.x + 1, r.position.y), width, maxRoomSize);
                openR = false;
                openB = false;
                openT = false;
                openL = true;

                if (roomCreated < numberOfRooms)
                {
                    int numOfOpening = numberOfRooms - roomCreated;
                    if (numOfOpening > 3)
                    {
                        numOfOpening = 3;
                    }
                    numOfOpening -= Random.Range(0, numOfOpening + 1);
                    for (int i = 0; i < numOfOpening; i++)
                    {
                        int numDoor = Random.Range(0, 3);

                        switch (numDoor)
                        {
                            case 0:
                                if (openR != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x + 1, room.position.y);
                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingL)
                                        {

                                            openR = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;
                                }
                                openR = true;
                                break;
                            case 1:
                                if (openT != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x, room.position.y + 1);
                                    //if (voisinDuVoisin != null && !voisinDuVoisin.openingB) { continue; }

                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingB)
                                        {
                                            openT = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;

                                }
                                openT = true;
                                break;
                            case 2:
                                if (openB != true)
                                {

                                    Room voisinDuVoisin = getRoomByPosition(room.position.x, room.position.y - 1);

                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingT)
                                        {
                                            openB = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;

                                }
                                openB = true;
                                break;

                            default:
                                break;
                        }

                    }

                }
                room.setVoisins(openL, openR, openT, openB);
                rooms.Add(room);
                queue.Enqueue(room);
            }
            if (r.openingT && !alreadyCreated(r.position.x, r.position.y + 1))
            {
                width = Random.Range(minRoomSize, maxRoomSize + 1);
                if (width % 2 == 1) width++;
                remainingOpening--;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setRoom(new Vector2Int(r.position.x, r.position.y + 1), width, maxRoomSize);
                openR = false;
                openB = true;
                openT = false;
                openL = false;

                if (roomCreated < numberOfRooms)
                {
                    int numOfOpening = numberOfRooms - roomCreated;
                    if (numOfOpening > 3)
                    {
                        numOfOpening = 3;
                    }
                    numOfOpening -= Random.Range(0, numOfOpening + 1);
                    for (int i = 0; i < numOfOpening; i++)
                    {
                        int numDoor = Random.Range(0, 3);

                        switch (numDoor)
                        {
                            case 0:
                                if (openR != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x + 1, room.position.y);
                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingL)
                                        {

                                            openR = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;
                                }
                                openR = true;
                                break;
                            case 1:
                                if (openT != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x, room.position.y + 1);
                                    //if (voisinDuVoisin != null && !voisinDuVoisin.openingB) { continue; }

                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingB)
                                        {
                                            openT = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;

                                }
                                openT = true;
                                break;
                            case 2:
                                if (openL != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x - 1, room.position.y);
                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingR)
                                        {
                                            openL = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;
                                }
                                openL = true;
                                break;

                            default:
                                break;
                        }

                    }

                }
                room.setVoisins(openL, openR, openT, openB);
                rooms.Add(room);
                queue.Enqueue(room);
            }

            if (r.openingB && !alreadyCreated(r.position.x, r.position.y - 1))
            {
                width = Random.Range(minRoomSize, maxRoomSize + 1);
                if (width % 2 == 1) width++;
                remainingOpening--;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setRoom(new Vector2Int(r.position.x, r.position.y - 1), width, maxRoomSize);
                openR = false;
                openB = false;
                openT = true;
                openL = false;

                if (roomCreated < numberOfRooms)
                {
                    int numOfOpening = numberOfRooms - roomCreated;
                    if (numOfOpening > 3)
                    {
                        numOfOpening = 3;
                    }
                    numOfOpening -= Random.Range(0, numOfOpening + 1);
                    for (int i = 0; i < numOfOpening; i++)
                    {
                        int numDoor = Random.Range(0, 3);

                        switch (numDoor)
                        {
                            case 0:
                                if (openR != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x + 1, room.position.y);
                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingL)
                                        {

                                            openR = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;
                                }
                                openR = true;
                                break;
                            case 1:
                                if (openB != true)
                                {

                                    Room voisinDuVoisin = getRoomByPosition(room.position.x, room.position.y - 1);

                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingT)
                                        {
                                            openB = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;

                                }
                                openB = true;
                                break;
                            case 2:
                                if (openL != true)
                                {
                                    Room voisinDuVoisin = getRoomByPosition(room.position.x - 1, room.position.y);
                                    if (voisinDuVoisin != null)
                                    {
                                        if (voisinDuVoisin.openingR)
                                        {
                                            openL = true;
                                        }
                                        continue;
                                    }

                                    remainingOpening++; roomCreated++;
                                }
                                openL = true;
                                break;

                            default:
                                break;
                        }

                    }

                }
                room.setVoisins(openL, openR, openT, openB);
                rooms.Add(room);
                queue.Enqueue(room);
            }

            if (queue.Count == 0 && rooms.Count < numberOfRooms)
            {
                addRandomRoomToQueue();
                remainingOpening--;
            }
        }

        int choice = addBossRoom();
        addChestRoom(choice);

        bugPatch();
    }

    int addBossRoom()
    {
        GameObject roo;
        Room room = null;
        int indexMax = 0;
        int directionMax = 0;

        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if(rooms[i].position.x < directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.x;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    return addBossRoom();
                }
                rooms[indexMax].openingL = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(false, true, false, false);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x - 1, rooms[indexMax].position.y), maxRoomSize, maxRoomSize,Room.RoomType.boss);
                
                break;
            case 1:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.x > directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.x;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    return addBossRoom();
                }
                rooms[indexMax].openingR = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(true, false, false, false);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x + 1, rooms[indexMax].position.y), maxRoomSize, maxRoomSize, Room.RoomType.boss);
                
                break;
            case 2:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.y < directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.y;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    return addBossRoom();
                }
                rooms[indexMax].openingB = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(false, false, true, false);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x, rooms[indexMax].position.y-1), maxRoomSize, maxRoomSize, Room.RoomType.boss);
               
                break;
            case 3:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.y > directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.y;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    return addBossRoom();
                }
                rooms[indexMax].openingT = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(false, false, false, true);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x, rooms[indexMax].position.y + 1), maxRoomSize, maxRoomSize, Room.RoomType.boss);
                
                break;
        }
        rooms.Add(room);
        return direction;
    }

    void addChestRoom(int directionPrecedente)
    {
        GameObject roo;
        Room room = null;
        int indexMax = 0;
        int directionMax = 0;

        int direction = Random.Range(0, 4);
        while(direction == directionPrecedente)
        {
            direction = Random.Range(0, 4);
        }

        switch (direction)
        {
            case 0:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.x < directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.x;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    addChestRoom(directionPrecedente);
                    return;
                }
                rooms[indexMax].openingL = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(false, true, false, false);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x - 1, rooms[indexMax].position.y), maxRoomSize, maxRoomSize, Room.RoomType.chest);

                break;
            case 1:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.x > directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.x;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    addChestRoom(directionPrecedente);
                    return;
                }
                rooms[indexMax].openingR = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(true, false, false, false);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x + 1, rooms[indexMax].position.y), maxRoomSize, maxRoomSize, Room.RoomType.chest);

                break;
            case 2:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.y < directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.y;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    addChestRoom(directionPrecedente);
                    return;
                }
                rooms[indexMax].openingB = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(false, false, true, false);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x, rooms[indexMax].position.y - 1), maxRoomSize, maxRoomSize, Room.RoomType.chest);

                break;
            case 3:
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].position.y > directionMax)
                    {
                        indexMax = i;
                        directionMax = rooms[i].position.y;
                    }
                }
                if (rooms[indexMax].roomType == Room.RoomType.spawn)
                {
                    addChestRoom(directionPrecedente);
                    return;
                }
                rooms[indexMax].openingT = true;
                roo = Instantiate(BasicRoom, new Vector3(0, 0, 0), Quaternion.identity);
                roo.transform.parent = transform;
                room = roo.GetComponent<Room>();
                room.setVoisins(false, false, false, true);
                room.setRoom(new Vector2Int(rooms[indexMax].position.x, rooms[indexMax].position.y + 1), maxRoomSize, maxRoomSize, Room.RoomType.chest);

                break;
        }
        room.roomState = Room.RoomState.finished;
        rooms.Add(room);
    }

    //Some walls created on possible path
    void bugPatch()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].openingB)
            {
                Room roomBottom = getRoomByPosition(rooms[i].position.x, rooms[i].position.y - 1);
                roomBottom.openingT = true;
            }
            if (rooms[i].openingT)
            {
                Room roomBottom = getRoomByPosition(rooms[i].position.x, rooms[i].position.y + 1);
                roomBottom.openingB = true;
            }
            if (rooms[i].openingL)
            {
                Room roomBottom = getRoomByPosition(rooms[i].position.x - 1, rooms[i].position.y);
                roomBottom.openingR = true;
            }
            if (rooms[i].openingR)
            {
                Room roomBottom = getRoomByPosition(rooms[i].position.x + 1, rooms[i].position.y);
                roomBottom.openingL = true;
            }
        }
    }

    bool alreadyCreated(int posX, int posY)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].position.x == posX && rooms[i].position.y == posY)
            {
                return true;
            }
        }
        return false;
    }

    Room getRoomByPosition(int posX, int posY)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].position.x == posX && rooms[i].position.y == posY)
            {
                return rooms[i];
            }
        }
        return null;
    }

    void addRandomRoomToQueue()
    {
        int compteur = 0;
        while (true)
        {
            compteur++;
            int numRoom = Random.Range(0, rooms.Count);

            if (!rooms[numRoom].openingL && !alreadyCreated(rooms[numRoom].position.x - 1, rooms[numRoom].position.y))
            {
                rooms[numRoom].openingL = true;
                roomCreated++;
                queue.Enqueue(rooms[numRoom]);
                return;
            }

            if (!rooms[numRoom].openingR && !alreadyCreated(rooms[numRoom].position.x + 1, rooms[numRoom].position.y))
            {
                rooms[numRoom].openingR = true;
                roomCreated++;
                queue.Enqueue(rooms[numRoom]);
                return;
            }

            if (!rooms[numRoom].openingB && !alreadyCreated(rooms[numRoom].position.x, rooms[numRoom].position.y - 1))
            {
                rooms[numRoom].openingB = true;
                roomCreated++;
                queue.Enqueue(rooms[numRoom]);
                return;
            }

            if (!rooms[numRoom].openingT && !alreadyCreated(rooms[numRoom].position.x, rooms[numRoom].position.y + 1))
            {
                rooms[numRoom].openingT = true;
                roomCreated++;
                queue.Enqueue(rooms[numRoom]);
                return;
            }
        }
    }

    void addConnection()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].openingB)
            {
                GameObject connector = new GameObject("Connector bottom ROOM " + rooms[i].position);
                connector.transform.parent = transform;
                GameObject floor;
                GameObject wall;
                Room rDown = getRoomByPosition(rooms[i].position.x, rooms[i].position.y - 1);

                for (int j = -3 - (maxRoomSize - rDown.width) / 2; j < (maxRoomSize - rooms[i].width) / 2; j++)
                {
                    floor = Instantiate(rooms[i].floorPrefab, new Vector3(mid, j * tailleCarreau,0), Quaternion.identity);
                    floor.transform.parent = connector.transform;
                    floor.name = "floorConnector Bottom " + rooms[i].position.x + " " + rooms[i].position.y;
                    floor = Instantiate(rooms[i].floorPrefab, new Vector3(mid + tailleCarreau, j * tailleCarreau, 0), Quaternion.identity);
                    floor.transform.parent = connector.transform;
                    floor.name = "floorConnector Bottom " + rooms[i].position.x + " " + rooms[i].position.y;

                    wall = Instantiate(rooms[i].wallPrefab, new Vector3(mid - 1, j * tailleCarreau, 0), Quaternion.identity);
                    wall.transform.parent = connector.transform;
                    wall.name = "WallConnector Bottom a" + rooms[i].position.x + " " + rooms[i].position.y;

                    wall = Instantiate(rooms[i].wallPrefab, new Vector3(mid + 2, j * tailleCarreau, 0), Quaternion.identity);
                    wall.transform.parent = connector.transform;
                    wall.name = "WallConnector Bottom b " + rooms[i].position.x + " " + rooms[i].position.y;
                }

                connector.transform.position = new Vector3(rooms[i].position.x * maxRoomSize * tailleCarreau + rooms[i].position.x * 3 * tailleCarreau, rooms[i].position.y * maxRoomSize * tailleCarreau + +(rooms[i].position.y * 3 * tailleCarreau), 0);
            }
            if (rooms[i].openingL)
            {
                GameObject connector = new GameObject("Connector Left ROOM " + rooms[i].position);
                connector.transform.parent = transform;
                GameObject floor;
                Room rLeft = getRoomByPosition(rooms[i].position.x - 1, rooms[i].position.y);
                GameObject wall;


                for (int j = -3 - (maxRoomSize - rLeft.width) / 2; j < (maxRoomSize - rooms[i].width) / 2; j++)
                {
                    floor = Instantiate(rooms[i].floorPrefab, new Vector3(j * tailleCarreau, mid + tailleCarreau, 0), Quaternion.identity);
                    floor.transform.parent = connector.transform;
                    floor.name = "floorConnector Left " + rooms[i].position.x + " " + rooms[i].position.y;

                    floor = Instantiate(rooms[i].floorPrefab, new Vector3(j * tailleCarreau,mid, 0), Quaternion.identity);
                    floor.transform.parent = connector.transform;
                    floor.name = "floorConnector Left " + rooms[i].position.x + " " + rooms[i].position.y;

                    wall = Instantiate(rooms[i].wallPrefab, new Vector3(j * tailleCarreau, mid + 2 , 0), Quaternion.identity);
                    wall.transform.parent = connector.transform;
                    wall.name = "WallConnector Left a" + rooms[i].position.x + " " + rooms[i].position.y;

                    wall = Instantiate(rooms[i].wallPrefab, new Vector3(j * tailleCarreau, mid - 1, 0), Quaternion.identity);
                    wall.transform.parent = connector.transform;
                    wall.name = "WallConnector Left b" + rooms[i].position.x + " " + rooms[i].position.y;

                }

                connector.transform.position = new Vector3(rooms[i].position.x * maxRoomSize * tailleCarreau + rooms[i].position.x * 3 * tailleCarreau, rooms[i].position.y * maxRoomSize * tailleCarreau + (rooms[i].position.y * 3 * tailleCarreau), 0);
            }

        }
    }

    void addWalls()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].addWalls();
            rooms[i].setDoorsTrigger();
        }
    }
}
