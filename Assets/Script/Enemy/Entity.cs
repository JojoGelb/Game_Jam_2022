using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    public int LifePoint = 10;
    public int MaxLifePoint = 10;
    public GameObject prefabDamagePopUp;

    public virtual void dealDamage(int amount)
    {
        //print(amount);
        LifePoint -= amount;

        if(LifePoint <= 0)
        {
            GameManager.instance.removeMonster(this);
            Destroy(gameObject);
        }

        UIPopUp(amount);

    }
    public void UIPopUp(int amount)
    {
        GameObject dmgPopUp = Instantiate(prefabDamagePopUp, transform.position + new Vector3(1.2f, 2), Quaternion.identity);
        DamagePopUp damagePopUp = dmgPopUp.GetComponent<DamagePopUp>();
        damagePopUp.Setup(amount);
    }
}