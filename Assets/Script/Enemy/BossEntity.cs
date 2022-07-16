using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntity : Entity
{
    void Start() 
    {
        MaxLifePoint = 100;
        LifePoint = 100;
    }
   
   public override void dealDamage(int amount)
    {
        //print(amount);
        LifePoint -= amount;

        if(LifePoint <= 0)
        {
            GameManager.instance.removeBoss(this);
            Destroy(gameObject);
        }

        UIPopUp(amount);

    }
}
