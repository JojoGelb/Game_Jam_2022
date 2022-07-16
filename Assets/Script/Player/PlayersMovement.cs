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
    public InputAction moveAction;
    private InputAction dashAction;
    public InputAction shootAction;
    private InputAction ReloadAction;
    private PlayerInput playerInput;
    private Direction facingDirection = Direction.South;
    public Rigidbody2D rb;

    private GunBehavior gunBehavior;
    
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
    }

    // Update is called once per frame
    void Update() { 
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
        //direction sprite

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
}

public enum Direction {
    North,
    East,
    South,
    West,
    None
}

