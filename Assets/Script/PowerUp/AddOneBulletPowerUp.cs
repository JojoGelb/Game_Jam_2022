using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOneBulletPowerUp : PowerUp
{
    public override void powerUpeffect()
    {
        GameManager.instance.player.GetComponent<GunBehavior>().bulletMax += 1;
        GameManager.instance.player.GetComponent<GunBehavior>().bulletMin += 1;
    }
}
