using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSpawnedByBoss : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;
    public float Viewdistance = float.PositiveInfinity;
    private Rigidbody2D rb;
    public int damage = 1;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player;
    }
 
    void Update(){
        Vector2 move = new Vector2(0, 0);
        move = (target.transform.position - transform.position).normalized*speed*Time.deltaTime;

        if (Vector2.Distance(target.transform.position, transform.position) < Viewdistance) {
            rb.MovePosition(rb.position + move);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Entity>().dealDamage(damage);
            collision.gameObject.GetComponent<PlayersMovement>().knockBack(transform.position ,100f);
        }
    }

    public void setViewDistance(float dist) {
        Viewdistance = dist;
    }
}
