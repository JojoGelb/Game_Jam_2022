using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    private GameObject target;
    public GameObject bulletShotGunPrefab;
    public float attackCooldown = 3f;
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
            attack3();
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
}
