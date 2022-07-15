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

    [Header("Rooms prefabs")]
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public virtual void setRoom(Vector2Int position, int width, int maxRoomSize, RoomType type)
    {
        roomType = type;
        this.width = width;
        this.maxRoomSize = maxRoomSize;

        transform.rotation = Quaternion.identity;
        this.position = position;
        int temporatyMaxRoomSize = maxRoomSize;
        if (type == RoomType.boss)
        {
            temporatyMaxRoomSize = 2 * maxRoomSize; 
            width = temporatyMaxRoomSize;
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(((temporatyMaxRoomSize - width)/2)*tailleCarreau + i * tailleCarreau , ((temporatyMaxRoomSize - width) / 2) * tailleCarreau + j * tailleCarreau,0), Quaternion.identity);
                floor.transform.parent = transform;
                floor.name = "floor " + i + " " + j;
            }
        }
        if (type == RoomType.boss)
        {
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
                if(System.Math.Sign(position.x) == 1)
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

        }
        else
        {
            transform.position = new Vector3(position.x * maxRoomSize * tailleCarreau + position.x * 3 * tailleCarreau, position.y * maxRoomSize * tailleCarreau + (position.y * 3 * tailleCarreau), 0);
        }
            
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

    public void setVoisins(bool left, bool right, bool top, bool bottom)
    {
        openingL = left;
        openingR = right;
        openingB = bottom;
        openingT = top;
    }

    public void addWalls()
    {
        GameObject wall;

        if(roomType == RoomType.boss)
        {
            width *= 2;
            maxRoomSize *= 2;
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
            width /= 2;
            maxRoomSize /= 2;
        }
    }
}
