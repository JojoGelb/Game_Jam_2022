using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision) {
        print("collision avec quelquechose");
        if(collision.gameObject.name == "Player") {
            powerUpeffect();
            Destroy(gameObject);
        }
    }
    public virtual void powerUpeffect() {}
}
