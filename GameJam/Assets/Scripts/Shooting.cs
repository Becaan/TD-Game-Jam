using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject cowboyArm;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    void Update()
    {
        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            cowboyArm.GetComponent<Animator>().Play("Arm_shoot", 0, 0.0f);

            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
