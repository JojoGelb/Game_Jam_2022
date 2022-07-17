using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTwoBulletPowerUp : PowerUp
{
    public override void powerUpeffect()
    {
        GunBehavior gb = GameManager.instance.player.GetComponent<GunBehavior>();
        gb.bulletMax += 2;
        gb.bulletMin += 2;
        gb.changeMaxBulletUI(gb.bulletMax);
    }
}
