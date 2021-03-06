using UnityEngine;

public class Mirror : MonoBehaviour
{
    public const string SHRINK_STATE_NAME = "Shrink";
    public const string EXPAND_STATE_NAME = "Expand";

    [SerializeField] private Mirror nextMirror;

    private Animator mirrorAnimator;
    private AudioSource myAudioSource;

    #region MonoBehaviour Events
    private void Awake()
    {
        mirrorAnimator = GetComponentInChildren<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        
        PlayAnimation(SHRINK_STATE_NAME);
        nextMirror.PlayAnimation(EXPAND_STATE_NAME);

        myAudioSource.Play();

        var bullet = collision.GetComponent<Bullet>();
        bullet.PauseTrailForOneFrame();
        
        bullet.transform.position = nextMirror.transform.position;
        bullet.transform.rotation = nextMirror.transform.rotation;
        bullet.SetLauchVelocity();
    }
    #endregion

    private void PlayAnimation(string animationName) => mirrorAnimator.Play(animationName, 0, 0.0f);
}
