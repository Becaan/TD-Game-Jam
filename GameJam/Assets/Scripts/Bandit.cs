using System.Collections;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    [SerializeField] private AudioSource KillEffect;

    public const string DEATH_STATE_NAME = "Death";

    private Animator myAnimator;

    #region MonoBehaviour Events
    private void Start()
    {
        myAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            myAnimator.Play(DEATH_STATE_NAME);
            KillEffect.Play();
            StartCoroutine(DeathCoroutine());
        }
    }
    #endregion

    private IEnumerator DeathCoroutine()
    {  
        yield return new WaitForEndOfFrame(); //Wait for animator state to get updated

        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
        LevelController.Instance.CheckForNextLevel();
    }
}
