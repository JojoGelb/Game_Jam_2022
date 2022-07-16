using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private PlayersMovement move;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        move = gameObject.GetComponentInParent<PlayersMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", move.moveAction.ReadValue<Vector2>().x);
        animator.SetFloat("Vertical", move.moveAction.ReadValue<Vector2>().y);
        animator.SetFloat("speed", move.moveAction.ReadValue<Vector2>().magnitude);
    }
}
