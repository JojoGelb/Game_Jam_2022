using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public int speed = 10;
    public override void powerUpeffect()
    {
        GameManager.instance.player.GetComponent<PlayersMovement>().speed += 10; 
    }
}
