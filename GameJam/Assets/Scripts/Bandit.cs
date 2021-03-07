using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.CompareTag("Bullet"))
        {
            //myRigidbody2D.isKinematic = false;
            Destroy(gameObject);
        }
    }
    
}
