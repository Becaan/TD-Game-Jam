using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = transform.GetComponent<Rigidbody2D>();
        myRigidbody2D.velocity = transform.right * speed;
    }

}
