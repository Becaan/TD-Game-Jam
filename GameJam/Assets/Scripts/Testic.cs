using UnityEngine;

public class Testic : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))

            GetComponent<Animator>().Play("Arm_shoot", 0, 0.0f);
    }
}
