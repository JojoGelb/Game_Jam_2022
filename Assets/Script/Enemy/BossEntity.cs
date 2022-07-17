using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEntity : Entity
{
    void Start() 
    {
        MaxLifePoint = 100;
        LifePoint = 100;

        if (healthBar)
        {
            healthBar.SetMaxHealth(MaxLifePoint);
            healthBar.SetHealth(LifePoint);
        }
    }
   
   public override void dealDamage(int amount)
    {
        //print(amount);
        LifePoint -= amount;

        if(LifePoint <= 0)
        {
            GameManager.instance.removeBoss(this);
            Destroy(gameObject);
            PassInfoBTWScene.result = PassInfoBTWScene.Result.win;
            SceneManager.LoadScene("EndGame");
        }

        if (healthBar)
        {
            healthBar.SetHealth(LifePoint);
        }

        UIPopUp(amount);

    }
}
