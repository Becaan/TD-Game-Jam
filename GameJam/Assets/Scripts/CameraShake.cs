using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public void Shake() => StartCoroutine(ShakeCoroutine(duration, magnitude));

    public void Shake(float duration, float magnitude) => StartCoroutine(ShakeCoroutine(duration, magnitude));
    
    public IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
         
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
