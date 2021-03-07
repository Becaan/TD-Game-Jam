using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Vector2 widthThresold;
    [SerializeField] Vector2 heightThresold;
    Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = transform.GetComponent<Rigidbody2D>();
        myRigidbody2D.velocity = transform.right * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
