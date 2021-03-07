using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowboyController : MonoBehaviour
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

    private Transform armPivotTransform;

    private void Start()
    {
        armPivotTransform = transform.GetChild(0).GetChild(0).GetChild(0);

        for (int i = 0; i < ammo; i++)
            Instantiate(ammoUIImage, ammoLayoutGroup);

        //Instantiate Bullet Object Pool
        bullets = new List<GameObject>(poolSize);
        GameObject temp;

        for (int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(bulletPrefab);
            temp.SetActive(false);
            bullets.Add(temp);
        }
    }

    private void Update()
    {
        ArmAiming();
        ProcessInput();
    }

    private void ArmAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = armPivotTransform.position.z;

        if (mousePosition.x > armPivotTransform.position.x)
        {
            Quaternion lookRotation = Quaternion.LookRotation(mousePosition - armPivotTransform.position);
            lookRotation = Quaternion.Euler(0.0f, 0.0f, -lookRotation.eulerAngles.x);
            armPivotTransform.rotation = lookRotation;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!bullets[i].activeInHierarchy && ammo > 0)
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
