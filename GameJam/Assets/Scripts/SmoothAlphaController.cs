using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class SmoothAlphaController : MonoBehaviour
{
    public float speed = 0.1f;
    public float startAlpha = 1.0f;
    public float finishThreshold = 0.02f;

    public float fadeOutMultiplier = 2.5f;

    public bool fadeOutOnStart = false;

    private float tempSpeed;
    private float tempFinishThreshold;

    private CanvasGroup targetGroup;

    public float targetAlpha { private set; get; } = 1.0f;

    private float groupAVelocity;

    private UnityAction targetCallback;
    private bool targetReached = false;

    private UnityAction onLevelLoadCallback;
    private bool waitForLevelLoad = false;

    #region Monobehaviour Events
    private void Awake()
    {
        targetReached = true;
        targetAlpha = startAlpha;

        targetGroup = GetComponent<CanvasGroup>();

#if UNITY_EDITOR
        if (fadeOutOnStart)
            StartCoroutine(FadeOutOnStart());
#endif

        SceneManager.sceneLoaded += OnLoadCallback;
    }

    private void Update()
    {
        if (targetGroup)
            targetGroup.alpha = Mathf.SmoothDamp(targetGroup.alpha, targetAlpha, ref groupAVelocity,
                tempSpeed > 0.0f ? tempSpeed : speed, Mathf.Infinity, Time.unscaledDeltaTime);

        if (!targetReached)
        {
            float threshold = (tempFinishThreshold > 0.0f ? tempFinishThreshold : finishThreshold);

            if (targetGroup
                && (targetGroup.alpha >= targetAlpha - threshold
                && targetGroup.alpha <= targetAlpha + threshold))
            {
                targetGroup.alpha = targetAlpha;

                targetReached = true;

                targetGroup.blocksRaycasts = false;

                if (targetCallback != null)
                    targetCallback.Invoke();
            }
        }
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode mode)
    {
        if (waitForLevelLoad)
        {
            StartCoroutine(FadeOutOnLevelLoad());

            if (onLevelLoadCallback != null)
                onLevelLoadCallback.Invoke();

            waitForLevelLoad = false;
        }
    }
    #endregion

    public float GetCurrentAlpha()
    {
        return targetGroup.alpha;
    }

    public void SetTargetAlpha(float value, UnityAction callback = null, float time = -1.0f, float customFadeOutMultiplier = -1.0f, float customFinishThreshold = -1.0f)
    {
        targetGroup.blocksRaycasts = true;

        targetCallback = callback;
        targetAlpha = value;

        if (targetGroup.alpha > value)
        {
            if (customFadeOutMultiplier < 0)
                customFadeOutMultiplier = fadeOutMultiplier;

            tempSpeed = time > 0 ? time * customFadeOutMultiplier : speed * customFadeOutMultiplier;
        }
        else
            tempSpeed = time;

        tempFinishThreshold = customFinishThreshold;

        targetReached = false;
    }

    public void LevelLoad(int buildIndex, UnityAction callback = null)
    {
        waitForLevelLoad = true;

        onLevelLoadCallback = callback;
        SetTargetAlpha(1.0f, () => { SceneManager.LoadScene(buildIndex); });
    }

    public void LevelLoad(string sceneName, UnityAction callback = null)
    {
        waitForLevelLoad = true;

        onLevelLoadCallback = callback;
        SetTargetAlpha(1.0f, () => { SceneManager.LoadScene(sceneName); });
    }

    IEnumerator FadeOutOnLevelLoad()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SetTargetAlpha(0.0f, null);
    }

    IEnumerator FadeOutOnStart()
    {
        yield return new WaitForSecondsRealtime(1.0f);

        SetTargetAlpha(0.0f, null, 0.175f);
    }

    public void FadeOutOnStartPublic() //Smoothly transition from Unity's splash screen
    {
        StartCoroutine(FadeOutOnStart());
    }
}