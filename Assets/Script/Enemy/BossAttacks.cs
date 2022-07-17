using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private GameObject target;
    public GameObject bulletHomingPrefab;
    public GameObject monsterPrefab;
    public GameObject bulletShotGunPrefab;
    public float speed = 3f;
    public float Viewdistance = 5f;
    public float attackCooldown = 1f;
    private float attackTime;
    private int attack;

    void Start () {
        target = GameManager.instance.player;
        attackTime = attackCooldown;
    }
 
    void Update(){
        attackTime += Time.deltaTime;
        if (attackTime >= attackCooldown) {
            attackTime = 0f;
            attack = Random.Range(3,4);
            switch (attack)
            {
                case 1:
                    attack1();
                    break;
                case 2:
                    attack2();
                    break;
                case 3:
                    attack3();
                    break;
                case 4:
                    attack4();
                    break;
                case 5:
                    attack5();
                    break;
                case 6:
                    attack6();
                    break;
            }
        }
        
    }
    private void attack1(){  //1 balle qui suit le joueur mais dispawn au bout de 3 sec
        GameObject bullet = null;
        int damage = 3;

        bullet = Instantiate(bulletHomingPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = (transform.position-target.transform.position).normalized;
        bullet.GetComponent<Bullet>().setDamage(damage);

        Destroy(bullet, 3f);
    }
    private void attack2(){ //fais spawn 2 monstres
        GameObject monster1 = null;
        GameObject monster2 = null;

        monster1 = Instantiate(monsterPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        monster1.transform.SetParent(transform);
        monster1.transform.localPosition = (transform.position-target.transform.position).normalized;

        monster2 = Instantiate(monsterPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        monster2.transform.SetParent(transform);
        monster2.transform.localPosition = (transform.position-target.transform.position).normalized;

    }
    private void attack3(){ //3 balles en shotgun vers le joueurs
        GameObject bullet1 = null;
        GameObject bullet2 = null;
        GameObject bullet3 = null;
        int damage = 1;

        bullet1 = Instantiate(bulletShotGunPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet1.transform.SetParent(transform);
        bullet1.transform.localPosition = (target.transform.position-transform.position).normalized + new Vector3(1,0,0);
        bullet1.GetComponent<Bullet>().setDamage(damage);

        bullet1.GetComponent<Rigidbody2D>().AddForce(-target.transform.position.normalized, ForceMode2D.Impulse);

        bullet2 = Instantiate(bulletShotGunPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet2.transform.SetParent(transform);
        bullet2.transform.localPosition = (target.transform.position-transform.position).normalized + new Vector3(0,0,0);;
        bullet2.GetComponent<Bullet>().setDamage(damage);

        bullet2.GetComponent<Rigidbody2D>().AddForce(-target.transform.position.normalized, ForceMode2D.Impulse);

        bullet3 = Instantiate(bulletShotGunPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet3.transform.SetParent(transform);
        bullet3.transform.localPosition = (target.transform.position-transform.position).normalized + new Vector3(-1,0,0);;
        bullet3.GetComponent<Bullet>().setDamage(damage);

        bullet3.GetComponent<Rigidbody2D>().AddForce(-target.transform.position.normalized, ForceMode2D.Impulse);

    }
    private void attack4(){ //4 balles autour du boss spawn et partent moyennement vite

    }
    private void attack5(){ //pose une mine devant lui qui reste 5 sec sur le terrain

    }
    private void attack6(){ //mélange 2 attaques précédentes

    }
}
