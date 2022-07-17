using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{

    public float invicibilityTime = 0.5f;
    private float timer = 1f;

    void Update()
    {
        timer += Time.deltaTime;
    }

    public override void dealDamage(int amount)
    {
        
        //print(amount);
        if(timer > invicibilityTime)
        {
            LifePoint -= amount;
            healthBar.gameObject.SetActive(true);
            timer = 0;
            UIPopUp(amount);
        }
        

        if (LifePoint <= 0)
        {
            PassInfoBTWScene.result = PassInfoBTWScene.Result.loose;
            SceneManager.LoadScene("EndGame");
        }

        if (healthBar)
        {
            healthBar.SetHealth(LifePoint);
        }


        

    }
}
