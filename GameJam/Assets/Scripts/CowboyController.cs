using UnityEngine;

public class CowboyController : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Quaternion lookRotation = Quaternion.LookRotation(mousePosition - transform.position);
        lookRotation = Quaternion.Euler(0.0f, 0.0f, -lookRotation.eulerAngles.x);
        transform.rotation = lookRotation;
    }
}
