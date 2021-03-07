using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        myRigidbody2D = transform.GetComponent<Rigidbody2D>();
        mySpriteRenderer = transform.GetComponent<SpriteRenderer>();
        myRigidbody2D.velocity = transform.right * speed;
    }

    void OnBecameInvisible() => DestroyBullet();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        if (!gameObject.activeInHierarchy)
            return;

        StartCoroutine(DestroyBulletCoroutine());
    }

    private IEnumerator DestroyBulletCoroutine()
    {
        mySpriteRenderer.enabled = false;
        myRigidbody2D.velocity = Vector2.zero;

        yield return new WaitForSeconds(1.0f);

        mySpriteRenderer.enabled = true;
        myRigidbody2D.velocity = transform.right * speed;

        gameObject.SetActive(false);
    }
}
