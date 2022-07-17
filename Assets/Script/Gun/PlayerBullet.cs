using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        print("player bullet collide " +LayerMask.LayerToName(collision.gameObject.layer));
        print("nom du truc touche" + collision.gameObject.name);
        if (collision.gameObject.name == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
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
            stop = true;
        }
    }
}
