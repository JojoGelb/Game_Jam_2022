using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesBullet : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            return;
        }

        base.OnCollisionEnter2D(collision);

        if (!stop)
        {
            Entity ent = collision.gameObject.GetComponent<Entity>();

            if (ent == null)
            {
                return;
            }

            ent.dealDamage(bulletDamage);
            stop = true;
        }
    }
}
