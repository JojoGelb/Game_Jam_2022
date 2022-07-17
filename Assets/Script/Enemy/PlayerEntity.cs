using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{

    public override void dealDamage(int amount)
    {
        //print(amount);
        LifePoint -= amount;

        if (LifePoint <= 0)
        {
            PassInfoBTWScene.result = PassInfoBTWScene.Result.loose;
            SceneManager.LoadScene("EndGame");
        }

        if (healthBar)
        {
            healthBar.SetHealth(LifePoint);
        }

        UIPopUp(amount);

    }
}
