using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : EnnemiesBullet
{
    private GameObject target;
    public float speed = 13f;
    private Rigidbody2D rb;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player;
    }
 
    void Update(){
        Vector2 move = new Vector2(0, 0);
        move = (target.transform.position - transform.position).normalized*speed*Time.deltaTime;

        rb.MovePosition(rb.position + move);

    }
}
