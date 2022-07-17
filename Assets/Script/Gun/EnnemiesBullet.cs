using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            return;
        }

        base.OnTriggerEnter2D(collision);

        if (!stop)
        {
            Entity ent = collision.gameObject.GetComponent<Entity>();

            if (ent == null)
            {
                return;
            }

            ent.dealDamage(bulletDamage);
            collision.gameObject.GetComponent<PlayersMovement>().knockBack(transform.position, 100f);
            stop = true;
        }
    }

    public override void destroyObject() {
        Destroy(gameObject, 4f);
    }
}
