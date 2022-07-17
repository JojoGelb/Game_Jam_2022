using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public override void powerUpeffect()
    {
        GameManager.instance.player.GetComponent<PlayersMovement>().speed += 5; 
    }
}
