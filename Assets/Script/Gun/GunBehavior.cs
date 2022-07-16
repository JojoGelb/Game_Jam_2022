using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
   private int numberOfBullet;
    public float bulletSpeed = 3.0f;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform shootingPoint;
    public int damage = 1;

    public float cooldownTime = 0.5f;
    private float timerColdownShoot = 3f;

    // Start is called before the first frame update
    void Start()
    {
        numberOfBullet = Random.Range(1,10);
    }


    public Direction shoot(Vector2 axis)
    {
        // 0 South , 1 East, 2 West, 3 North, -1 None
        Direction shootingDirection = getShootingDirection(axis);
        changeLookingDirection(shootingDirection);

        timerColdownShoot += Time.deltaTime;

        if (timerColdownShoot > cooldownTime && shootingDirection != Direction.None)
        {
            timerColdownShoot = 0;
            GameObject bullet = null;
            Vector3 direction;

        switch (shootingDirection)
        {
            case Direction.South:
                bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.Euler(0, 0, 180));
                break;
            case Direction.North:
                 bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.Euler(0, 0, 0));
                 break;
            case Direction.East:
                bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.Euler(0, 0, -90));
                break;
            case Direction.West:
                bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.Euler(0, 0, 90));
                break;
            }
            direction = transform.position - shootingPoint.transform.position;
            bullet.GetComponent<Bullet>().setDamage(damage);
            //bullet.transform.up =
            bullet.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }

        return shootingDirection;
    }

    void changeLookingDirection(Direction facingDirection)
    {
        switch (facingDirection)
        {
            case Direction.North:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.South:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.East:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.West:
                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }


    Direction getShootingDirection(Vector2 shootingDirection)
    {
        if (shootingDirection.y < 0)
        {
            return Direction.South;
        }
        else if (shootingDirection.x > 0)
        {
            return Direction.East;
        }
        else if (shootingDirection.x < 0)
        {
            return Direction.West;
        }
        else if (shootingDirection.y > 0)
        {
            return Direction.North;
        }
        else
        {
            return Direction.None;
        }
    }

}
