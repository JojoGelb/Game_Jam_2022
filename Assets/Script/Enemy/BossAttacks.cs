using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private GameObject target;
    public GameObject bulletHomingPrefab;
    public GameObject monsterPrefab;
    public GameObject bulletShotGunPrefab;
    public GameObject bulletBossPrefab;
    public GameObject minePrefab;
    public float speed = 3f;
    public float Viewdistance = 5f;
    public float attackCooldown = 1f;
    private float attackTime;
    private int attack;
    public int nbMaxOfMob = 6;

    void Start () {
        target = GameManager.instance.player;
        attackTime = attackCooldown;
    }
 
    void Update(){
        attackTime += Time.deltaTime;
        if (attackTime >= attackCooldown) {
            attackTime = 0f;
            attack = 5;//Random.Range(1,7);
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
        bullet.transform.localPosition = (target.transform.position-transform.position).normalized;
        bullet.GetComponent<Bullet>().setDamage(damage);

        Destroy(bullet, 3f);
    }
    private void attack2(){ //fais spawn 2 monstres
        for(int i=0; i<2; i++) {
            if (GameManager.instance.currentRoom.monsters.Count <nbMaxOfMob) {
                GameObject monster = null;

                monster = Instantiate(monsterPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
                monster.transform.SetParent(transform);
                monster.transform.localPosition = (transform.position-target.transform.position + i*transform.right).normalized;

                GameManager.instance.currentRoom.monsters.Add(monster);
            }
        }
    }
    private void attack3(){ //3 balles en shotgun vers le joueurs
        GameObject bullet1 = null;
        GameObject bullet2 = null;
        GameObject bullet3 = null;
        Vector3 shootingDir = target.transform.position-transform.position;
        Vector3 spacing = new Vector3(0,0,0);
        int damage = 1;
        int speed = 3;

        if(shootingDir.x>1 && shootingDir.y<0) {//bas droite
            spacing = new Vector3(0.2f,0.2f,0);
        } else if (shootingDir.x>0 && shootingDir.y<0) {//bas
            spacing = new Vector3(0.2f,0,0);
        } else if (shootingDir.x<0 && shootingDir.y<0) {//bas gauche
            spacing = new Vector3(0.2f,-0.2f,0);
        } else if (shootingDir.x>1 && shootingDir.y>0 && shootingDir.y<1){ //droite
            spacing = new Vector3(0,0.2f,0);
        } else if (shootingDir.x<0 && shootingDir.y>0 && shootingDir.y<1){ //gauche
            spacing = new Vector3(0,0.2f,0); 
        } else if (shootingDir.x>1 && shootingDir.y>1) {//haut droite
            spacing = new Vector3(0.2f,-0.2f,0);
        } else if (shootingDir.x>0 && shootingDir.y>1) {//haut
            spacing = new Vector3(0.2f,0,0);
        } else if (shootingDir.x<0 && shootingDir.y>1) {//haut gauche
            spacing = new Vector3(0.2f,0.2f,0);
        } 

        bullet1 = Instantiate(bulletShotGunPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet1.transform.SetParent(transform);
        bullet1.transform.localPosition = (shootingDir).normalized + spacing;
        bullet1.GetComponent<Bullet>().setDamage(damage);

        bullet1.GetComponent<Rigidbody2D>().AddForce(shootingDir.normalized*speed, ForceMode2D.Impulse);

        bullet2 = Instantiate(bulletShotGunPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet2.transform.SetParent(transform);
        bullet2.transform.localPosition = (shootingDir).normalized;
        bullet2.GetComponent<Bullet>().setDamage(damage);

        bullet2.GetComponent<Rigidbody2D>().AddForce(shootingDir.normalized*speed, ForceMode2D.Impulse);

        bullet3 = Instantiate(bulletShotGunPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bullet3.transform.SetParent(transform);
        bullet3.transform.localPosition = (shootingDir).normalized - spacing;
        bullet3.GetComponent<Bullet>().setDamage(damage);

        bullet3.GetComponent<Rigidbody2D>().AddForce(shootingDir.normalized*speed, ForceMode2D.Impulse);

    }
    private void attack4(){ //8 balles autour du boss spawn et partent moyennement vite
        GameObject bulletN = null;
        GameObject bulletE = null;
        GameObject bulletS = null;
        GameObject bulletW = null;

        GameObject bulletNE = null;
        GameObject bulletNW = null;
        GameObject bulletSE = null;
        GameObject bulletSW = null;
        int damage = 1;
        int speed = 3;

        bulletN = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        bulletN.transform.SetParent(transform);
        bulletN.transform.localPosition = new Vector3(0, 0.6f, 0);
        bulletN.GetComponent<Bullet>().setDamage(damage);

        bulletN.GetComponent<Rigidbody2D>().AddForce(Vector2.up*speed, ForceMode2D.Impulse);

        bulletE = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, -90));
        bulletE.transform.SetParent(transform);
        bulletE.transform.localPosition = new Vector3(0.6f, 0, 0);
        bulletE.GetComponent<Bullet>().setDamage(damage);

        bulletE.GetComponent<Rigidbody2D>().AddForce(Vector2.right*speed, ForceMode2D.Impulse);

        bulletS = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 180));
        bulletS.transform.SetParent(transform);
        bulletS.transform.localPosition = new Vector3(0, -0.6f, 0);
        bulletS.GetComponent<Bullet>().setDamage(damage);

        bulletS.GetComponent<Rigidbody2D>().AddForce(-Vector2.up*speed, ForceMode2D.Impulse);

        bulletW = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 90));
        bulletW.transform.SetParent(transform);
        bulletW.transform.localPosition = new Vector3(-0.6f, 0, 0);
        bulletW.GetComponent<Bullet>().setDamage(damage);

        bulletW.GetComponent<Rigidbody2D>().AddForce(-Vector2.right*speed, ForceMode2D.Impulse);

        bulletNE = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, -45));
        bulletNE.transform.SetParent(transform);
        bulletNE.transform.localPosition = new Vector3(0.6f, 0.6f, 0);
        bulletNE.GetComponent<Bullet>().setDamage(damage);

        bulletNE.GetComponent<Rigidbody2D>().AddForce((Vector2.up+Vector2.right).normalized*speed, ForceMode2D.Impulse);

        bulletNW = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 45));
        bulletNW.transform.SetParent(transform);
        bulletNW.transform.localPosition = new Vector3(-0.6f, 0.6f, 0);
        bulletNW.GetComponent<Bullet>().setDamage(damage);

        bulletNW.GetComponent<Rigidbody2D>().AddForce((Vector2.up-Vector2.right).normalized*speed, ForceMode2D.Impulse);

        bulletSE = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, -135));
        bulletSE.transform.SetParent(transform);
        bulletSE.transform.localPosition = new Vector3(0.6f, -0.6f, 0);
        bulletSE.GetComponent<Bullet>().setDamage(damage);

        bulletSE.GetComponent<Rigidbody2D>().AddForce((-Vector2.up+Vector2.right).normalized*speed, ForceMode2D.Impulse);

        bulletSW = Instantiate(bulletBossPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 135));
        bulletSW.transform.SetParent(transform);
        bulletSW.transform.localPosition = new Vector3(-0.6f, -0.6f, 0);
        bulletSW.GetComponent<Bullet>().setDamage(damage);

        bulletSW.GetComponent<Rigidbody2D>().AddForce(-(Vector2.up+Vector2.right).normalized*speed, ForceMode2D.Impulse);

    }
    private void attack5(){ //pose une mine devant lui qui reste 5 sec sur le terrain
        GameObject mine = null;
        Vector3 shootingDir = target.transform.position-transform.position;
        Vector3 spacing = new Vector3(0,0,0);

        int damage = 5;

        if(shootingDir.x>1 && shootingDir.y<0) {//bas droite
            spacing = new Vector3(-2,2,-0.1f);
        } else if (shootingDir.x>0 && shootingDir.y<0) {//bas
            spacing = new Vector3(0,2,-0.1f);
        } else if (shootingDir.x<0 && shootingDir.y<0) {//bas gauche
            spacing = new Vector3(2,2,-0.1f);
        } else if (shootingDir.x>1 && shootingDir.y>0 && shootingDir.y<1){ //droite
            spacing = new Vector3(-2,0,-0.1f);
        } else if (shootingDir.x<0 && shootingDir.y>0 && shootingDir.y<1){ //gauche
            spacing = new Vector3(2,0,-0.1f); 
        } else if (shootingDir.x>1 && shootingDir.y>1) {//haut droite
            spacing = new Vector3(-2,-2,-0.1f);
        } else if (shootingDir.x>0 && shootingDir.y>1) {//haut
            spacing = new Vector3(0,-2,-0.1f);
        } else if (shootingDir.x<0 && shootingDir.y>1) {//haut gauche
            spacing = new Vector3(2,-2,-0.1f);
        } 

        mine = Instantiate(minePrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, -135));
        mine.transform.SetParent(target.transform);
        mine.transform.localPosition = spacing;
        mine.GetComponent<Bullet>().setDamage(damage);
    }
    private void attack6(){ //mélange 2 attaques précédentes
        for(int i = 0; i < 2; i++) {
            attack = Random.Range(1,6);
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
            }
        } 
    }
}
