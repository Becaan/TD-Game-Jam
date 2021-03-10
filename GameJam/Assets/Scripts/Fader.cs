using UnityEngine;
using UnityEngine.Events;

public class Fader
{
    private static SmoothAlphaController smoothAlphaController;

    private static bool initialized = false;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        if (initialized)
            return;

        var instance = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Fader Canvas"));
         
        smoothAlphaController = instance.GetComponentInChildren<SmoothAlphaController>();

        initialized = true;
    }

    public static void FadeOutOnStart()
    {
        if (!smoothAlphaController)
            Initialize();

        smoothAlphaController.FadeOutOnStartPublic();
    }

    public static void SetTargetAlpha(float value, UnityAction callback = null, float time = -1.0f, float customFadeOutMultiplier = -1.0f, float customFinishThreshold = -1.0f)
    {
        smoothAlphaController.SetTargetAlpha(value, callback, time, customFadeOutMultiplier, customFinishThreshold);
    }

    public static void LevelLoad(int buildIndex, UnityAction callback = null)
    {
        smoothAlphaController.LevelLoad(buildIndex, callback);
    }

    public static void LevelLoad(string sceneName, UnityAction callback = null)
    {
        smoothAlphaController.LevelLoad(sceneName, callback);
    }

    public static float GetCurrentAlpha()
    {
        return smoothAlphaController.GetCurrentAlpha();
    }

    public static float GetTargetAlpha()
    {
        return smoothAlphaController.targetAlpha;
    }
}