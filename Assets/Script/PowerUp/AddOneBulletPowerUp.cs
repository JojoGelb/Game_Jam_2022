using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOneBulletPowerUp : PowerUp
{
    public override void powerUpeffect()
    {
        GunBehavior gb = GameManager.instance.player.GetComponent<GunBehavior>();
        gb.bulletMax += 1;
        gb.bulletMin += 1;
        gb.changeMaxBulletUI(gb.bulletMax);
    }
}
