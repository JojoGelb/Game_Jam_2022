using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    private int bulletMax = 6;
    private int numberOfBullet = 0;
    public float bulletSpeed = 3.0f;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform shootingPoint;
    public int damage = 1;
    private int damageModifier = 0;

    public float cooldownTime = 0.5f;
    private float timerColdownShoot = 3f;

    public GameObject prefabMessagePopUp;

    // Start is called before the first frame update
    void Start()
    {
        numberOfBullet = Random.Range(1, bulletMax + 1);
    }


    public Direction shoot(Vector2 axis)
    {
        // 0 South , 1 East, 2 West, 3 North, -1 None
        Direction shootingDirection = getShootingDirection(axis);
        changeLookingDirection(shootingDirection);

        timerColdownShoot += Time.deltaTime;

        if (timerColdownShoot > cooldownTime && shootingDirection != Direction.None)
        {

            if (numberOfBullet == 0)
            {
                crashReloading();
                return shootingDirection;
            }

            numberOfBullet--;

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
            bullet.GetComponent<Bullet>().setDamage(damage + damageModifier);
            //bullet.transform.up =
            bullet.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * bulletSpeed, ForceMode2D.Impulse);

            if (damageModifier == 0) damageModifier++;
            else damageModifier *= 2;
        }

        return shootingDirection;
    }

    void crashReloading()
    {
        UIPopUp("Crash reloading");
        timerColdownShoot = -3f;
        numberOfBullet = Random.Range(1, bulletMax + 1);
        damageModifier = 0;
    }

    public void reloading()
    {
        UIPopUp("Reloading");
        timerColdownShoot = -1.5f;

        if (numberOfBullet != 0)
        {
            damageModifier = 0;
        }

        numberOfBullet = Random.Range(1, bulletMax + 1);

    }

    void UIPopUp(string message)
    {
        GameObject msgPopUp = Instantiate(prefabMessagePopUp, transform.position + new Vector3(1.2f, 2), Quaternion.identity);
        msgPopUp.transform.SetParent(this.gameObject.transform);
        MessagePopUp messagePopUp = msgPopUp.GetComponent<MessagePopUp>();
        messagePopUp.Setup(message);
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
