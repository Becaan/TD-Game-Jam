using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject cowboyArm;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

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
        StartCoroutine(cameraShake.Shake(duration, magnitude));
    }
}
