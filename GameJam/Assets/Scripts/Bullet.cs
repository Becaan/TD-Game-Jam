using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnBecameInvisible() => DestroyBullet();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
            DestroyBullet();
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
        gameObject.SetActive(false);
    }

    public void LaunchBullet() => myRigidbody2D.velocity = transform.right * speed;
}
