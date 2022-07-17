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
            SceneManager.LoadScene("EndMenu");
        }

        UIPopUp(amount);

    }
}
