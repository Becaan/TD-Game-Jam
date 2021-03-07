using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int ammo;
    [SerializeField] private GameObject ammoUIImage;
    [SerializeField] private Transform ammoLayoutGroup;

    [SerializeField] private Animator cowboyArmAnimator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private CameraShake cameraShake;
    
    [SerializeField] private int poolSize;

    private List<GameObject> bullets;

    #region MonoBehaviour Events
    private void Start()
    {
        for (int i = 0; i < ammo; i++)
            Instantiate(ammoUIImage, ammoLayoutGroup);

        bullets = new List<GameObject>(poolSize);
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
            Shoot();
        }
    }

    private void Shoot()
    {
        for(int i = 0; i < poolSize; i++)
        {
            if(!bullets[i].activeInHierarchy && ammo > 0)
            {
                cowboyArmAnimator.GetComponent<Animator>().Play("Arm_shoot", 0, 0.0f);

                Transform bulletTransform = bullets[i].GetComponent<Transform>();

                bulletTransform.position = firePoint.position;
                bulletTransform.rotation = firePoint.rotation;

                bullets[i].SetActive(true);
                bullets[i].GetComponent<Bullet>().LaunchBullet();

                var currentAmmoSprite = ammoLayoutGroup.GetChild(ammo - 1).GetComponent<Image>();
                currentAmmoSprite.color = new Vector4(currentAmmoSprite.color.r, currentAmmoSprite.color.g, currentAmmoSprite.color.b, 0.15f);
                ammo--;

                cameraShake.Shake();
                break;
            }
        }
    }
}
