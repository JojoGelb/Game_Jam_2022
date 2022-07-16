using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMovement : MonoBehaviour
{
    public float speed = 1f;
    private float dashTime;
    public float dashColdown = 5f;
    public float dashLength = 10f;
    private Vector2 dash;
    private Vector2 savedVelocity = new Vector2(0,0);
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction shootAction;
    private PlayerInput playerInput;
    private Direction facingDirection = Direction.South;
    private CharacterController ct;

    private GunBehavior gunBehavior;
    
    // Start is called before the first frame update
    void Start()
    {
        gunBehavior = GetComponent<GunBehavior>();
        ct = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"] ;
        dashAction = playerInput.actions["Dash"];
        shootAction = playerInput.actions["Shoot"];
    }

    // Update is called once per frame
    void Update() { 
        //deplacement 
        Vector2 cumulatedMove = new Vector2(0, 0);

        cumulatedMove += moveAction.ReadValue<Vector2>()*speed*Time.deltaTime;
        WichDirectionToLookWhenMooving(cumulatedMove);
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
        
        ct.Move(cumulatedMove);

        changeLookingDirection();
    }

    void changeLookingDirection()
    {
        switch (facingDirection)
        {
            case Direction.North:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0,0,0);
                break;
            case Direction.South:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.East:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.West:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
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
        if ( dashAction.triggered && dashTime >= dashColdown)
        {
            dashTime = 0;
            Vector2 input = moveAction.ReadValue<Vector2>();

            dash = (transform.up * input.y + transform.right * input.x).normalized *dashLength;
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

