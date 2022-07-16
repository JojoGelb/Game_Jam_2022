using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    public int LifePoint = 10;
    public int MaxLifePoint = 10;

    public void dealDamage(int amount)
    {
        print(amount);
        LifePoint -= amount;

        if(LifePoint <= 0)
        {
            Destroy(gameObject);
        }

    }
}
