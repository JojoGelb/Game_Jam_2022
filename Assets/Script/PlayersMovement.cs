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
    private PlayerInput playerInput;
    private Direction facingDirection = Direction.South;
    private CharacterController ct;
    
    // Start is called before the first frame update
    void Start()
    {
        ct = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"] ;
        dashAction = playerInput.actions["Dash"];
    }

    // Update is called once per frame
    void Update() { 
        //deplacement 
        Vector2 cumulatedMove = new Vector2(0, 0);

        cumulatedMove += moveAction.ReadValue<Vector2>()*speed*Time.deltaTime; 

        //shoot

        //direction sprite

        //dash
        if (dash.magnitude > 0.2F)
        {
            cumulatedMove += dash * Time.deltaTime;
            dash = Vector2.Lerp(dash, Vector2.zero, 5 * Time.deltaTime);
        }

        Dash();
        
        ct.Move(cumulatedMove);
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
    Noth,
    East,
    South,
    West
}

