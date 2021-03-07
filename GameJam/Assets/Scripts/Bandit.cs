using System.Collections;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            myAnimator.Play("Death");
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForEndOfFrame(); //Wait for animator state to get updated

        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
