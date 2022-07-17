using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : EnnemiesBullet
{
    public override void destroyObject() {
        Destroy(gameObject, 5f);
    }
}
