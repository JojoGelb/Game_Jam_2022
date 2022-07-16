using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    protected int bulletDamage = 1;
    protected bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);  
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision avec " + collision.gameObject.name);
        Destroy(gameObject);
    }

    public void setDamage(int dmg)
    {
        bulletDamage = dmg;
    }
}
