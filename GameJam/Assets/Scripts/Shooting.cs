using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Animator cowboyArmAnimator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private CameraShake cameraShake;
    
    [SerializeField] private int poolSize;

    private List<GameObject> bullets;

    #region MonoBehaviour Events
    private void Start()
    {
        bullets = new List<GameObject>(10);
        GameObject temp;

        for(int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(bulletPrefab);
            temp.SetActive(false);
            bullets.Add(temp);
        }
    }

    private void Update()
    {
        ProcessInput();
    }
    #endregion

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cowboyArmAnimator.GetComponent<Animator>().Play("Arm_shoot", 0, 0.0f);
            Shoot();
        }
    }

    private void Shoot()
    {
        for(int i = 0; i < poolSize; i++)
        {
            if(!bullets[i].activeInHierarchy)
            {
                Transform bulletTransform = bullets[i].GetComponent<Transform>();

                bulletTransform.position = firePoint.position;
                bulletTransform.rotation = firePoint.rotation;

                bullets[i].SetActive(true);
                bullets[i].GetComponent<Bullet>().LaunchBullet();

                cameraShake.Shake();
                break;
            }
        }
    }
}
