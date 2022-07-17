using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTwoBulletPowerUp : PowerUp
{
    public override void powerUpeffect()
    {
        GameManager.instance.player.GetComponent<GunBehavior>().bulletMax += 2;
        GameManager.instance.player.GetComponent<GunBehavior>().bulletMin += 2;
    }
}
