using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMovement : MonoBehaviour
{
    public float speed = 1f;
    public float dashCooldown = 5f;
    private float dashTime;
    public float dashLength = 10f;
    private Vector2 dash;
    private Vector2 savedVelocity = new Vector2(0,0);



    private Direction facingDirection = Direction.South;
    public Rigidbody2D rb;

    Vector2 knockVector;

    [Header("Dont touch this")]
    public InputAction moveAction;
    public InputAction dashAction;
    public InputAction shootAction;
    public InputAction ReloadAction;
    public InputAction PauseAction;
    private PlayerInput playerInput;

    private GunBehavior gunBehavior;
    
    public GameObject getCanva()
    {
        return gunBehavior.Canva;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gunBehavior = GetComponent<GunBehavior>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"] ;
        dashAction = playerInput.actions["Dash"];
        shootAction = playerInput.actions["Shoot"];
        ReloadAction = playerInput.actions["Reload"];
        PauseAction = playerInput.actions["Pause"];
    }

    // Update is called once per frame
    void Update() {

        //pause
        Pause();
        if (GameManager.instance.pause)
        {
            return;
        }
        //deplacement 
        Vector2 cumulatedMove = new Vector2(0, 0);

        cumulatedMove += moveAction.ReadValue<Vector2>()*speed*Time.deltaTime;
        WichDirectionToLookWhenMooving(cumulatedMove);

        //reload

        if (ReloadAction.triggered)
        {
            gunBehavior.reloading();
            print("reloading");
        }

        //shoot
        Direction shootDiR = gunBehavior.shoot(shootAction.ReadValue<Vector2>());
        if(shootDiR != Direction.None)
        {
            facingDirection = shootDiR;
        }

        //KnockBack
        if (knockVector.magnitude > 0.2F)
        {
            cumulatedMove += knockVector * Time.deltaTime;
            knockVector = Vector2.Lerp(knockVector, Vector2.zero, 5 * Time.deltaTime);
        }
        
        //dash
        if (dash.magnitude > 0.2F)
        {
            cumulatedMove += dash * Time.deltaTime;
            dash = Vector2.Lerp(dash, Vector2.zero, 5 * Time.deltaTime);
        }

        Dash();
        WichDirectionToLook(cumulatedMove);
        
        rb.MovePosition(rb.position + cumulatedMove);

        changeLookingDirection();
    }

    void Pause()
    {

        if (PauseAction.triggered)
        {
            if (gunBehavior.Canva.transform.GetChild(0).gameObject.activeInHierarchy || gunBehavior.Canva.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                gunBehavior.Canva.transform.GetChild(0).gameObject.SetActive(false);
                gunBehavior.Canva.transform.GetChild(1).gameObject.SetActive(false);
                GameManager.instance.pause = false;
            }
            else
            {
                gunBehavior.Canva.transform.GetChild(0).gameObject.SetActive(true);
                GameManager.instance.pause = true;
            }
        }

        
    }
    void changeLookingDirection()
    {
        switch (facingDirection)
        {
            case Direction.North:
                transform.GetChild(2).transform.localRotation = Quaternion.Euler(0,0,0);
                break;
            case Direction.South:
                transform.GetChild(2).transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.East:
                transform.GetChild(2).transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.West:
                transform.GetChild(2).transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    void WichDirectionToLookWhenMooving(Vector2 directionToMoove)
    {
        if (directionToMoove.y < 0)
        {
            facingDirection = Direction.South;
        }
        else if (directionToMoove.x > 0)
        {
            facingDirection = Direction.East;
        }
        else if (directionToMoove.x < 0)
        {
            facingDirection = Direction.West;
        }
        else
        {
            facingDirection = Direction.North;
        }
    }

    void Dash()
    {
        dashTime += Time.deltaTime;
        if ( dashAction.triggered && dashTime >= dashCooldown)
        {
            dashTime = 0;
            Vector2 input = moveAction.ReadValue<Vector2>();

            dash = (transform.up * input.y + transform.right * input.x).normalized *dashLength;
        }
    }

    void WichDirectionToLook(Vector2 directionToMoove)
    {
        if (directionToMoove.y < 0) {
            facingDirection = Direction.South;
        } else if (directionToMoove.x > 0) {
            facingDirection = Direction.East;
        } else if (directionToMoove.x < 0) {
            facingDirection = Direction.West;
        } else {
            facingDirection = Direction.North;
        }
    }

    public void knockBack(Vector3 position ,float amount)
    {
        knockVector = (transform.position - position).normalized * amount;
    }


}

public enum Direction {
    North,
    East,
    South,
    West,
    None
}

