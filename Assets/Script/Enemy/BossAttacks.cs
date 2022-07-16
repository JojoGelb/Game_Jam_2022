using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject target;
    public float speed = 3f;
    public float Viewdistance = 5f;
    public float attackCooldown = 1f;
    private float attackTime;
    private int attack;
    private Rigidbody2D rb;

    void Start () {
        target = GameManager.instance.player;
        attackTime = attackCooldown;
    }
 
    void Update(){
        attackTime += Time.deltaTime;
        if (attackTime > attackCooldown) {
            attackTime = 0f;
            attack = Random.Range(1,7);
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

    }
    private void attack2(){ //fais spawn 2 monstres

    }
    private void attack3(){ //3 balles en shotgun vers le joueurs

    }
    private void attack4(){ //4 balles autour du boss spawn et partent moyennement vite

    }
    private void attack5(){ //pose une mine devant lui qui reste 5 sec sur le terrain

    }
    private void attack6(){ //mélange 2 attaques précédentes

    }
}
