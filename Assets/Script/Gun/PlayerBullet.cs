using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            return;
        }

        base.OnTriggerEnter(collision);

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
