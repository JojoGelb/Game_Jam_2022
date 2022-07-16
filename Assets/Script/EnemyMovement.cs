using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;
    private Vector2 currentPos;
    public float speed = 3f;
    public float Viewdistance = 5f;
    private CharacterController ct;

    void Start () {
        ct = GetComponent<CharacterController>();
        targetTransform = target.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
    }
 
    void Update(){
        Vector2 move = new Vector2(0, 0);
        move = (targetTransform.position - transform.position).normalized*speed*Time.deltaTime;

        if (Vector2.Distance(targetTransform.position, transform.position) < Viewdistance) {
            ct.Move(move);
        }
    }
}
