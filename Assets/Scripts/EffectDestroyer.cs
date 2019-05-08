using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    private void Start()
    {
        // Destroy effect after it's finished
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
