using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;
    private TrailRenderer myTrailRenderer;
    private Collider2D myCollider;

    private Vector2 lastVelocity;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTrailRenderer = GetComponent<TrailRenderer>();
        myCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        lastVelocity = myRigidbody2D.velocity;
    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
            DestroyBullet();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateRotation();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        UpdateRotation();
    }

    public void UpdateRotation()
    {
        if (myRigidbody2D.velocity == Vector2.zero)
            return;

        Vector2 v = myRigidbody2D.velocity;
        var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        myCollider.isTrigger = true;

        yield return new WaitForSeconds(1.0f);

        mySpriteRenderer.enabled = true;
        gameObject.SetActive(false);
        myCollider.isTrigger = false;

        LevelController.Instance.CheckForNextLevel();
    }

    public void SetLauchVelocity() => myRigidbody2D.velocity = transform.right * speed;

    public void PauseTrailForOneFrame() => StartCoroutine(PauseTrailForOneFrameCoroutine());

    private IEnumerator PauseTrailForOneFrameCoroutine()
    {
        var trailTime = myTrailRenderer.time;
        myTrailRenderer.time = 0.0f;

        yield return new WaitForEndOfFrame();
        yield return new WaitForFixedUpdate();

        myTrailRenderer.time = trailTime;
    }
}
