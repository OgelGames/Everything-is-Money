using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float magnatude; // How much it can shake
    public float duraion; // How long to shake for

    private bool isShaking; // Is the ShakeCamera() coroutine already running?

    public void Shake()
    {
        // Start shaking if not already shaking
        if (!isShaking)
        {
            StartCoroutine(ShakeCamera());
        }
    }

    private IEnumerator ShakeCamera()
    {
        isShaking = true;

        // Store position before shaking
        Vector3 originalPosition = transform.position;

        // Shake!
        for (float i = 0; i < duraion; i += Time.deltaTime)
        {
            // Set position to a new random position
            transform.position = new Vector3(Random.Range(-1f, 1f) * magnatude, Random.Range(-1f, 1f) * magnatude, originalPosition.z);
            yield return null;
        }

        // After shaking set position to pre-shake position
        transform.position = originalPosition;

        isShaking = false;
    }
}
