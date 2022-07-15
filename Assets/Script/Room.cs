using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room: MonoBehaviour
{
    public enum RoomState {ready, onGoing, finished};

    [Header("Infos a ne pas changer")]
    public Vector2Int position;
   
    public bool openingL = false;
    public bool openingR = false;
    public bool openingT= false;
    public bool openingB = false;

    public int tailleCarreau = 4;

    private int maxRoomSize;
    public int width;

    public RoomState roomState = RoomState.ready;

    [Header("Rooms prefabs")]
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public virtual void setRoom(Vector2Int position, int width, int maxRoomSize)
    {
        this.width = width;
        this.maxRoomSize = maxRoomSize;

        transform.rotation = Quaternion.identity;
        this.position = position;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(((maxRoomSize - width)/2)*tailleCarreau + i * tailleCarreau , ((maxRoomSize - width) / 2) * tailleCarreau + j * tailleCarreau,0), Quaternion.identity);
                floor.transform.parent = transform;
                floor.name = "floor " + i + " " + j;
            }
        }
        transform.position = new Vector3(position.x * maxRoomSize * tailleCarreau + position.x*3*tailleCarreau, position.y * maxRoomSize * tailleCarreau +(position.y*3*tailleCarreau),0);
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
    }
}
