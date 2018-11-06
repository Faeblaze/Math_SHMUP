using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;

    public void Shoot(int number, GameLogic.Operator op)
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>().SetValues(op, number);
    }
}
