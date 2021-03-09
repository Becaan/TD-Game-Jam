﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowboyController : MonoBehaviour
{
    public const string ARM_SHOOT_STATE_NAME = "Shoot";

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int ammo;
    public int Ammo
    {
        get => ammo;
        private set => ammo = value;
    }

    [SerializeField] private GameObject ammoUIImage;
    [SerializeField] private Transform ammoLayoutGroup;

    [SerializeField] private AudioSource gunShotAudio;
    [SerializeField] private AudioSource EmptyGunAudio;

    [SerializeField] private Animator cowboyArmAnimator;
    [SerializeField] private Transform armPivotTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private int poolSize;

    public List<GameObject> Bullets { get; private set; }

    public static CowboyController Instance;

    #region MonoBehaviour Events
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < ammo; i++)
            Instantiate(ammoUIImage, ammoLayoutGroup);
        
        Bullets = new List<GameObject>(poolSize);

        GameObject temp;
        for (int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(bulletPrefab);
            temp.SetActive(false);
            Bullets.Add(temp);
        }
    }

    private void Update()
    {
        ArmAiming();
        ProcessInput();
    }
    #endregion

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
            Shoot();
    }

    private void Shoot()
    {
        if (ammo == 0)
        {
            EmptyGunAudio.Play();
            return;
        }

        gunShotAudio.Play();

        for (int i = 0; i < poolSize; i++)
        {
            if (!Bullets[i].activeInHierarchy)
            {
                cowboyArmAnimator.Play(ARM_SHOOT_STATE_NAME, 0, 0.0f);

                Transform bulletTransform = Bullets[i].GetComponent<Transform>();

                bulletTransform.position = firePoint.position;
                bulletTransform.rotation = firePoint.rotation;

                Bullets[i].SetActive(true);
                Bullets[i].GetComponent<Bullet>().SetLauchVelocity();

                var currentAmmoSprite = ammoLayoutGroup.GetChild(ammo - 1).GetComponent<Image>();
                currentAmmoSprite.color = new Vector4(currentAmmoSprite.color.r, currentAmmoSprite.color.g, currentAmmoSprite.color.b, 0.15f);
                ammo--;

                cameraShake.Shake();
                break;
            }
        }
    }
}
