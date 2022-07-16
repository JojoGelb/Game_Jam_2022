using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    BoxCollider2D doorCollider;
    MeshRenderer doorRenderer;

    private Room room;

    public void setRoom(Room r)
    {
        room = r;
    }

    private void Start()
    {
        doorCollider = gameObject.GetComponent<BoxCollider2D>();
        doorRenderer = gameObject.GetComponent<MeshRenderer>();
        doorRenderer.enabled = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("oui");
        if (other.gameObject.name == "Player")
        {
            room.enteringRoom();
        }
    }

    public void openDoor()
    {
        Destroy(gameObject);
    }

    public void closeDoor()
    {

        print(gameObject.transform.rotation);
        switch (gameObject.transform.rotation.eulerAngles.y)
        {
            case 0:
                gameObject.transform.localPosition -= new Vector3(0, 0, 0);
                break;
            case 90:
                gameObject.transform.localPosition -= new Vector3(0.5f, 0, 0);
                break;
            case 270:
                gameObject.transform.localPosition -= new Vector3(-0.5f, 0, 0);
                break;
            case 180:
                gameObject.transform.localPosition -= new Vector3(0, 0, 0);
                break;
        }

        doorCollider.isTrigger = false;
        doorRenderer.enabled = true;
    }

}
